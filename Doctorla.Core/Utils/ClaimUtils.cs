using Doctorla.Core.Enums;
using Doctorla.Core.InternalDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Core.Utils
{
    public static class ClaimUtils
    {
        private const string Username = "name";
        private const string UserId = "uid";
        private const string HospitalId = "hid";
        private const string UserType = "rid";
        private const string GuestUser = "guest";
        private const string CanSeeHiddenHospitals = "csh";

        public static DoctorlaClaims GetClaims(IEnumerable<Claim> claims)
        {
            var dsc = new DoctorlaClaims();
            if (claims == null) return dsc;
            foreach (var claim in claims)
            {
                switch (claim.Type)
                {
                    case Username:
                        dsc.Username = claim.Value;
                        break;
                    case UserId:
                        dsc.UserId = long.Parse(claim.Value);
                        break;
                    case HospitalId:
                        dsc.HospitalId = long.Parse(claim.Value);
                        break;
                    case UserType:
                        dsc.UserType = (UserType)int.Parse(claim.Value);
                        break;
                    case GuestUser:
                        dsc.GuestUser = bool.Parse(claim.Value);
                        break;
                    case CanSeeHiddenHospitals:
                        dsc.CanSeeHiddenHospitals = bool.Parse(claim.Value);
                        break;
                }
            }
            return dsc;
        }

        public static Claim[] CreateDoctorClaims(long userId, string username, UserType userType, long hospitalId)
        {
            return new[]
            {
                new Claim(Username, username),
                new Claim(UserId, userId.ToString()),
                new Claim(UserType, ((int)userType).ToString()),
                new Claim(HospitalId, hospitalId.ToString())
            };
        }

        public static Claim[] CreateUserClaims(long userId, string username, UserType userType)
        {
            return new[]
            {
                new Claim(Username, username),
                new Claim(UserId, userId.ToString()),
                new Claim(UserType, ((int)userType).ToString()),
            };
        }

        public static IEnumerable<Claim> CreateAdminClaims(long userId, string username)
        {
            return new[]
            {
                new Claim(UserId, userId.ToString()),
                new Claim(GuestUser, false.ToString()),
                new Claim(Username, username),
                new Claim(CanSeeHiddenHospitals, true.ToString())
            };
        }

        public static IEnumerable<Claim> CreateHospitalClaims(long userId, string username)
        {
            return new[]
            {
                new Claim(UserId, userId.ToString()),
                new Claim(GuestUser, false.ToString()),
                new Claim(Username, username),
                new Claim(CanSeeHiddenHospitals, true.ToString())
            };
        }
    }
}
