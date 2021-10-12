using Microsoft.EntityFrameworkCore;
using M = AppointmentBuddy.Core.Model;

namespace AppointmentBuddy.Infrastructure.Repository
{
    public class RoomBuddyDBContext : DbContext
    {
        public RoomBuddyDBContext(DbContextOptions<RoomBuddyDBContext> options) : base(options)
        {
        }

        public DbSet<M.Room> Appointment { get; set; }
        public DbSet<M.Role> Role { get; set; }
        public DbSet<M.User> User { get; set; }
        public DbSet<M.UserRole> UserRole { get; set; }
    }
}
