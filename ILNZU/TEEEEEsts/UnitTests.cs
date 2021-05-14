// <copyright file="UnitTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TEEEEEsts
{
    using System.IO;
    using BLL.Services;
    using DAL;
    using DAL.Models;
    using Xunit;

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

        /// <summary>
        /// Function for create attachment testing.
        /// </summary>
        [Fact]
        public void CreateAttachmentTest1()
        {
            string fileName = "lolipop.cs";
            Attachment attachment = PathService.CreateAttachment(fileName);
            Assert.Equal(fileName, attachment.FileName);
        }

        /// <summary>
        /// Function for create attachment testing.
        /// </summary>
        [Fact]
        public void CreateAttachmentTest2()
        {
            string fileName = "lolipop.cs";
            Attachment attachment = PathService.CreateAttachment(fileName);
            Assert.Equal("Files", attachment.Path.Substring(0, 5));
        }

        /// <summary>
        /// Function for create attachment testing.
        /// </summary>
        [Fact]
        public void CreateAttachmentTest3()
        {
            string fileName = "lolipop.cs";
            Attachment attachment = PathService.CreateAttachment(fileName);
            Assert.Equal(fileName, Path.GetFileName(attachment.Path).Substring(8, fileName.Length));
        }
    }
}
