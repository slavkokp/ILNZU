// <copyright file="CustomUserIdProvider.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ILNZU.Services
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.SignalR;

    /// <summary>
    /// Custom user id provider.
    /// </summary>
    public class CustomUserIdProvider : IUserIdProvider
    {
        /// <summary>
        /// Gets user id.
        /// </summary>
        /// <param name="connection">Connection context.</param>
        /// <returns>User id.</returns>
        public virtual string GetUserId(HubConnectionContext connection)
        {
            return connection.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}