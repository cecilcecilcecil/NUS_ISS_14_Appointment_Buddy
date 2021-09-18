using Microsoft.AspNetCore.Mvc;

namespace AppointmentBuddy.Core.Model
{
    [Bind("Username, Password, Salt")]
    public class UserParameter
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string UserTypeId { get; set; }
        public string Salt { get; set; }
    }
}
