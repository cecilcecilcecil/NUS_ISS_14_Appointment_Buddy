using System;

namespace AppointmentBuddy.Core.Model
{
    public class UserInfo
    {
        public User User { get; set; }
        public Role Role { get; set; }
        public UserRole UserRole { get; set; }
    }
}
