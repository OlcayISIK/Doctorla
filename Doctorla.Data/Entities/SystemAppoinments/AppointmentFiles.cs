using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Doctorla.Data.Entities.SystemAppoinments
{
    public class AppointmentFiles : Entity
    {
        public string FileName { get; set; }
        public int AppointmentId { get; set; }
        public int UserId { get; set; }
        public bool IsDoctor { get; set; }
        [JsonIgnore]
        public Appointment Appointment { get; set; }
    }
}
