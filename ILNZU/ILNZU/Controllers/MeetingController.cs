using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ILNZU.ViewModels;
using DAL;
using DAL.Models;

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
            if (true) // todo : add check if user is a member of meeting room with id
            {
                ViewBag.MeetingRoomId = id;
                List<Message> messages = await rep.getMessages(Convert.ToInt32(id));
                messages = messages.OrderByDescending(msg => msg.DateTime).ToList();
                foreach (var msg in messages)
                {
                    msg.User = await rep.findUser(msg.UserId);
                }
                return View(messages);
            }
            // return Access Denied view
        }

        //[Authorize]
        //public async Task<IActionResult> CreateRoom(RoomModel model)
        //{
        //    DBManager.createRoom(model.Title, );
        //}
    }
}
