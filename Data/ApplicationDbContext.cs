using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoApi.Areas.Identity.Data;
using TodoApi.Models;

namespace ProjectCars2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ProjectCars2User, Role, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Camera> Cameras { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarClass> CarClasses { get; set; }
        public DbSet<Controller> Controllers { get; set; }
        public DbSet<LapTimes> LapTimes { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<SessionMode> SessionModes { get; set; }
        public DbSet<Setup> Setups { get; set; }
        public DbSet<Track> Tracks { get; set; }
    }
}
