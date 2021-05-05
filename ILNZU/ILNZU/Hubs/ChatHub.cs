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
    using Microsoft.AspNetCore.SignalR;

    /// <summary>
    /// ChatHub class.
    /// </summary>
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly MessageRepository rep;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatHub"/> class.
        /// </summary>
        /// <param name="rep">Message repository.</param>
        public ChatHub(MessageRepository rep)
        {
            this.rep = rep ?? throw new ArgumentNullException(nameof(rep));
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
        /// <returns>A task.</returns>
        public async Task Send(Message message, int meetingRoomId)
        {
            message.DateTime = DateTime.Now;
            message.MeetingRoomId = meetingRoomId;
            message.UserId = Convert.ToInt32(this.Context.UserIdentifier);
            await this.rep.CreateMessage(message);
            await this.Clients.Group(meetingRoomId.ToString()).SendAsync("Receive", message, this.Context.User.Identity.Name, meetingRoomId);

            // await this.Clients.Users(userIds.ConvertAll(x => x.ToString())).SendAsync("Receive", message, this.Context.User.Identity.Name, meetingRoomId);
        }
    }
}
