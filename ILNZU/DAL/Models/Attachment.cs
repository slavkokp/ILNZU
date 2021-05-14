// <copyright file="Attachment.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DAL.Models
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Attachment class.
    /// </summary>
    public class Attachment
    {
        /// <summary>
        /// Gets or sets attachment id.
        /// </summary>
        public int AttachmentId { get; set; }

        /// <summary>
        /// Gets or sets path.
        /// </summary>
        [JsonPropertyName("path")]
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets file name.
        /// </summary>
        [JsonPropertyName("filename")]
        public string FileName { get; set; }
    }
}
