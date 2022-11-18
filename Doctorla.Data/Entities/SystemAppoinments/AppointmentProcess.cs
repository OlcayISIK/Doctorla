using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Doctorla.Data.Entities.SystemUsers;

namespace Doctorla.Data.Entities.SystemAppoinments
{
    public class AppointmentProcess
    {
        public int Id { get; set; }
        public DateTime IDate { get; set; }
        public int AppointmentId { get; set; }
        public bool IsDoctor { get; set; }
        public string ProcessMessage { get; set; }
        public int ProcessTypes { get; set; }
        public virtual User User { get; set; }
        [JsonIgnore]
        public Appointment Appointment { get; set; }
    }
}
