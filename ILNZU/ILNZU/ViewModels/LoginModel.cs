// <copyright file="LoginModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ILNZU.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Login model.
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Gets or sets email.
        /// </summary>
        [Required(ErrorMessage = "Email not set")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets password.
        /// </summary>
        [Required(ErrorMessage = "Password not set")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
