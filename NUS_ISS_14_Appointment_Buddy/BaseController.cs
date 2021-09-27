using AppointmentBuddy.Core.Common.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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

        protected string UserName
        {
            get
            {
                string userName = null;

                var identity = User.Identity;

                if (identity != null && identity.IsAuthenticated)
                {
                    userName = identity.Name;
                }

                return userName ?? "";
            }
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

        protected string UserId
        {
            get
            {
                var claims = User.Claims;
                string userId = "";

                if (claims != null)
                {
                    userId = GetClaimValue(Constants.AppClaimTypes.Id);
                }

                return userId;
            }
        }

        protected string UserType
        {
            get
            {
                var claims = User.Claims;
                string userType = "";

                if (claims != null)
                {
                    userType = GetClaimValue(Constants.AppClaimTypes.UserType);
                }

                return userType;
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

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ViewData["Nonce"] = "Z2VtczIwMjBjc3Bub25jZQ==";

            base.OnActionExecuted(filterContext);
        }
    }
}
