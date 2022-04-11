using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppFinalTest.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Band> Bands { get; set; }
        public DbSet<Album> Albums { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Band>().HasData(
                new Band() { Id = 1, Name = "Pink Floyd", Year = 1965 },
                new Band() { Id = 2, Name = "Europe", Year = 1979 },
                new Band() { Id = 3, Name = "The Doors", Year = 1965 }
            );
            modelBuilder.Entity<Album>().HasData(
               new Album() { Id = 1, Name = "LA Woman", Genre = "rock", Year = 1971, Sold = 4, BandId = 3 },
               new Album() { Id = 2, Name = "The Wall", Genre = "art rock", Year = 1979, Sold = 30, BandId = 1 },
               new Album() { Id = 3, Name = "The Final Countdown", Genre = "glam metal", Year = 1986, Sold = 4, BandId = 2 },
               new Album() { Id = 4, Name = "Meddle", Genre = "rock", Year = 1971, Sold = 2, BandId = 1 },
               new Album() { Id = 5, Name = "Strange Days", Genre = "rock", Year = 1969, Sold = 1, BandId = 3 }
           );
            base.OnModelCreating(modelBuilder);
        }
    }
}
