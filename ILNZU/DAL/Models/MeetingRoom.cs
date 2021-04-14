// <copyright file="MeetingRoom.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DAL.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// A class to represent invite model.
    /// </summary>
    public class MeetingRoom
    {
        /// <summary>
        /// Gets or sets MeetingRoomId.
        /// </summary>
        public int MeetingRoomId { get; set; }

        /// <summary>
        /// Gets or sets Title.
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets UserId.
        /// </summary>
        public int UserId { get; set; } // Creator of the room

        /// <summary>
        /// Gets or sets User.
        /// </summary>
        public User User { get; set; }
    }
}
