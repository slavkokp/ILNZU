using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Models;

namespace BLL.Services
{
    public class AttachmentRepository
    {
        public async Task<int> AddAttachment(Attachment attachment)
        {
            return await DBRepository.AddAttachment(attachment);
        }
    }
}
