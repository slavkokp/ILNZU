// <copyright file="HomeController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ILNZU.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using BLL.Services;
    using ILNZU.Models;
    using ILNZU.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Home controller.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly MeetingRoomRepository rep;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="rep">Meeting room repository.</param>
        public HomeController(MeetingRoomRepository rep)
        {
            this.rep = rep;
        }

        /// <summary>
        /// Shows a view.
        /// </summary>
        /// <returns>A view.</returns>
        [Authorize]
        public async Task<IActionResult> Index()
        {
            this.ViewBag.MeetingRooms = await this.rep.GetMeetingRooms(Convert.ToInt32(this.User.FindFirst(ClaimTypes.NameIdentifier).Value));
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
            var room = await this.rep.CreateRoom(model.Title, Convert.ToInt32(this.User.FindFirst(ClaimTypes.NameIdentifier).Value));
            await this.rep.AddUserToMeeting(Convert.ToInt32(this.User.FindFirst(ClaimTypes.NameIdentifier).Value), room.MeetingRoomId);
            this.ViewBag.Meetings = this.rep.GetMeetingRooms(Convert.ToInt32(this.User.FindFirst(ClaimTypes.NameIdentifier).Value)).Result;
            return this.View();
        }

        /// <summary>
        /// Deletes meeting room.
        /// </summary>
        /// <param name="meetingId">Meeting id.</param>
        /// <returns>A view.</returns>
        [Authorize]
        public async Task<IActionResult> DeleteMeeting(int meetingId)
        {
            bool isCreatorOfMeeting = await this.rep.CheckIfUserIsCreatorOfMeetingRoom(Convert.ToInt32(this.User.FindFirst(ClaimTypes.NameIdentifier).Value), meetingId);
            if (isCreatorOfMeeting)
            {
                await this.rep.DeleteRoom(meetingId);
            }
            else
            {
                await this.rep.RemoveUserFromMeeting(Convert.ToInt32(this.User.FindFirst(ClaimTypes.NameIdentifier).Value), meetingId);
            }

            return this.View();
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
            await this.rep.RemoveUserFromMeeting(userId, meetingId);
            return this.View();
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
