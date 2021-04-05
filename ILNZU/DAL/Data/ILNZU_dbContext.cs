﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Models;

namespace DAL.Data
{
    public class ILNZU_dbContext : DbContext
    {
        //public ILNZU_dbContext(DbContextOptions<ILNZU_dbContext> options)
        //    : base(options)
        //{
        //}
        public ILNZU_dbContext()
        {

        }

        public DbSet<User> User { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Invite> Invite { get; set; }
        public DbSet<MeetingRoom> MeetingRoom { get; set; }
        public DbSet<UserMemberOfMeetingRoom> UserMemberOfMeetingRoom { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserMemberOfMeetingRoom>().HasKey(umr => new { umr.UserId, umr.MeetingRoomId });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ILNZU_database;Username=postgres;Password=555");
        }
    }
}
