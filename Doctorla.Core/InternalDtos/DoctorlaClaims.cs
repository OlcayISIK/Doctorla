using Doctorla.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Core.InternalDtos
{
    public class DoctorlaClaims
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public UserType UserType { get; set; }
        public long HospitalId { get; set; }
        public bool GuestUser { get; set; }
        public bool CanSeeHiddenHospitals { get; set; }
    }
}
