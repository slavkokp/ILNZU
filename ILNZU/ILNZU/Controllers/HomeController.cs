using ILNZU.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using DAL;

namespace ILNZU.Controllers
{
    public class HomeController : Controller
    {
        DBRepository rep;
        public HomeController(DBRepository rep)
        {
            this.rep = rep;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            ViewBag.MeetingRooms = await rep.GetMeetingRooms(Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            var res = await rep.GetMeetingRooms(Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            return View("Index", User.Identity.Name);
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
