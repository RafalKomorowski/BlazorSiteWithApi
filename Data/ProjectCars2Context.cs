using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoApi.Areas.Identity.Data;
using TodoApi.Models;

namespace ProjectCars2Api.Models
{
    public class ProjectCars2Context : IdentityDbContext<ProjectCars2User, Role, int>
    {
        public ProjectCars2Context(DbContextOptions<ProjectCars2Context> options)
            : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ProjectCars2User>()
        //        .HasIndex(p => new { p.GamerTag })
        //        .IsUnique(true);
        //}


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