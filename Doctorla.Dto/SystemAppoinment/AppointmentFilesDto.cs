using Doctorla.Dto.SystemAppoinment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Dto.SystemAppoinment
{
    public class AppointmentFilesDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime IDate { get; set; }
        public int IUser { get; set; }
        public DateTime? UUDate { get; set; }
        public int? UUser { get; set; }
        public string FileName { get; set; }
        public int AppointmentId { get; set; }
        public int UserId { get; set; }
        public bool IsDoctor { get; set; }
        public AppoinmentDto Appointment { get; set; }
    }
}
