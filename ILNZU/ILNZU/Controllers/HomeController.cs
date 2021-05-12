// <copyright file="HomeController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ILNZU.Controllers
{
    using System;
    using System.Linq;
    using System.Diagnostics;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using BLL.Services;
    using DAL.Models;
    using ILNZU.Models;
    using ILNZU.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

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
        /// <param name="meetingRep">Meeting room repository.</param>
        /// <param name="inviteRep">Invite repository.</param>
        /// <param name="userRep">User repository.</param>
        public HomeController(MeetingRoomRepository meetingRep, InviteRepository inviteRep, UserRepository userRep)
        {
            this.meetingRoomRepository = meetingRep;
            this.inviteRepository = inviteRep;
            this.userRepository = userRep;
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
        /// return viewbag of invites.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<IActionResult> Invite()
        {
            var invites = await this.inviteRepository.GetInvites(Convert.ToInt32(this.User.FindFirst(ClaimTypes.NameIdentifier).Value));
            this.ViewBag.Invites = from invite in invites
                                    select new KeyValuePair<string, DateTime>(invite.MeetingRoom.Title, invite.DateTime);
            return this.View("Invite", this.User.Identity.Name);
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
            User u = this.userRepository.FindUser(email).Result;
            if (u != null)
            {
                await this.inviteRepository.RemoveInvite(u.Id, meetingId);
                await this.inviteRepository.AddInvite(u.Id, meetingId);
            }

            return this.RedirectToAction("Index");
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
