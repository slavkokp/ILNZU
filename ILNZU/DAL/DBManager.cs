using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class DBManager
    {
        private ILNZU_dbContext db;

        public DBManager(ILNZU_dbContext dbContext)
        {
            db = dbContext;
        }

        public List<int> getUsers(int meetingRoomId)
        {
            return (from pairs in db.UserMemberOfMeetingRoom
                    where pairs.MeetingRoomId == meetingRoomId
                    select pairs.UserId).ToList();
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
    }
}
