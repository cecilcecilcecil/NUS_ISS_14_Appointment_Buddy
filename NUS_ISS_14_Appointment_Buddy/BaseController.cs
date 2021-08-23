using AppointmentBuddy.Core.Common.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NUS_ISS_14_Appointment_Buddy
{
    public class BaseController : Controller
    {
        ILogger _logger;
        private string _accessToken;
        //private readonly AppSettings _appSettings;

        public BaseController(ILogger logger)
        {
            _logger = logger;
        }

        protected string AccessToken
        {
            get
            {
                var claims = User.Claims;

                if (string.IsNullOrEmpty(_accessToken) && claims != null)
                {
                    _accessToken = GetClaimValue(Constants.AppClaimTypes.AccessToken);
                }

                return _accessToken;
            }
        }

        protected string GetClaimValue(string type)
        {
            string claimValue = null;
            var claims = User.Claims;

            if (claims != null)
                claimValue = claims.FirstOrDefault(x => x.Type == type)?.Value;

            return claimValue;
        }
    }
}
