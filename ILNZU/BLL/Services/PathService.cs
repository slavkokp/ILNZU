// <copyright file="PathService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BLL.Services
{
    using System;
    using System.IO;
    using DAL.Models;

    /// <summary>
    /// Path service class.
    /// </summary>
    public class PathService
    {
        /// <summary>
        /// Creates an attachment.
        /// </summary>
        /// <param name="fileName">File name.</param>
        /// <returns>Attachment.</returns>
        public static Attachment CreateAttachment(string fileName)
        {
            Attachment attachment = new Attachment();
            attachment.FileName = fileName;
            attachment.Path = Path.Combine("Files", DateTime.Now.ToString(@"hh\_mm\_ss") + fileName);
            return attachment;
        }
    }
}
