// <copyright file="UserRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BLL.Services
{
    using System.Threading.Tasks;
    using DAL;
    using DAL.Models;

    /// <summary>
    /// User repository class.
    /// </summary>
    public class UserRepository
    {
        /// <summary>
        /// Findes user by id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>The user found.</returns>
        public async Task<User> FindUser(int id)
        {
            return await DBRepository.FindUser(id);
        }

        /// <summary>
        /// Finds user by email.
        /// </summary>
        /// <param name="email">Users email.</param>
        /// <returns>The user found.</returns>
        public async Task<User> FindUser(string email)
        {
            return await DBRepository.FindUser(email);
        }

        /// <summary>
        /// Finds user by email and salted password.
        /// </summary>
        /// <param name="email">Users email.</param>
        /// <param name="saltedPassword">Salted password.</param>
        /// <returns>The user found.</returns>
        public async Task<User> FindUser(string email, string saltedPassword)
        {
            return await DBRepository.FindUser(email, saltedPassword);
        }

        /// <summary>
        /// Checks if user is member of meeting room.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <param name="meetingRoomId">Meeting room id.</param>
        /// <returns>Boolean if user is member of meeting room.</returns>
        public async Task<bool> CheckIfUserIsMemberOfMeetingRoom(int userId, int meetingRoomId)
        {
            return await DBRepository.CheckIfUserIsMemberOfMeetingRoom(userId, meetingRoomId);
        }

        /// <summary>
        /// Adds a user.
        /// </summary>
        /// <param name="email">User email.</param>
        /// <param name="password">User password.</param>
        /// <param name="name">User name.</param>
        /// <param name="surname">User surname.</param>
        /// <param name="username">User username.</param>
        /// <returns>The user added.</returns>
        public async Task<int> AddUser(string email, string password, string name, string surname, string username)
        {
            return await DBRepository.AddUser(email, password, name, surname, username);
        }
    }
}
