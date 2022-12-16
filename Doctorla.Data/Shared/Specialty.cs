using Doctorla.Data.Members.DoctorEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Data.Shared
{
    public class Specialty : Entity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public List<Doctor> Doctors { get; set; }
    }
}
