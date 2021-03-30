using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    class Invite
    {
        public int InviteId { get; set; }
        public int UserId { get; set; }
        public int MeetingId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
