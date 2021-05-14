// <copyright file="AttachmentController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ILNZU.Controllers
{
    using System.IO;
    using System.Threading.Tasks;
    using BLL.Services;
    using DAL.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Attachment controller.
    /// </summary>
    public class AttachmentController : Controller
    {
        private readonly AttachmentRepository attachRep;
        private readonly IWebHostEnvironment appEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentController"/> class.
        /// </summary>
        /// <param name="attachRep">Attachment repository.</param>
        /// <param name="appEnvironment">IWebHostInvironment.</param>
        public AttachmentController(AttachmentRepository attachRep, IWebHostEnvironment appEnvironment)
        {
            this.appEnvironment = appEnvironment;
            this.attachRep = attachRep;
        }

        /// <summary>
        /// Creates an attachment.
        /// </summary>
        /// <param name="file">File name.</param>
        /// <returns>Result.</returns>
        [Authorize]
        [HttpPost]
        public async Task<OkObjectResult> CreateAttachment(IFormFile file)
        {
            Attachment attachment = PathService.CreateAttachment(file.FileName);
            using (var fileStream = new FileStream(Path.Combine(this.appEnvironment.WebRootPath, attachment.Path), FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            int id = await this.attachRep.AddAttachment(attachment);
            return this.Ok(id);
        }

        /// <summary>
        /// Downloads an attachment.
        /// </summary>
        /// <param name="filePath">File path.</param>
        /// <returns>Result.</returns>
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
