using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ILNZU.ViewModels
{
    public class RoomModel
    {
        [Required(ErrorMessage = "Title not set")]
        public string Title { get; set; }
    }
}
