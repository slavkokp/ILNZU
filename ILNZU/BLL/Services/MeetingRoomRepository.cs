using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Models;

namespace BLL.Services
{
    public class MeetingRoomRepository
    {
        public async Task CreateRoom(string title, int userId)
        {
            await DBRepository.CreateRoom(title, userId);
        }

        public async Task<List<int>> GetMeetings(int userId)
        {
            return await DBRepository.GetMeetings(userId);
        }

        public async Task<bool> CheckIfUserIsMemberOfMeetingRoom(int userId, int meetingRoomId)
        {
            return await DBRepository.CheckIfUserIsMemberOfMeetingRoom(userId, meetingRoomId);
        }

        public async Task<List<Tuple<int, string>>> GetMeetingRooms(int userId)
        {
            return await DBRepository.GetMeetingRooms(userId);
        }
    }
}
