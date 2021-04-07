using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Models;
using BLL;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class DBRepository
    {
        public async Task<User> findUser(int id)
        {
            using (var db = new ILNZU_dbContext())
            {
                return await db.User.FirstOrDefaultAsync(u => u.Id == id);
            }
        }

        public async Task<User> findUser(string email)
        {
            using (var db = new ILNZU_dbContext())
            {
                return await db.User.FirstOrDefaultAsync(u => u.Email == email);
            }
        }

        public async Task<User> findUser(string email, string SaltedPassword)
        {
            using (var db = new ILNZU_dbContext())
            {
                string hash = PasswordHash.hashPassword(SaltedPassword);
                return await db.User.FirstOrDefaultAsync(u => u.Email == email && u.Password == hash);
            }
        }

        public async Task<int> addUser(string email, string password, string name, string surname, string username)
        {

            string salt = PasswordHash.GetSalt();
            string hash = PasswordHash.hashPassword(password + salt);
            using (var db = new ILNZU_dbContext())
            {
                User u = new User { Email = email, Password = hash, Name = name, ProfilePicture = 0, Surname = surname, Username = username, Salt = salt };
                db.User.Add(u);

                await db.SaveChangesAsync();

                return u.Id;
            }
        }

        public async void createRoom(string title, int userId)
        {
            using (var db = new ILNZU_dbContext())
            {
                MeetingRoom room = new MeetingRoom { Title = title, UserId = userId };
                db.Add(room);
                await db.SaveChangesAsync();
            }
        }

        public async void createMessage(Message message)
        {
            using (var db = new ILNZU_dbContext())
            {
                db.Add(message);
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<Message>> getMessages(int id)
        {
            using (var db = new ILNZU_dbContext())
            {
                return await Task.Run(() => (from mes in db.Message
                                             where mes.MeetingRoomId == id
                                             select mes).ToList());
            }
        }

        public async Task<List<int>> getMeetings(int id)
        {
            using (var db = new ILNZU_dbContext())
            {
                return await Task.Run(() => (from pairs in db.UserMemberOfMeetingRoom
                                             where pairs.UserId == id
                                             select pairs.MeetingRoomId).ToList());
            }
        }

        public async Task<List<int>> getUsers(int meetingRoomId)
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
