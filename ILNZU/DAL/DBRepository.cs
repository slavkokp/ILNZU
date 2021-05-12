// <copyright file="DBRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DAL.Data;
    using DAL.Models;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// A class for DBRepository.
    /// </summary>
    public static class DBRepository
    {
        /// <summary>
        /// Finds user by id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>Found user.</returns>
        public static async Task<User> FindUser(int id)
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
        public static async Task<User> FindUser(string email)
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
        public static async Task<User> FindUser(string email, string saltedPassword)
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
        public static async Task<bool> CheckIfUserIsMemberOfMeetingRoom(int userId, int meetingRoomId)
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
        public static async Task<int> AddUser(string email, string password, string name, string surname, string username)
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
        /// <returns>Nothing.</returns>
        public static async Task<MeetingRoom> CreateRoom(string title, int userId)
        {
            using (var db = new ILNZU_dbContext())
            {
                MeetingRoom room = new MeetingRoom { Title = title, UserId = userId };
                db.Add(room);
                await db.SaveChangesAsync();
                return room;
            }
        }

        /// <summary>
        /// Deletes room.
        /// </summary>
        /// <param name="meetingId">Meeting id.</param>
        /// <returns>Nothing.</returns>
        public static async Task DeleteRoom(int meetingId)
        {
            using (var db = new ILNZU_dbContext())
            {
                var room = await db.MeetingRoom.FirstOrDefaultAsync(u => u.MeetingRoomId == meetingId);
                if (room != null)
                {
                    db.Remove(room);
                    await db.SaveChangesAsync();
                }
            }
        }

        /// <summary>
        /// Checks if user is creator of meeting room.
        /// </summary>
        /// <param name="userId">Users id.</param>
        /// <param name="meetingId">Meeting id.</param>
        /// <returns>If user is creator of meeting room.</returns>
        public static async Task<bool> CheckIfUserIsCreatorOfMeetingRoom(int userId, int meetingId)
        {
            using (var db = new ILNZU_dbContext())
            {
                var chat = await db.MeetingRoom.FirstOrDefaultAsync(u => u.MeetingRoomId == meetingId);
                if (chat.UserId == userId)
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Adds a user to meeting.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <param name="meetingId">Meeting id.</param>
        /// <returns>Nothing.</returns>
        public static async Task AddUserToMeeting(int userId, int meetingId)
        {
            using (var db = new ILNZU_dbContext())
            {
                UserMemberOfMeetingRoom relation = new UserMemberOfMeetingRoom { MeetingRoomId = meetingId, UserId = userId };
                db.Add(relation);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// REmoves user froom meeting room.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <param name="meetingId">Meeting id.</param>
        /// <returns>Nothing.</returns>
        public static async Task RemoveUserFromMeeting(int userId, int meetingId)
        {
            using (var db = new ILNZU_dbContext())
            {
                var relation = await db.UserMemberOfMeetingRoom.FirstOrDefaultAsync(u => u.MeetingRoomId == meetingId && u.UserId == userId);
                if (relation != null)
                {
                    db.Remove(relation);
                    await db.SaveChangesAsync();
                }
            }
        }

        /// <summary>
        /// Creates a message in database.
        /// </summary>
        /// <param name="message">A message.</param>
        /// <returns>Nothing.</returns>
        public static async Task CreateMessage(Message message)
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
        public static async Task<List<Message>> GetMessages(int id)
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
        public static async Task<List<int>> GetMeetings(int id)
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
        public static async Task<List<int>> GetUsers(int meetingRoomId)
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
        public static async Task<List<Tuple<int, string>>> GetMeetingRooms(int userId)
        {
            using (var db = new ILNZU_dbContext())
            {
                return await Task.Run(() => (from pairs in db.UserMemberOfMeetingRoom
                                             join meetings in db.MeetingRoom on pairs.MeetingRoomId equals meetings.MeetingRoomId
                                             where pairs.UserId == userId
                                             select new Tuple<int, string>(meetings.MeetingRoomId, meetings.Title)).ToList());
            }
        }

        /// <summary>
        /// Gets the meeting title.
        /// </summary>
        /// <param name="meetingId">Meeting id.</param>
        /// <returns>Meeting room title.</returns>
        public static async Task<string> GetMeetingTitle(int meetingId)
        {
            using (var db = new ILNZU_dbContext())
            {
                MeetingRoom room = await db.MeetingRoom.FirstOrDefaultAsync(m => m.MeetingRoomId == meetingId);
                return room.Title;
            }
        }

        /// <summary>
        /// Removes the invite.
        /// </summary>
        /// <param name="inviteId">invite id.</param>
        /// <returns>Nothing.</returns>
        public static async Task RemoveInvite(int inviteId)
        {
            using (var db = new ILNZU_dbContext())
            {
                var invite = await db.Invite.FirstOrDefaultAsync(i => i.InviteId == inviteId);
                if (invite != null)
                {
                    db.Remove(invite);
                    await db.SaveChangesAsync();
                }
            }
        }

        /// <summary>
        /// Removes the invite.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <param name="meetingId">Meeting id.</param>
        /// <returns>Nothing.</returns>
        public static async Task RemoveInvite(int userId, int meetingId)
        {
            using (var db = new ILNZU_dbContext())
            {
                var invite = await db.Invite.FirstOrDefaultAsync(i => i.UserId == userId && i.MeetingRoomId == meetingId);
                if (invite != null)
                {
                    db.Remove(invite);
                    await db.SaveChangesAsync();
                }
            }
        }

        /// <summary>
        /// Adds an invite.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <param name="meetingId">Meeting id.</param>
        /// <returns>Nothing.</returns>
        public static async Task AddInvite(int userId, int meetingId)
        {
            Invite i = new Invite { UserId = userId, MeetingRoomId = meetingId, DateTime = DateTime.Now };
            using (var db = new ILNZU_dbContext())
            {
                db.Add(i);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Gets invites.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns>List of invites.</returns>
        public static async Task<List<Invite>> GetInvites(int userId)
        {
            using (var db = new ILNZU_dbContext())
            {
                return await Task.Run(() => (from invite in db.Invite
                                             where invite.UserId == userId
                                             select invite).ToList());
            }
        }

        public static async Task<Invite> FindInvite(int inviteId)
        {
            using (var db = new ILNZU_dbContext())
            {
                return await db.Invite.FirstOrDefaultAsync(i => i.InviteId == inviteId);
            }
        }

        public static async Task<int> AddAttachment(Attachment attachment)
        {
            using (var db = new ILNZU_dbContext())
            {
                db.Attachment.Add(attachment);
                await db.SaveChangesAsync();
                return attachment.AttachmentId;
            }
        }

        public static async Task<Attachment> FindAttachment(int attachmentId)
        {
            using (var db = new ILNZU_dbContext())
            {
                return await db.Attachment.FirstOrDefaultAsync(a => a.AttachmentId == attachmentId);
            }
        }
    }
}
