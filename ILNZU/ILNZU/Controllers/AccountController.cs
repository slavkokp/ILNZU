using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ILNZU.ViewModels;
using DAL.Models;
using DAL.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Cryptography;
using System.Text;
using DAL;
using System;

namespace ILNZU.Controllers
{
    public class AccountController : Controller
    {
        private DBRepository dbRepository;
        public AccountController(DBRepository rep)
        {
            dbRepository = rep;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = dbRepository.FindUser(model.Email).Result;
                if (user != null)
                {
                    string SaltedPassword = model.Password + user.Salt;
                    User ExistingUser = dbRepository.FindUser(model.Email, SaltedPassword).Result;
                    if (ExistingUser != null)
                    {
                        await Authenticate(model.Email, ExistingUser.Id, ExistingUser.Name);

                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Wrong login or password");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {   
                try
                {
                    int userId = dbRepository.AddUser(model.Email, model.Password, model.Name, model.Surname, model.Username).Result;

                    await Authenticate(model.Email, userId, model.Name);

                    return RedirectToAction("Index", "Home");
                }
                catch(DbUpdateException e)
                {
                    if (e.InnerException.Message.Split(":")[0] == "23505")
                    {
                        ModelState.AddModelError("", "Email already taken");
                    }
                    else
                    {
                        ModelState.AddModelError("", e.InnerException.Message);
                    }
                }
            }
            return View(model);
        }

        private async Task Authenticate(string Email, int UserId, string Name)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, Name),
                new Claim(ClaimTypes.NameIdentifier, UserId.ToString()),
                new Claim(ClaimTypes.Email, Email)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
