namespace TEEEEEsts
{
    using System;
    using BLL;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class UnitTests
    {
        [Fact]
        public void PasswordHashTheSameTest()
        {
            string password = "555";
            string saltedPassword = password + PasswordHash.GetSalt();
            string hash = PasswordHash.HashPassword(saltedPassword);
            Assert.Equal(hash, PasswordHash.HashPassword(saltedPassword));
        }

        [Fact]
        public void PasswordHashTest()
        {
            string password = "555";
            string hash = PasswordHash.HashPassword(password);
            Assert.Equal(hash, "de21c670ae7c3f6f3f1f37029303c9");
        }

    }
}
