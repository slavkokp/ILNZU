using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DAL.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        
        [JsonPropertyName("datetime")]
        public DateTime DateTime { get; set; }
        
        [Required]
        [JsonPropertyName("text")]
        public string Text { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int MeetingRoomId { get; set; }
        public MeetingRoom MeetingRoom { get; set; }
    }
}
