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

namespace ILNZU.Controllers
{
    public class AccountController : Controller
    {
        private MvcUserContext db;
        public AccountController(MvcUserContext context)
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
                User user = await db.User.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user != null)
                {
                    string hash = PasswordHash.hashPassword(model.Password + user.Salt);
                    User ExistingUser = await db.User.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == hash);
                    if (ExistingUser != null)
                    {
                        await Authenticate(model.Email);

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
                    string salt = PasswordHash.GetSalt();
                    string hash = PasswordHash.hashPassword(model.Password + salt);

                    db.User.Add(new User { Email = model.Email, Password = hash, Name = model.Name, ProfilePicture = 0, Surname = model.Surname, Username = model.Username, Salt = salt });

                    await db.SaveChangesAsync();

                    await Authenticate(model.Email);

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

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
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
