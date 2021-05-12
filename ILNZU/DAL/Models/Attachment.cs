using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace DAL.Models
{
    public class Attachment
    {
        public int AttachmentId { get; set; }

        [JsonPropertyName("path")]
        public string Path { get; set; }

        [JsonPropertyName("filename")]
        public string FileName { get; set; }
    }
}
