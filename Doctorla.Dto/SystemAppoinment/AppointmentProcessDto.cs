using Doctorla.Dto.SystemAppoinment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Dto.SystemAppoinments
{
    public class AppointmentProcessDto
    {
        public int Id { get; set; }
        public DateTime IDate { get; set; }
        public int AppointmentId { get; set; }
        public bool IsDoctor { get; set; }
        public string ProcessMessage { get; set; }
        public int ProcessTypes { get; set; }
        public virtual User User { get; set; }
        public AppoinmentDto Appointment { get; set; }
    }
}
