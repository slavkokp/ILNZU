using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using BLL.Services;
using DAL.Models;

namespace ILNZU.Controllers
{
    public class AttachmentController : Controller
    {
        private readonly AttachmentRepository attachRep;
        private readonly IWebHostEnvironment appEnvironment;

        public AttachmentController(AttachmentRepository attachRep, IWebHostEnvironment appEnvironment)
        {
            this.appEnvironment = appEnvironment;
            this.attachRep = attachRep;
        }

        [Authorize]
        [HttpPost]
        public async Task<OkObjectResult> CreateAttachment(IFormFile file)
        {
            Attachment attachment = new Attachment();
            attachment.FileName = file.FileName;
            attachment.Path = Path.Combine("Files", DateTime.Now.ToString(@"hh\_mm\_ss") + file.FileName);
            using (var fileStream = new FileStream(Path.Combine(this.appEnvironment.WebRootPath, attachment.Path), FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            int id = await this.attachRep.AddAttachment(attachment);
            return this.Ok(id);
        }

        [Authorize]
        public async Task<IActionResult> Download(string filePath)
        {
            string filename = Path.GetFileName(filePath);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            return this.File(memory, MimeTypes.GetMimeType(filename), filename);
        }
    }
}
