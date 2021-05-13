using DAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PathService
    {
        public static Attachment CreateAttachment(string fileName)
        {
            Attachment attachment = new Attachment();
            attachment.FileName = fileName;
            attachment.Path = Path.Combine("Files", DateTime.Now.ToString(@"hh\_mm\_ss") + fileName);
            return attachment;
        }
    }
}
