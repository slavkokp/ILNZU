// <copyright file="ErrorViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ILNZU.Models
{
    /// <summary>
    /// Error view model.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Gets or sets Requet id..
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Gets a value indicating whether the request id is not null, or empty.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(this.RequestId);
    }
}
