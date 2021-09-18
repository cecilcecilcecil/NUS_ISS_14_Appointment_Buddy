using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AppointmentBuddy.Core.Common.Helper
{
    public static class Validator
    {
        public static string IsSafeHost(string host)
        {
            // Replace invalid characters with empty strings.
            try
            {
                return Regex.Replace(host, Constants.RegularExpressions.IsURL, "",
                     RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            // If we timeout when replacing invalid characters,
            // we should return Empty.
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }

        public static string CleanInput(string strIn)
        {
            // Replace invalid characters with empty strings.
            try
            {
                return Regex.Replace(strIn, @"[^\w\.@-]", "",
                                     RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            // If we timeout when replacing invalid characters,
            // we should return Empty.
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }
    }
}
