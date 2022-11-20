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
        private const string Id = "id";
        private const string Email = "email";
        private const string AccountType = "atype";

        public static DoctorlaClaims GetClaims(IEnumerable<Claim> claims)
        {
            var dsc = new DoctorlaClaims();
            if (claims == null) return dsc;
            foreach (var claim in claims)
            {
                switch (claim.Type)
                {
                    case Email:
                        dsc.Email = claim.Value;
                        break;
                    case Id:
                        dsc.Id = long.Parse(claim.Value);
                        break;
                    case AccountType:
                        dsc.AccountType = (AccountType)int.Parse(claim.Value);
                        break;
                }
            }
            return dsc;
        }

        public static Claim[] CreateClaims(long id, string email, AccountType accountType)
        {
            return new[]
            {
                new Claim(Id, id.ToString()),
                new Claim(Email, email),
                new Claim(AccountType, ((int)accountType).ToString()),
            };
        }
    }
}
