using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    class InviteRepository
    {
        public async Task<List<Invite>> GetInvites(int userId)
        {
            return await DBRepository.GetInvites(userId);
        }

        public async Task AddInvite(int userId, int meetingId)
        {
            await DBRepository.AddInvite(userId, meetingId);
        }

        public async Task RemoveInvite(int userId, int meetingId)
        {
            await DBRepository.RemoveInvite(userId, meetingId);
        }

    }
}
