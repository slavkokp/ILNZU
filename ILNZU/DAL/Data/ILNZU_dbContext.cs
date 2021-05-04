// <copyright file="ILNZU_dbContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DAL.Data
{
    using DAL.Models;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// A class for DbContext.
    /// </summary>
    public class ILNZU_dbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ILNZU_dbContext"/> class.
        /// </summary>
        public ILNZU_dbContext()
        {
        }

        /// <summary>
        /// Gets or sets User.
        /// </summary>
        public DbSet<User> User { get; set; }

        /// <summary>
        /// Gets or sets Message.
        /// </summary>
        public DbSet<Message> Message { get; set; }

        /// <summary>
        /// Gets or sets Invite.
        /// </summary>
        public DbSet<Invite> Invite { get; set; }

        /// <summary>
        /// Gets or sets Meeting room.
        /// </summary>
        public DbSet<MeetingRoom> MeetingRoom { get; set; }

        /// <summary>
        /// Gets or sets User member of meeting room.
        /// </summary>
        public DbSet<UserMemberOfMeetingRoom> UserMemberOfMeetingRoom { get; set; }

        public DbSet<Attachment> Attachment { get; set; }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserMemberOfMeetingRoom>().HasKey(umr => new { umr.UserId, umr.MeetingRoomId });
        }

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ILNZU_database;Username=postgres;Password=555");
        }
    }
}
