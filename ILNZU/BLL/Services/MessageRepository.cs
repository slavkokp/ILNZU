// <copyright file="MessageRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BLL.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DAL;
    using DAL.Models;

    /// <summary>
    /// Class for managing messages.
    /// </summary>
    public class MessageRepository
    {
        /// <summary>
        /// Creates a message in DBRepository.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <returns>Created messages.</returns>
        public async Task CreateMessage(Message message)
        {
            await DBRepository.CreateMessage(message);
        }

        /// <summary>
        /// Gets messages from a meeting room.
        /// </summary>
        /// <param name="meetingRoomId">Meeting room id.</param>
        /// <returns>Messages.</returns>
        public async Task<List<Message>> GetMessages(int meetingRoomId)
        {
            return await DBRepository.GetMessages(meetingRoomId);
        }
    }
}
