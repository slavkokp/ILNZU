using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Models;

namespace BLL.Services
{
    public class UserRepository
    {
        public async Task<User> FindUser(int id)
        {
            return await DBRepository.FindUser(id);
        }

        public async Task<User> FindUser(string email)
        {
            return await DBRepository.FindUser(email);
        }

        public async Task<User> FindUser(string email, string saltedPassword)
        {
            return await DBRepository.FindUser(email, saltedPassword);
        }

        public async Task<bool> CheckIfUserIsMemberOfMeetingRoom(int userId, int meetingRoomId)
        {
            return await DBRepository.CheckIfUserIsMemberOfMeetingRoom(userId, meetingRoomId);
        }

        public async Task<int> AddUser(string email, string password, string name, string surname, string username)
        {
            return await DBRepository.AddUser(email, password, name, surname, username);
        }
    }
}
