using Microsoft.EntityFrameworkCore;
using PizzaClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PizzaClub.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
    }
}
