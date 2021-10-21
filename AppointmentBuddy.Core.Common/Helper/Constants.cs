using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentBuddy.Core.Common.Helper
{
    public static class Constants
    {
        public static class AppClaimTypes
        {
            public const string AccessToken = "ApptBuddyToken";
            public const string ClaimTypeName = "ApptBuddyClaimType";
            public const string Id = "ApptBuddyId";
            public const string Sub = "Sub";
            public const string UserType = "ApptBuddyUserType";
        }

        public static class ErrorCodes
        {
            public const int Failure = 0;
            public const int Success = 1;
            public const int ConcurrencyError = 2;
            public const int NotAuthorized = 3;

            //File Error Codes
            public const int FileZero = 0;
            public const int FileSize = 1;
            public const int FileExtension = 2;
            public const int FileName = 3;
            public const int FileCharacter = 4;
            public const int NoError = 5;
        }

        public static class General
        {
            public const string Test = "";
        }

        public static class Header
        {
            public const string ServiceBearerHeaderName = "Bearer";
            public const string ServiceCorrelationHeaderName = "X-Correlation-ID";
            public const string ServiceConnectionHeaderName = "Connection-ID";
            public const string ServiceAuthorizationHeaderName = "Authorization";
        }

        public static class RegularExpressions
        {
            public const string IsGuid = "^[{(]?[0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$";
            public const string IsNumeric = "[0-9]*$";
            public const string IsUnitNo = "[0-9]{1,5}[A-Za-z]?$";
            public const string isMoney2DP = "\\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$";
            public const string IsPhone = "^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\\s\\./0-9]*$";
            public const string IsSGPhone = "[6|8|9]\\d{7}|\\+65[6|8|9]\\d{7}";
            public const string IsSGPhoneNoCode = "^[8|9]\\d{7}$";
            public const string IsEmailLocal = "^([a-zA-Z0-9_\\-\\.]+)";
            public const string IsEmailFull = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            public const string IsEmailDomain = "^((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$";
            public const string TimeInHHMM = "^(0[0-9]|1[0-9]|2[0-3]|[0-9]):[0-5][0-9]$";
            public const string DateInddMMyyyy = "^(?:(?:31(\\/|-|\\.)(?:0?[13578]|1[02]))\\1|(?:(?:29|30)(\\/|-|\\.)(?:0?[1,3-9]|1[0-2])\\2))(?:(?:1[6-9]|[2-9]\\d)?\\d{2})$|^(?:29(\\/|-|\\.)0?2\\3(?:(?:(?:1[6-9]|[2-9]\\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\\d|2[0-8])(\\/|-|\\.)(?:(?:0?[1-9])|(?:1[0-2]))\\4(?:(?:1[6-9]|[2-9]\\d)?\\d{2})$";
            public const string ValidFileFormat = "/^[\\w,\\s-]+\\.[A-Za-z]{3}$/";
            public const string ValidFileFormat2 = "^[\\w,\\s()-_.]+\\.[a-zA-Z0-9]{3,4}$";
            public const string FileName = "^[\\w,\\s-]+\\.[A-Za-z]{3}$";
            public const string IsJwt = "^[A-Za-z0-9-_=]+\\.[A-Za-z0-9-_=]+\\.?[A-Za-z0-9-_.+/=]*$";
            public const string IsURL = "^https?:\\/\\/(www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\\+.~#?&//=]*)$";
            public const string DomainNames = "^(((?!-))(xn--|_{1,1})?[a-z0-9-]{0,61}[a-z0-9]{1,1}\\.)*(xn--)?([a-z0-9][a-z0-9\\-]{0,60}|[a-z0-9-]{1,30}\\.[a-z]{2,})$";
        }

        public static class RoleType
        {
            public const string Admin = "FA9DDCE8-387B-4018-B10C-A2C690B71B9F";
            public const string Patient = "8F0CD491-06CB-43EC-B874-F492C768E9EB";
            public const string Staff = "9B660716-4E47-4C17-A28D-09B66B44B6D6";
        }

        public static class UserType
        {
            public const string Admin = "F06EC0F3-D16F-4B9E-9261-05857F28B32D";
            public const string Patient = "D939D14A-53B3-4479-A384-7C2278543A56";
            public const string Staff = "005FB465-9E78-4FEE-BDD2-888C4890D462";
        }

        public static class ValidationMessages
        {
            public const string RequiredMessage = "The {0} is required";
            public const string DateFormatMessage = "The {0} must be in date format.";
            public const string TimeFormatMessage = "The {0} must be in HH:MM format.";
            public const string PhoneFormatMessage = "Please enter a valid Singapore number.";
            public const string Concurrency = "The record may be modified elsewhere, please refresh and try again.";
            public const string SystemUnavailable = "System unavailable, please refresh or try again later.";
        }
    }
}
