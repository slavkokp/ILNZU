using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class UserMemberOfMeetingRoom
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int MeetingRoomId { get; set; }
        public MeetingRoom MeetingRoom { get; set; }
    }
}
