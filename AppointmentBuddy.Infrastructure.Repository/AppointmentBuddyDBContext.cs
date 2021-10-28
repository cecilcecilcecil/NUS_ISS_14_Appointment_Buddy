using Microsoft.EntityFrameworkCore;
using M = AppointmentBuddy.Core.Model;

namespace AppointmentBuddy.Infrastructure.Repository
{
    public class AppointmentBuddyDBContext : DbContext
    {
        public AppointmentBuddyDBContext(DbContextOptions<AppointmentBuddyDBContext> options) : base(options)
        {
        }

        public DbSet<M.Appointment> Appointment { get; set; }
        public DbSet<M.PatientInfo> PatientInfo { get; set; }
        public DbSet<M.Services> Services { get; set; }
        public DbSet<M.Room> Room { get; set; }
        public DbSet<M.Role> Role { get; set; }
        public DbSet<M.User> User { get; set; }
        public DbSet<M.UserRole> UserRole { get; set; }
    }
}
