using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class DBRepository
    {
        private readonly ILNZU_dbContext db;
        public DBRepository(ILNZU_dbContext db)
        {
            this.db = db;
        }

        public async Task<User> findUser(string email)
        {
            return await db.User.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> findUser(string email, string SaltedPassword)
        {
            string hash = PasswordHash.hashPassword(SaltedPassword);
            return await db.User.FirstOrDefaultAsync(u => u.Email == email && u.Password == hash);
        }

        public async Task<int> addUser(string email, string password, string name, string surname, string username)
        {
            string salt = PasswordHash.GetSalt();
            string hash = PasswordHash.hashPassword(password + salt);

            User u = new User { Email = email, Password = hash, Name = name, ProfilePicture = 0, Surname = surname, Username = username, Salt = salt };
            db.User.Add(u);

            await db.SaveChangesAsync();

            return u.Id;
        }

        public async void createRoom(string title, int userId)
        {
            MeetingRoom room = new MeetingRoom { Title = title, UserId = userId};
            db.Add(room);
            await db.SaveChangesAsync();
        }

        public async Task<List<Message>> getMessages(int id)
        {
            return await Task.Run(() => (from mes in db.Message
                    where mes.MeetingRoomId == id
                    select mes).ToList());
        }

        public async Task<List<int>> getMeetings(int id)
        {
            return await Task.Run(() => (from pairs in db.UserMemberOfMeetingRoom
                    where pairs.UserId == id
                    select pairs.MeetingRoomId).ToList());
        }

        public async Task<List<int>> getUsers(int meetingRoomId)
        {
            return await Task.Run(() => (from pairs in db.UserMemberOfMeetingRoom
                                         where pairs.MeetingRoomId == meetingRoomId
                                         select pairs.UserId).ToList());
        }
    }
}
