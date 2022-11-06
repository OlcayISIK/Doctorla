using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Core.Utils
{
    public static class Generate
    {
        private const int InvoiceNumberPaddingLength = 5;
        public static string ApprovalToken()
        {
            return Guid.NewGuid().ToString();
        }

        public static string PasswordResetToken()
        {
            return Guid.NewGuid().ToString();
        }

        public static string TableCode()
        {
            return new Random().Next(10000, 99999).ToString();
        }


        public static string NextInvoiceNumber(string currentInvoiceNumber)
        {
            if (currentInvoiceNumber == null)
            {
                return GenerateInvoiceNumber(GetTodaysDateTimeString(), 1);
            }
            else
            {
                var currentDateTimeString = currentInvoiceNumber.Substring(0, 8);
                var actualDateTimeString = GetTodaysDateTimeString();
                if (string.Equals(currentDateTimeString, actualDateTimeString))
                {
                    var dailyInvoiceNumber = int.Parse(currentInvoiceNumber.Substring(8, InvoiceNumberPaddingLength));
                    dailyInvoiceNumber++;
                    return GenerateInvoiceNumber(currentDateTimeString, dailyInvoiceNumber);
                }
                else
                {
                    return GenerateInvoiceNumber(GetTodaysDateTimeString(), 1);
                }
            }
        }

        private static string GenerateInvoiceNumber(string dateTimeString, int number)
        {
            return dateTimeString + number.ToString().PadLeft(5, '0');
        }

        private static string GetTodaysDateTimeString()
        {
            return DateTime.UtcNow.ToString("yyyyMMdd");
        }

        /// <summary>
        /// Generates a Random Password
        /// respecting the given strength requirements.
        /// </summary>
        /// <param name="opts">A valid PasswordOptions object
        /// containing the password strength requirements.</param>
        /// <returns>A random password</returns>
        public static string InitialUserPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
            "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
            "abcdefghijkmnopqrstuvwxyz",    // lowercase
            "0123456789",                   // digits
            "!@$?_-"                        // non-alphanumeric
        };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
    }
}
