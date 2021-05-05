// <copyright file="Invite.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DAL.Models
{
    using System;

    /// <summary>
    /// A class to represent invite model.
    /// </summary>
    public class Invite
    {
        /// <summary>
        /// Gets or sets InviteId.
        /// </summary>
        public int InviteId { get; set; }

        /// <summary>
        /// Gets or sets UserId.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets DateTime.
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Gets or sets MeetingRoomId.
        /// </summary>
        public int MeetingRoomId { get; set; }

        /// <summary>
        /// Gets or sets MeeringRoom.
        /// </summary>
        public MeetingRoom MeetingRoom { get; set; }
    }
}
