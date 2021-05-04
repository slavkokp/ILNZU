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
using BLL.Services;
using ILNZU.ViewModels;

namespace ILNZU.Controllers
{
    public class HomeController : Controller
    {
        private readonly MeetingRoomRepository rep;

        public HomeController(MeetingRoomRepository rep)
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

        [Authorize]
        public async Task<IActionResult> CreateMeeting(RoomModel model)
        {
            var room = rep.CreateRoom(model.Title, Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value)).Result;
            rep.AddUserToMeeting(Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value), room.MeetingRoomId);
            return View();
        }

        [Authorize]
        public async Task<IActionResult> DeleteMeeting(int meetingId)
        {
            if(rep.CheckIfUserIsCreatorOfMeetingRoom(Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value), meetingId).Result)
            {
                rep.DeleteRoom(meetingId);
            }
            else
            {
                rep.RemoveUserFromMeeting(Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value), meetingId);
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Kick(int userId, int meetingId)
        {
            rep.RemoveUserFromMeeting(userId, meetingId);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
