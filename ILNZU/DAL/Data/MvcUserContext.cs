using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Models;

namespace DAL.Data
{
    public class MvcUserContext : DbContext
    {
        public MvcUserContext(DbContextOptions<MvcUserContext> options)
            : base(options)
        {
        }
        public DbSet<User> User { get; set; }
    }
}
