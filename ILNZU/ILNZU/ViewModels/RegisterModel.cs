// <copyright file="RegisterModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ILNZU.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Register model.
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// Gets or sets email.
        /// </summary>
        [Required(ErrorMessage = "Email not set")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets password.
        /// </summary>
        [Required(ErrorMessage = "Password not set")]
        [MinLength(3, ErrorMessage = "The password must contain at least 3 symbols")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets ConfirmPassword.
        /// </summary>
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Wrong Password")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Gets or sets username.
        /// </summary>
        [Required(ErrorMessage = "Username not set")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        [Required(ErrorMessage = "Name not set")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets surname.
        /// </summary>
        [Required(ErrorMessage = "Surname not set")]
        public string Surname { get; set; }
    }
}
