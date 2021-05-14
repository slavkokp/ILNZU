// <copyright file="Message.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DAL.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;

    /// <summary>
    /// A class to represent message model.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Gets or sets MessageId.
        /// </summary>
        public int MessageId { get; set; }

        /// <summary>
        /// Gets or sets DateTime.
        /// </summary>
        [JsonPropertyName("datetime")]
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Gets or sets Text.
        /// </summary>
        [Required]
        [JsonPropertyName("text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets UserId.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets User.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets MeetingRoomId.
        /// </summary>
        public int MeetingRoomId { get; set; }

        /// <summary>
        /// Gets or sets MeetingRoom.
        /// </summary>
        public MeetingRoom MeetingRoom { get; set; }

        /// <summary>
        /// Gets or sets attachment id.
        /// </summary>
        public int? AttachmentId { get; set; }

        /// <summary>
        /// Gets or sets attachment.
        /// </summary>
        public virtual Attachment Attachment { get; set; }
    }
}
