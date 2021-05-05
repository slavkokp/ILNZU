﻿// <copyright file="RoomModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ILNZU.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Room model.
    /// </summary>
    public class RoomModel
    {
        /// <summary>
        /// Gets or sets title.
        /// </summary>
        [Required(ErrorMessage = "Title not set")]
        public string Title { get; set; }
    }
}
