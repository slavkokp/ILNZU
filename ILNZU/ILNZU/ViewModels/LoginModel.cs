using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ILNZU.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email not set")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password not set")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
