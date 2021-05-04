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
        public async Task<MeetingRoom> CreateRoom(string title, int userId)
        {
            return await DBRepository.CreateRoom(title, userId);
        }

        public async Task DeleteRoom(int id)
        {
            await DBRepository.DeleteRoom(id);
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

        public async Task<bool> CheckIfUserIsCreatorOfMeetingRoom(int userId, int meetingRoomId)
        {
            return await DBRepository.CheckIfUserIsCreatorOfMeetingRoom(userId, meetingRoomId);
        }

        public async Task AddUserToMeeting(int userId, int meetingRoomId)
        {
            await DBRepository.AddUserToMeeting(userId, meetingRoomId);
        }
        public async Task RemoveUserFromMeeting(int userId, int meetingRoomId)
        {
            await DBRepository.RemoveUserFromMeeting(userId, meetingRoomId);
        }

    }
}
