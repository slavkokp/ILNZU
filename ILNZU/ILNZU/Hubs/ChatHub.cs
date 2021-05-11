﻿// <copyright file="ChatHub.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ILNZU
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using BLL.Services;
    using DAL.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using System.Security.Claims;

    /// <summary>
    /// ChatHub class.
    /// </summary>
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly MessageRepository msgRep;
        private readonly AttachmentRepository attachRep;
        private readonly IWebHostEnvironment appEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatHub"/> class.
        /// </summary>
        /// <param name="dbRepository">database repository.</param>
        public ChatHub(MessageRepository msgRep, AttachmentRepository attachRep, IWebHostEnvironment appEnvironment)
        {
            this.msgRep = msgRep;
            this.attachRep = attachRep;
            this.appEnvironment = appEnvironment;
        }

        public async Task SetGroup(int meetingRoomId)
        {
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, meetingRoomId.ToString());
        }

        /// <summary>
        /// Sends a message.
        /// </summary>
        /// <param name="message">User message.</param>
        /// <param name="meetingRoomId">Meeting room id.</param>
        /// <returns>A task.</returns>
        public async Task Send(Message message, int meetingRoomId, int? attachmentId)
        {
            message.AttachmentId = attachmentId;
            message.DateTime = DateTime.Now;
            message.MeetingRoomId = meetingRoomId;
            message.UserId = Convert.ToInt32(this.Context.UserIdentifier);
            await this.msgRep.CreateMessage(message);
            await this.Clients.Group(meetingRoomId.ToString()).SendAsync("Receive", message, this.Context.User.Identity.Name);
        }
    }
}
