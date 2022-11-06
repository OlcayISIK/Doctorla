using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Core.Utils
{
    public static class Validate
    {
        // username should not contain whitespaces or uppercase letters
        public static bool Username(string username)
        {
            return username == username.ToLowerInvariant().Trim();
        }

        // password should not contain whitespaces
        public static bool Password(string password)
        {
            return password == password.Trim();
        }
    }
}
