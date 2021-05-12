﻿// <copyright file="HomeController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ILNZU.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using BLL.Services;
    using DAL.Models;
    using ILNZU.Models;
    using ILNZU.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Home controller.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly MeetingRoomRepository meetingRoomRepository;
        private readonly InviteRepository inviteRepository;
        private readonly UserRepository userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="rep">Meeting room repository.</param>
        public HomeController(MeetingRoomRepository rep)
        {
            this.meetingRoomRepository = rep;
        }

        /// <summary>
        /// Shows a view.
        /// </summary>
        /// <returns>A view.</returns>
        [Authorize]
        public async Task<IActionResult> Index()
        {
            this.ViewBag.MeetingRooms = await this.meetingRoomRepository.GetMeetingRooms(Convert.ToInt32(this.User.FindFirst(ClaimTypes.NameIdentifier).Value));
            return this.View("Index", this.User.Identity.Name);
        }

        /// <summary>
        /// Shows a view.
        /// </summary>
        /// <returns>A view.</returns>
        [Authorize]
        public IActionResult Privacy()
        {
            return this.View();
        }

        /// <summary>
        /// Adds new meeting.
        /// </summary>
        /// <param name="model">Room model.</param>
        /// <returns>A view.</returns>
        [Authorize]
        public async Task<IActionResult> CreateMeeting(RoomModel model)
        {
            var room = await this.meetingRoomRepository.CreateRoom(model.Title, Convert.ToInt32(this.User.FindFirst(ClaimTypes.NameIdentifier).Value));
            await this.meetingRoomRepository.AddUserToMeeting(Convert.ToInt32(this.User.FindFirst(ClaimTypes.NameIdentifier).Value), room.MeetingRoomId);
            this.ViewBag.Meetings = this.meetingRoomRepository.GetMeetingRooms(Convert.ToInt32(this.User.FindFirst(ClaimTypes.NameIdentifier).Value)).Result;

            return this.RedirectToAction("Index");
        }

        /// <summary>
        /// Deletes meeting room.
        /// </summary>
        /// <param name="meetingId">Meeting id.</param>
        /// <returns>A view.</returns>
        [Authorize]
        public async Task<IActionResult> DeleteMeeting(int meetingId)
        {
            bool isCreatorOfMeeting = await this.meetingRoomRepository.CheckIfUserIsCreatorOfMeetingRoom(Convert.ToInt32(this.User.FindFirst(ClaimTypes.NameIdentifier).Value), meetingId);
            if (isCreatorOfMeeting)
            {
                await this.meetingRoomRepository.DeleteRoom(meetingId);
            }
            else
            {
                await this.meetingRoomRepository.RemoveUserFromMeeting(Convert.ToInt32(this.User.FindFirst(ClaimTypes.NameIdentifier).Value), meetingId);
            }

            return this.RedirectToAction("Index");
        }

        /// <summary>
        /// Deletes user from a meeting.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <param name="meetingId">Meeting id.</param>
        /// <returns>A view.</returns>
        [Authorize]
        public async Task<IActionResult> Kick(int userId, int meetingId)
        {
            await this.meetingRoomRepository.RemoveUserFromMeeting(userId, meetingId);
            return this.View();
        }

        /// <summary>
        /// Creates an invite.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="meetingId">Meeing id.</param>
        /// <returns>Web page.</returns>
        [Authorize]
        public async Task<IActionResult> CreateInvite(string email, int meetingId)
        {
            User u = await this.userRepository.FindUser(email);
            await this.meetingRoomRepository.RemoveUserFromMeeting(u.Id, meetingId);
            return this.View();
        }

        /// <summary>
        /// Removes invite.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <param name="meetingId">Meeting id.</param>
        /// <returns>Web page.</returns>
        [Authorize]
        public async Task<IActionResult> RemoveInvite(int userId, int meetingId)
        {
            await this.inviteRepository.RemoveInvite(userId, meetingId);
            return this.View();
        }

        /// <summary>
        /// Gets invites.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns>Web page.</returns>
        [Authorize]
        public async Task<IActionResult> GetInvites(int userId)
        {
            this.ViewBag.Invites = await this.inviteRepository.GetInvites(userId);
            return this.View();
        }

        /// <summary>
        /// Gets meeting title.
        /// </summary>
        /// <param name="meetingId">Meeting id.</param>
        /// <returns>A view.</returns>
        [Authorize]
        public async Task<IActionResult> GetMeetingTitle(int meetingId)
        {
            string title = await this.meetingRoomRepository.GetMeetingTitle(meetingId);
            return this.View(title);
        }

        /// <summary>
        /// Shows an error.
        /// </summary>
        /// <returns>A view.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
