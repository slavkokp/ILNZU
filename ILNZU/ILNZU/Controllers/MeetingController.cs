// <copyright file="MeetingController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ILNZU.Controllers
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using BLL.Services;
    using DAL.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Hosting;

    /// <summary>
    /// MeetingController class.
    /// </summary>
    public class MeetingController : Controller
    {
        private readonly UserRepository userRep;
        private readonly MeetingRoomRepository meetingRoomRepository;
        private readonly MessageRepository messageRep;
        private readonly AttachmentRepository attachRep;
        private readonly IWebHostEnvironment appEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingController"/> class.
        /// </summary>
        /// <param name="userRep">UserRepository service.</param>
        /// <param name="messageRep">MessageRepository service.</param>
        public MeetingController(UserRepository userRep, MessageRepository messageRep, MeetingRoomRepository meetingRep, AttachmentRepository attachRep, IWebHostEnvironment appEnvironment)
        {
            this.userRep = userRep;
            this.messageRep = messageRep;
            this.meetingRoomRepository = meetingRep;
            this.appEnvironment = appEnvironment;
            this.attachRep = attachRep;
        }

        /// <summary>
        /// Meeting room page view or error view if no premission.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>View.</returns>
        [Authorize]
        public async Task<IActionResult> Room(int? id)
        {
            bool allowed = await this.userRep.CheckIfUserIsMemberOfMeetingRoom(Convert.ToInt32(this.User.FindFirst(ClaimTypes.NameIdentifier).Value), Convert.ToInt32(id));
            if (allowed)
            {
                this.ViewBag.MeetingRoomId = id;
                List<Message> messages = await this.messageRep.GetMessages(Convert.ToInt32(id));
                messages = messages.OrderBy(msg => msg.DateTime).ToList();
                Dictionary<int, Attachment> attachments = new Dictionary<int, Attachment>();
                foreach (var msg in messages)
                {
                    msg.User = await this.userRep.FindUser(msg.UserId);
                    if (msg.AttachmentId != null)
                    {
                        attachments.Add(msg.AttachmentId.Value, await this.attachRep.FindAttachment(msg.AttachmentId.Value));
                    }
                }

                ViewBag.attachments = attachments;
                this.ViewBag.MeetingRooms = await this.meetingRoomRepository.GetMeetingRooms(Convert.ToInt32(this.User.FindFirst(ClaimTypes.NameIdentifier).Value));
                return this.View(messages);
            }
            
            return this.View("Error"); // todo : change error view to page not found
        }

        // [Authorize]
        // public async Task<IActionResult> CreateRoom(RoomModel model)
        // {
        //    DBManager.createRoom(model.Title, );
        // }
    }
}
