// <copyright file="DBRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace DAL
{
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
        public async Task<User> FindUser(int id)
        {
            using (var db = new ILNZU_dbContext())
            {
                return await db.User.FirstOrDefaultAsync(u => u.Id == id);
            }
        }

        public async Task<User> FindUser(string email)
        {
            using (var db = new ILNZU_dbContext())
            {
                return await db.User.FirstOrDefaultAsync(u => u.Email == email);
            }
        }

        public async Task<User> FindUser(string email, string SaltedPassword)
        {
            using (var db = new ILNZU_dbContext())
            {
                string hash = PasswordHash.HashPassword(SaltedPassword);
                return await db.User.FirstOrDefaultAsync(u => u.Email == email && u.Password == hash);
            }
        }

        public async Task<bool> CheckIfUserIsMemberOfMeetingRoom(int userId, int meetingRoomId)
        {
            using (var db = new ILNZU_dbContext())
            {
                UserMemberOfMeetingRoom member = await db.UserMemberOfMeetingRoom.FirstOrDefaultAsync(u => u.UserId == userId && u.MeetingRoomId == meetingRoomId);
                if(member == null)
                {
                    return false;
                }
                return true;
            }
        }

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

        public async void CreateRoom(string title, int userId)
        {
            using (var db = new ILNZU_dbContext())
            {
                MeetingRoom room = new MeetingRoom { Title = title, UserId = userId };
                db.Add(room);
                await db.SaveChangesAsync();
            }
        }

        public async void CreateMessage(Message message)
        {
            using (var db = new ILNZU_dbContext())
            {
                db.Add(message);
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<Message>> GetMessages(int id)
        {
            using (var db = new ILNZU_dbContext())
            {
                return await Task.Run(() => (from mes in db.Message
                                             where mes.MeetingRoomId == id
                                             select mes).ToList());
            }
        }

        public async Task<List<int>> GetMeetings(int id)
        {
            using (var db = new ILNZU_dbContext())
            {
                return await Task.Run(() => (from pairs in db.UserMemberOfMeetingRoom
                                             where pairs.UserId == id
                                             select pairs.MeetingRoomId).ToList());
            }
        }

        public async Task<List<int>> GetUsers(int meetingRoomId)
        {
            using (var db = new ILNZU_dbContext())
            {
                return await Task.Run(() => (from pairs in db.UserMemberOfMeetingRoom
                                             where pairs.MeetingRoomId == meetingRoomId
                                             select pairs.UserId).ToList());
            }
            
        }
    }
}
