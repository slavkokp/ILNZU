using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ILNZU.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email not set")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password not set")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Wrong Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Username not set")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Name not set")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname not set")]
        public string Surname { get; set; }
    }
}
