using Doctorla.Data.Entities.Doc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Data.Members.DoctorEntity
{
    public class Doctor : Member
    {
        public string Title { get; set; } // Ünvan
        public string HospitalName { get; set; }
        public string Introduction { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Website { get; set; }
        //public int PageView { get; set; }
        //[NotMapped]
        //public int BlogView { get; set; }

        public List<DoctorEducations> DoctorEducations { get; set; }
        public List<DoctorExperiences> DoctorExperiences { get; set; }
        public List<DoctorMedicalInterests> DoctorMedicalInterests { get; set; }
        public List<DoctorScientificMembership> DoctorScientificMembership { get; set; }
    }
}
