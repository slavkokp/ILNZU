﻿using System;
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
        private DBRepository dbManager;

        public ChatHub(DBRepository dbManager)
        {
            this.dbManager = dbManager;
        }

        public async Task Send(string message, int meetingRoomId)
        {
            await Clients.User(Context.UserIdentifier).SendAsync("Receive", message, Context.User.Identity.Name);
        }
    }
}
