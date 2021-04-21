// <copyright file="ChatHub.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ILNZU
{
    using System;
    using System.Threading.Tasks;
    using DAL;
    using DAL.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;

    /// <summary>
    /// ChatHub class.
    /// </summary>
    [Authorize]
    public class ChatHub : Hub
    {
        private DBRepository dbRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatHub"/> class.
        /// </summary>
        /// <param name="dbRepository">database repository.</param>
        public ChatHub(DBRepository dbRepository)
        {
            this.dbRepository = dbRepository;
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
        public async Task Send(Message message, int meetingRoomId)
        {
            message.DateTime = DateTime.Now;
            message.MeetingRoomId = meetingRoomId;
            message.UserId = Convert.ToInt32(this.Context.UserIdentifier);
            this.dbRepository.CreateMessage(message);
            await this.Clients.Group(meetingRoomId.ToString()).SendAsync("Receive", message, this.Context.User.Identity.Name, meetingRoomId);

            // await this.Clients.Users(userIds.ConvertAll(x => x.ToString())).SendAsync("Receive", message, this.Context.User.Identity.Name, meetingRoomId);
        }
    }
}
