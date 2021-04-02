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
        private ILNZU_dbContext db;
        public AccountController(ILNZU_dbContext context)
        {
            db = context;
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
                User user = DBManager.findUser(model.Email).Result;
                if (user != null)
                {
                    string SaltedPassword = model.Password + user.Salt;
                    User ExistingUser = DBManager.findUser(model.Email, SaltedPassword).Result;
                    if (ExistingUser != null)
                    {
                        await Authenticate(model.Email, ExistingUser.Id);

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
                    int userId = DBManager.addUser(model.Email, model.Password, model.Name, model.Surname, model.Username).Result;

                    await Authenticate(model.Email, userId);

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

        private async Task Authenticate(string userName, int UserId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim(ClaimsIdentity.DefaultNameClaimType, UserId.ToString())
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
