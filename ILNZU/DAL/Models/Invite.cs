using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Invite
    {
        public int InviteId { get; set; }
        public int UserId { get; set; }
        public DateTime DateTime { get; set; }

        public int MeetingRoomId { get; set; }
        public MeetingRoom MeetingRoom { get; set; }
    }
}
