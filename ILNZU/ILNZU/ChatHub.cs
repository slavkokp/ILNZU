using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using DAL;
using DAL.Data;
using DAL.Models;

namespace ILNZU
{
    [Authorize]
    public class ChatHub : Hub
    {
        private DBRepository dbRepository;

        public ChatHub(DBRepository dbRepository)
        {
            this.dbRepository = dbRepository;
        }

        public async Task Send(Message message, int meetingRoomId)
        {
            message.DateTime = DateTime.Now;
            message.MeetingRoomId = meetingRoomId;
            message.UserId = Convert.ToInt32(Context.UserIdentifier);
            dbRepository.createMessage(message);
            var userIds = await dbRepository.getUsers(meetingRoomId);
            
            await Clients.Users(userIds.ConvertAll(x => x.ToString())).SendAsync("Receive", message, Context.User.Identity.Name);
        }
    }
}
