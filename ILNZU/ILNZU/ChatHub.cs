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
            var userIds = await this.dbRepository.GetUsers(meetingRoomId);
            await this.Clients.Users(userIds.ConvertAll(x => x.ToString())).SendAsync("Receive", message, this.Context.User.Identity.Name, meetingRoomId);
        }

        // public override async Task OnConnectedAsync()
        // {
        //    foreach (Message message in dbRepository.getMessages())
        //    {

        // }
        //    await Clients.Caller.SendAsync("Receive", message, Context.User.Identity.Name);
        //    await base.OnConnectedAsync();
        // }
    }
}
