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
    }
}
