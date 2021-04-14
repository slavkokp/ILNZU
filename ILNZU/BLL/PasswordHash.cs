// <copyright file="PasswordHash.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BLL
{
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// A class for hashing passwords.
    /// </summary>
    public static class PasswordHash
    {
        /// <summary>
        /// Creates a hash for users password.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <returns>The hashed password.</returns>
        public static string HashPassword(string password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));
            byte[] result = md5.Hash;
            StringBuilder str = new StringBuilder();
            for (int i = 1; i < result.Length; i++)
            {
                str.Append(result[i].ToString("x2"));
            }

            return str.ToString();
        }

        /// <summary>
        /// Creates a salt string.
        /// </summary>
        /// <returns>The salt string.</returns>
        public static string GetSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[16];

            rng.GetBytes(buffer);
            string salt = System.BitConverter.ToString(buffer);
            return salt;
        }
    }
}
