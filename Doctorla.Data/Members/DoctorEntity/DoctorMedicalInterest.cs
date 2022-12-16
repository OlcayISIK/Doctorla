using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Doctorla.Data.Members.DoctorEntity
{
    public class DoctorMedicalInterest : Entity
    {
        public string Name { get; set; }
        public long DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
