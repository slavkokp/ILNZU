// <copyright file="UnitTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TEEEEEsts
{
    using DAL;
    using Xunit;
    using ILNZU.Controllers;
    using DAL.Models;
    using System.IO;

    /// <summary>
    /// Class with Unit tests.
    /// </summary>
    public class UnitTests
    {
        /// <summary>
        /// Function for password hashing testing.
        /// </summary>
        [Fact]
        public void PasswordHashTheSameTest()
        {
            string password = "555";
            string saltedPassword = password + PasswordHash.GetSalt();
            string hash = PasswordHash.HashPassword(saltedPassword);
            Assert.Equal(hash, PasswordHash.HashPassword(saltedPassword));
        }

        /// <summary>
        /// Function for password hashing testing.
        /// </summary>
        [Fact]
        public void PasswordHashTest()
        {
            string password = "555";
            string hash = PasswordHash.HashPassword(password);
            Assert.Equal("de21c670ae7c3f6f3f1f37029303c9", hash);
        }

        [Fact]
        public void CreateAttachmentTest1()
        {
            string fileName = "lolipop.cs";
            Attachment attachment = AttachmentController.CreateAttachment(fileName);
            Assert.Equal(fileName, attachment.FileName);
        }

        [Fact]
        public void CreateAttachmentTest2()
        {
            string fileName = "lolipop.cs";
            Attachment attachment = AttachmentController.CreateAttachment(fileName);
            Assert.Equal(fileName, attachment.Path.Substring(0, 5));
        }

        [Fact]
        public void CreateAttachmentTest3()
        {
            string fileName = "lolipop.cs";
            Attachment attachment = AttachmentController.CreateAttachment(fileName);
            Assert.Equal(fileName, Path.GetFileName(attachment.Path).Substring(8, fileName.Length));
        }
    }
}
