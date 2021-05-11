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
        private readonly MessageRepository messageRep;
        private readonly AttachmentRepository attachRep;
        private readonly IWebHostEnvironment appEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingController"/> class.
        /// </summary>
        /// <param name="userRep">UserRepository service.</param>
        /// <param name="messageRep">MessageRepository service.</param>
        public MeetingController(UserRepository userRep, MessageRepository messageRep, AttachmentRepository attachRep, IWebHostEnvironment appEnvironment)
        {
            this.userRep = userRep;
            this.messageRep = messageRep;
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
                return this.View(messages);
            }

            return this.View("Error"); // todo : change error view to page not found
        }

        //[Authorize]
        //[HttpPost]
        //public async Task<OkObjectResult> CreateAttachment(IFormFile file)
        //{
        //    Attachment attachment = new Attachment();
        //    attachment.FileName = file.FileName;
        //    attachment.Path = Path.DirectorySeparatorChar.ToString() + Path.Combine("Files", DateTime.Now.ToString(@"hh\_mm\_ss") + file.FileName);
        //    using (var fileStream = new FileStream(this.appEnvironment.WebRootPath + attachment.Path, FileMode.Create))
        //    {
        //        file.CopyTo(fileStream);
        //    }

        //    int id = await this.attachRep.AddAttachment(attachment);
        //    this.ViewBag.AttachmentId = id;
        //    return this.Ok(id);
        //}

        //[Authorize]
        //public async Task<IActionResult> Download()
        //{
        //    //if (filename == null)
        //    //{
        //    //    return Content("filename not present");
        //    //}
        //    string filename = "07_52_00osuGif.gif";
        //    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", filename);

        //    var memory = new MemoryStream();
        //    using (var stream = new FileStream(path, FileMode.Open))
        //    {
        //        await stream.CopyToAsync(memory);
        //    }

        //    memory.Position = 0;
        //    return this.File(memory, MimeTypes.GetMimeType(filename), filename);
        //}

        // [Authorize]
        // public async Task<IActionResult> CreateRoom(RoomModel model)
        // {
        //    DBManager.createRoom(model.Title, );
        // }
    }
}
