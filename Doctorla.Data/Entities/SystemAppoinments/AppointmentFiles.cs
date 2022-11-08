using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Doctorla.Data.Entities.SystemAppointments
{
    public class AppointmentFiles : IBaseEntity
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
        [JsonIgnore]
        public Appointment Appointment { get; set; }
    }
}
