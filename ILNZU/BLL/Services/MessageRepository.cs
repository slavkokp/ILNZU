using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Models;

namespace BLL.Services
{
    public class MessageRepository
    {
        public async Task CreateMessage(Message message)
        {
            await DBRepository.CreateMessage(message);
        }

        public async Task<List<Message>> GetMessages(int meetingRoomId)
        {
            return await DBRepository.GetMessages(meetingRoomId);
        }
    }
}
