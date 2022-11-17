using Doctorla.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Data.Entities
{
    public class Admin : Entity
    {
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public UserStatus Status { get; set; }
    }
}
