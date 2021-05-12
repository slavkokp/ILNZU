// <copyright file="MeetingRoomRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DAL;
    using DAL.Models;

    /// <summary>
    /// Meeting room repository managing class.
    /// </summary>
    public class MeetingRoomRepository
    {
        /// <summary>
        /// Gets the meeting title.
        /// </summary>
        /// <param name="meetingId">Meeting id.</param>
        /// <returns>Meeting title.</returns>
        public async Task<string> GetMeetingTitle(int meetingId)
        {
            return await DBRepository.GetMeetingTitle(meetingId);
        }

        /// <summary>
        /// Creates a meeting room.
        /// </summary>
        /// <param name="title">Room title.</param>
        /// <param name="userId">Id of user creator.</param>
        /// <returns>The room created in database repository.</returns>
        public async Task<MeetingRoom> CreateRoom(string title, int userId)
        {
            return await DBRepository.CreateRoom(title, userId);
        }

        /// <summary>
        /// Deletes a meeting room.
        /// </summary>
        /// <param name="id">Room id.</param>
        /// <returns>Nothing.</returns>
        public async Task DeleteRoom(int id)
        {
            await DBRepository.DeleteRoom(id);
        }

        /// <summary>
        /// Gets the meetiongs from DBrepository.
        /// </summary>
        /// <param name="userId">Users id.</param>
        /// <returns>Meetings where user is.</returns>
        public async Task<List<int>> GetMeetings(int userId)
        {
            return await DBRepository.GetMeetings(userId);
        }

        /// <summary>
        /// Checks if user is member of meeting room.
        /// </summary>
        /// <param name="userId">Users id.</param>
        /// <param name="meetingRoomId">Meeting room id.</param>
        /// <returns>If user is member of meeting room.</returns>
        public async Task<bool> CheckIfUserIsMemberOfMeetingRoom(int userId, int meetingRoomId)
        {
            return await DBRepository.CheckIfUserIsMemberOfMeetingRoom(userId, meetingRoomId);
        }

        /// <summary>
        /// Gets meeting rooms.
        /// </summary>
        /// <param name="userId">Users id.</param>
        /// <returns>Meeting rooms.</returns>
        public async Task<List<Tuple<int, string>>> GetMeetingRooms(int userId)
        {
            return await DBRepository.GetMeetingRooms(userId);
        }

        /// <summary>
        /// Checks if user is creator of meeting room.
        /// </summary>
        /// <param name="userId">Users id.</param>
        /// <param name="meetingRoomId">Meeting room id.</param>
        /// <returns>If user is creator of meeting room.</returns>
        public async Task<bool> CheckIfUserIsCreatorOfMeetingRoom(int userId, int meetingRoomId)
        {
            return await DBRepository.CheckIfUserIsCreatorOfMeetingRoom(userId, meetingRoomId);
        }

        /// <summary>
        /// Adds a user to meeting.
        /// </summary>
        /// <param name="userId">Users id.</param>
        /// <param name="meetingRoomId">Meeting room id.</param>
        /// <returns>The user added.</returns>
        public async Task AddUserToMeeting(int userId, int meetingRoomId)
        {
            await DBRepository.AddUserToMeeting(userId, meetingRoomId);
        }

        /// <summary>
        /// Removes user from meeting.
        /// </summary>
        /// <param name="userId">Users id.</param>
        /// <param name="meetingRoomId">Meeting room id.</param>
        /// <returns>The user removed.</returns>
        public async Task RemoveUserFromMeeting(int userId, int meetingRoomId)
        {
            await DBRepository.RemoveUserFromMeeting(userId, meetingRoomId);
        }
    }
}
