using Doctorla.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Data.Shared
{
    public class Appointment : Entity
    {
        public DateTime AppointmentStartDate { get; set; }
        public DateTime AppointmentEndhDate { get; set; }
        public AppointmentStatus Status { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }
        public long DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
