// <copyright file="AccountController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ILNZU.Controllers
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using BLL.Services;
    using DAL.Models;
    using ILNZU.ViewModels;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Account controller.
    /// </summary>
    public class AccountController : Controller
    {
        private readonly UserRepository rep;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="rep">User repository.</param>
        public AccountController(UserRepository rep)
        {
            this.rep = rep;
        }

        /// <summary>
        /// Shows login view.
        /// </summary>
        /// <returns>A login view.</returns>
        [HttpGet]
        public IActionResult Login()
        {
            return this.View();
        }

        /// <summary>
        /// Logs in.
        /// </summary>
        /// <param name="model">login model.</param>
        /// <returns>Login view.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (this.ModelState.IsValid)
            {
                User user = this.rep.FindUser(model.Email).Result;
                if (user != null)
                {
                    string saltedPassword = model.Password + user.Salt;
                    User existingUser = this.rep.FindUser(model.Email, saltedPassword).Result;
                    if (existingUser != null)
                    {
                        await this.Authenticate(model.Email, existingUser.Id, existingUser.Name);

                        return this.RedirectToAction("Index", "Home");
                    }
                }

                this.ModelState.AddModelError(string.Empty, "Wrong login or password");
            }

            return this.View(model);
        }

        /// <summary>
        /// Shows refister view.
        /// </summary>
        /// <returns>returns register view.</returns>
        [HttpGet]
        public IActionResult Register()
        {
            return this.View();
        }

        /// <summary>
        /// Registers new user.
        /// </summary>
        /// <param name="model">Register model.</param>
        /// <returns>A view.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    int userId = this.rep.AddUser(model.Email, model.Password, model.Name, model.Surname, model.Username).Result;

                    await this.Authenticate(model.Email, userId, model.Name);

                    return this.RedirectToAction("Index", "Home");
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException.Message.Split(":")[0] == "23505")
                    {
                        this.ModelState.AddModelError(string.Empty, "Email already taken");
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, e.InnerException.Message);
                    }
                }
            }

            return this.View(model);
        }

        /// <summary>
        /// Logs out from the current account.
        /// </summary>
        /// <returns>A login view.</returns>
        public async Task<IActionResult> Logout()
        {
            await this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return this.RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// Authenticates user.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="userId">User id.</param>
        /// <param name="name">Name.</param>
        /// <returns>Nothing.</returns>
        private async Task Authenticate(string email, int userId, string name)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, name),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email, email),
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await this.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
