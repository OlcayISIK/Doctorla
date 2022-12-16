using Doctorla.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Data.Members
{
    public abstract class Member : Entity
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string HashedPassword { get; set; }
        public string IdentificationNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Address { get; set; }
        public AccountStatus AccountStatus { get; set; }
        public DateTime LastLoginDate { get; set; }
    }
}
