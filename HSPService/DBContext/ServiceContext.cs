using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HSPService.Models;


namespace HSPService.DBContext
{
    public class ServiceContext : DbContext
    {
        public ServiceContext(DbContextOptions<ServiceContext> options) : base(options)
        {
        }
        public DbSet<Service> Service { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Service>().HasData(
                new Service
                {
                    Id = 1,
                    Services = "testServices",
                    CreatedBy = "S1234567C",
                    CreatedDate = System.DateTime.Now,
                    LastUpdatedBy = "S234567C",
                    LastUpdatedDate = System.DateTime.Now,
                    IsDeleted  = false
                }
            );
        }

    }
}
