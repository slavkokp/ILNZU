using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class MeetingRoom
    {
        public int MeetingRoomId { get; set; }
        [Required]
        public string Title { get; set; }

        public int UserId { get; set; } // Creator of the room
        public User User { get; set; }
    }
}
