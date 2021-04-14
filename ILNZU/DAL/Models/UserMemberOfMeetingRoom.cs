// <copyright file="UserMemberOfMeetingRoom.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DAL.Models
{
    /// <summary>
    /// A class to represent invite model.
    /// </summary>
    public class UserMemberOfMeetingRoom
    {
        /// <summary>
        /// Gets or sets UserId.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets User.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets meeting room id.
        /// </summary>
        public int MeetingRoomId { get; set; }

        /// <summary>
        /// Gets or sets meeting room.
        /// </summary>
        public MeetingRoom MeetingRoom { get; set; }
    }
}
