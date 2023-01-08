using Doctorla.Core.Enums;
using Doctorla.Data.Members;
using Doctorla.Data.Members.DoctorEntity;
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
        public DateTime AppointmentEndDate { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; }
        public bool OwnedByDoctor { get; set; }
        public string SessionKey { get; set; }
        public double SessionTime { get; set; }
        public long SessionPrice { get; set; }
        public string PatientNote { get; set; }
        public string DoctorNote { get; set; }
        public string MeetingLink { get; set; }

        public long? UserId { get; set; }
        public User User { get; set; }
        public long DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
