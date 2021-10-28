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

        private static readonly int[] Multiples = { 2, 7, 6, 5, 4, 3, 2 };

        public static bool IsNRICValid(string nric)
        {
            if (string.IsNullOrEmpty(nric))
            {
                return false;
            }

            //	check length must be 9 digits
            if (nric.Length != 9)
            {
                return false;
            }

            int total = 0
                , count = 0
                , numericNric;
            char first = nric[0]
                , last = nric[nric.Length - 1];

            // first chat always S, T, F, G
            if (first != 'S' && first != 'T' && first != 'F' && first != 'G')
            {
                return false;
            }

            if (!int.TryParse(nric.Substring(1, nric.Length - 2), out numericNric))
            {
                return false;
            }

            while (numericNric != 0)
            {
                total += numericNric % 10 * Multiples[Multiples.Length - (1 + count++)];

                numericNric /= 10;
            }

            char[] outputs;

            if (first == 'S' || first == 'T')
            {
                outputs = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'Z', 'J' };
            }
            else
            {
                outputs = new char[] { 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'T', 'U', 'W', 'X' };
            }

            int checkDigit = 11 - total % 11;

            return last == outputs[checkDigit - 1];
        }
    }
}
