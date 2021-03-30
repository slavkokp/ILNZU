﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    class Message
    {
        public int MessageId { get; set; }
        public DateTime DateTime { get; set; }
        [Required]
        public string Text { get; set; }
        public int UserId { get; set; }
        public int MeetingRoomId { get; set; }
    }
}
