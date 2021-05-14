// <copyright file="ChatHub.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ILNZU
{
    using System;
    using System.Threading.Tasks;
    using BLL.Services;
    using DAL.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.SignalR;

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
        /// <param name="msgRep">Message repository.</param>
        /// <param name="attachRep">Attachment repository.</param>
        /// <param name="appEnvironment">App environment.</param>
        public ChatHub(MessageRepository msgRep, AttachmentRepository attachRep, IWebHostEnvironment appEnvironment)
        {
            this.msgRep = msgRep;
            this.attachRep = attachRep;
            this.appEnvironment = appEnvironment;
        }

        /// <summary>
        /// Sets group.
        /// </summary>
        /// <param name="meetingRoomId">Meeting room id.</param>
        /// <returns>Nothing.</returns>
        public async Task SetGroup(int meetingRoomId)
        {
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, meetingRoomId.ToString());
        }

        /// <summary>
        /// Sends a message.
        /// </summary>
        /// <param name="message">User message.</param>
        /// <param name="meetingRoomId">Meeting room id.</param>
        /// <param name="attachmentId">Attachment id.</param>
        /// <returns>A task.</returns>
        public async Task Send(Message message, int meetingRoomId, int? attachmentId)
        {
            message.AttachmentId = attachmentId;
            message.DateTime = DateTime.Now;
            message.MeetingRoomId = meetingRoomId;
            message.UserId = Convert.ToInt32(this.Context.UserIdentifier);
            await this.msgRep.CreateMessage(message);
            if (attachmentId.HasValue)
            {
                var attachment = await this.attachRep.FindAttachment(attachmentId.Value);
                await this.Clients.Group(meetingRoomId.ToString()).SendAsync("Receive", message, this.Context.User.Identity.Name, attachment);
            }
            else
            {
                var attachment = new Attachment();
                attachment.Path = string.Empty;
                await this.Clients.Group(meetingRoomId.ToString()).SendAsync("Receive", message, this.Context.User.Identity.Name, attachment);
            }
        }
    }
}
