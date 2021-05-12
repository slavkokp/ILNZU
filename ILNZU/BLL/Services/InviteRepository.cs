// <copyright file="InviteRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BLL.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DAL;
    using DAL.Models;

    /// <summary>
    /// Invite repositoru class.
    /// </summary>
    public class InviteRepository
    {
        /// <summary>
        /// Gets users invites.
        /// </summary>
        /// <param name="userId">Users id.</param>
        /// <returns>List of invites.</returns>
        public async Task<List<Invite>> GetInvites(int userId)
        {
            return await DBRepository.GetInvites(userId);
        }

        /// <summary>
        /// Adds an invite to a database.
        /// </summary>
        /// <param name="userId">Users id.</param>
        /// <param name="meetingId">Meeting id.</param>
        /// <returns>Nothing.</returns>
        public async Task AddInvite(int userId, int meetingId)
        {
            await DBRepository.AddInvite(userId, meetingId);
        }

        /// <summary>
        /// Removes the invite from the database.
        /// </summary>
        /// <param name="inviteId">Invite id.</param>
        /// <returns>Nothing.</returns>
        public async Task RemoveInvite(int inviteId)
        {
            await DBRepository.RemoveInvite(inviteId);
        }

        public async Task<Invite> FindInvite(int inviteId)
        {
            return await DBRepository.FindInvite(inviteId);
        }
    }
}
