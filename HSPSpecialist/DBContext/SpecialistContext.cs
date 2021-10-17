using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HSPSpecialist.Models;

namespace HSPSpecialist.DBContext
{
    public class SpecialistContext : DbContext
    {
        public SpecialistContext(DbContextOptions<SpecialistContext> options) : base(options)
        {
        }

        public DbSet<Specialist> Specialist { get; set; }
        public DbSet<Service> Service { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Specialist>().HasData(
                new Specialist
                {
                    Id = 1,
                    Name = "Milo",
                    NRIC = "S1234567C",
                    Services = 3,
                    Contact = 7697807,
                    Available = true,
                    Address = "BLK221 PENDING ROAD #08-149",
                    Email = "test@singnet.com",
                    CreatedBy = "S1234567C",
                    CreatedDate = System.DateTime.Now,
                    LastUpdatedBy = "S234567C",
                    LastUpdatedDate = System.DateTime.Now,
                    IsDeleted = false
                }
            );

            modelBuilder.Entity<Service>().HasData(
                new Service
                {
                    Id = 3,
                    Description = "testServices",
                    CreatedBy = "S1234567C",
                    CreatedDate = System.DateTime.Now,
                    LastUpdatedBy = "S234567C",
                    LastUpdatedDate = System.DateTime.Now,
                    IsDeleted = false
                }
            );
        }
    }
}
