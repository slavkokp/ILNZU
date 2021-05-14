// <copyright file="AttachmentRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BLL.Services
{
    using System.Threading.Tasks;
    using DAL;
    using DAL.Models;

    /// <summary>
    /// Attachment repository class.
    /// </summary>
    public class AttachmentRepository
    {
        /// <summary>
        /// Adds an attachment to the database.
        /// </summary>
        /// <param name="attachment">Attachment.</param>
        /// <returns>Attachment id.</returns>
        public async Task<int> AddAttachment(Attachment attachment)
        {
            return await DBRepository.AddAttachment(attachment);
        }

        /// <summary>
        /// Finds an attachment in a database.
        /// </summary>
        /// <param name="attachmentId">Attachment id.</param>
        /// <returns>An attachment.</returns>
        public async Task<Attachment> FindAttachment(int attachmentId)
        {
            return await DBRepository.FindAttachment(attachmentId);
        }
    }
}
