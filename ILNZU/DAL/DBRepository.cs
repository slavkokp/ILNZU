// <copyright file="DBRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BLL;
    using DAL.Data;
    using DAL.Models;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// A class for DBRepository.
    /// </summary>
    public class DBRepository
    {
        /// <summary>
        /// Finds user by id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>Found user.</returns>
        public async Task<User> FindUser(int id)
        {
            using (var db = new ILNZU_dbContext())
            {
                return await db.User.FirstOrDefaultAsync(u => u.Id == id);
            }
        }

        /// <summary>
        /// Finds user by email.
        /// </summary>
        /// <param name="email">User email.</param>
        /// <returns>Found user.</returns>
        public async Task<User> FindUser(string email)
        {
            using (var db = new ILNZU_dbContext())
            {
                return await db.User.FirstOrDefaultAsync(u => u.Email == email);
            }
        }

        /// <summary>
        /// Finds user by email and salted password.
        /// </summary>
        /// <param name="email">User email.</param>
        /// <param name="saltedPassword">User salted password.</param>
        /// <returns>Found user.</returns>
        public async Task<User> FindUser(string email, string saltedPassword)
        {
            using (var db = new ILNZU_dbContext())
            {
                string hash = PasswordHash.HashPassword(saltedPassword);
                return await db.User.FirstOrDefaultAsync(u => u.Email == email && u.Password == hash);
            }
        }

        /// <summary>
        /// Cheaks if user is the member of a meeting room.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <param name="meetingRoomId">Meeting room id.</param>
        /// <returns>Boolean which represents if user is a member of a meeting room.</returns>
        public async Task<bool> CheckIfUserIsMemberOfMeetingRoom(int userId, int meetingRoomId)
        {
            using (var db = new ILNZU_dbContext())
            {
                UserMemberOfMeetingRoom member = await db.UserMemberOfMeetingRoom.FirstOrDefaultAsync(u => u.UserId == userId && u.MeetingRoomId == meetingRoomId);
                if (member == null)
                {
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// Adds a user to the database.
        /// </summary>
        /// <param name="email">User email.</param>
        /// <param name="password">User password.</param>
        /// <param name="name">User name.</param>
        /// <param name="surname">User surname.</param>
        /// <param name="username">User nickname.</param>
        /// <returns>User id.</returns>
        public async Task<int> AddUser(string email, string password, string name, string surname, string username)
        {
            string salt = PasswordHash.GetSalt();
            string hash = PasswordHash.HashPassword(password + salt);
            using (var db = new ILNZU_dbContext())
            {
                User u = new User { Email = email, Password = hash, Name = name, ProfilePicture = 0, Surname = surname, Username = username, Salt = salt };
                db.User.Add(u);

                await db.SaveChangesAsync();

                return u.Id;
            }
        }

        /// <summary>
        /// Creates a meeting room in database.
        /// </summary>
        /// <param name="title">Room title.</param>
        /// <param name="userId">Creator id.</param>
        public async void CreateRoom(string title, int userId)
        {
            using (var db = new ILNZU_dbContext())
            {
                MeetingRoom room = new MeetingRoom { Title = title, UserId = userId };
                db.Add(room);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Creates a message in database.
        /// </summary>
        /// <param name="message">A message.</param>
        public async void CreateMessage(Message message)
        {
            using (var db = new ILNZU_dbContext())
            {
                db.Add(message);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Gets messages from the database.
        /// </summary>
        /// <param name="id">Database id.</param>
        /// <returns>List of messages.</returns>
        public async Task<List<Message>> GetMessages(int id)
        {
            using (var db = new ILNZU_dbContext())
            {
                return await Task.Run(() => (from mes in db.Message
                                             where mes.MeetingRoomId == id
                                             select mes).ToList());
            }
        }

        /// <summary>
        /// Gets users meetings.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>List of meetings ids.</returns>
        public async Task<List<int>> GetMeetings(int id)
        {
            using (var db = new ILNZU_dbContext())
            {
                return await Task.Run(() => (from pairs in db.UserMemberOfMeetingRoom
                                             where pairs.UserId == id
                                             select pairs.MeetingRoomId).ToList());
            }
        }

        /// <summary>
        /// Gets users in meeting room.
        /// </summary>
        /// <param name="meetingRoomId">Meeting room id.</param>
        /// <returns>List of users ids.</returns>
        public async Task<List<int>> GetUsers(int meetingRoomId)
        {
            using (var db = new ILNZU_dbContext())
            {
                return await Task.Run(() => (from pairs in db.UserMemberOfMeetingRoom
                                             where pairs.MeetingRoomId == meetingRoomId
                                             select pairs.UserId).ToList());
            }
        }

        /// <summary>
        /// Get list of meeting rooms available for user with userid.
        /// </summary>
        /// <param name="userId">User ID.</param>
        /// <returns>List of ints.</returns>
        public async Task<List<Tuple<int, string>>> GetMeetingRooms(int userId)
        {
            using (var db = new ILNZU_dbContext())
            {
                return await Task.Run(() => (from pairs in db.UserMemberOfMeetingRoom
                                             join meetings in db.MeetingRoom on pairs.MeetingRoomId equals meetings.MeetingRoomId
                                             where pairs.UserId == userId
                                             select new Tuple<int, string>(meetings.MeetingRoomId, meetings.Title)).ToList());
            }
        }
    }
}
