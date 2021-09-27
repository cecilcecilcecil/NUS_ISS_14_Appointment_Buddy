using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NUS_ISS_14_Appointment_Buddy.Helper
{
    public class IdentityHelper
    {
        public static bool HasRole(ClaimsIdentity identity, string roleId)
        {
            if (identity == null)
                throw new ArgumentNullException(nameof(identity));

            foreach (var claim in identity.Claims)
            {
                if (claim != null && !string.IsNullOrEmpty(roleId))
                {
                    if (string.Equals(claim.Type, ClaimTypes.Role, StringComparison.Ordinal)
                        && string.Equals(claim.Value, roleId, StringComparison.Ordinal))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
