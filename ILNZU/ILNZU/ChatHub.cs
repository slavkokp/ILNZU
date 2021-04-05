using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ILNZU
{
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task Send(string message, int meetingRoomId)
        {
            //await Clients.All.SendAsync("Receive", message, Context.User.Identity.Name);
            Console.WriteLine(Context.UserIdentifier);
            await Clients.User(Context.UserIdentifier).SendAsync("Recieve", message, Context.User.Identity.Name);
        }
    }
}
