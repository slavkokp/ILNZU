using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ILNZU.ViewModels;
using DAL;
using DAL.Models;
using System.Security.Claims;

namespace ILNZU.Controllers
{
    public class MeetingController : Controller
    {
        private readonly DBRepository rep;
        public MeetingController(DBRepository rep)
        {
            this.rep = rep;
        }

        [Authorize]
        public async Task<IActionResult> Room(int? id)
        {
            bool allowed = await rep.CheckIfUserIsMemberOfMeetingRoom(Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value), Convert.ToInt32(id));
            if (allowed)
            {
                ViewBag.MeetingRoomId = id;
                List<Message> messages = await rep.GetMessages(Convert.ToInt32(id));
                messages = messages.OrderByDescending(msg => msg.DateTime).ToList();
                foreach (var msg in messages)
                {
                    msg.User = await rep.FindUser(msg.UserId);
                }
                return View(messages);
            }
            return View("Error"); // todo : change error view to page not found 
        }

        //[Authorize]
        //public async Task<IActionResult> CreateRoom(RoomModel model)
        //{
        //    DBManager.createRoom(model.Title, );
        //}
    }
}
