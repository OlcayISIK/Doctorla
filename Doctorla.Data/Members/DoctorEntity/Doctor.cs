using Doctorla.Data.Shared;
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
        public string Introduction { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Website { get; set; }
        //public int PageView { get; set; }
        //[NotMapped]
        //public int BlogView { get; set; }
        public long SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }
        public List<DoctorEducation> DoctorEducations { get; set; }
        public List<DoctorExperience> DoctorExperiences { get; set; }
        public List<DoctorMedicalInterest> DoctorMedicalInterests { get; set; }
        public List<DoctorScientificMembership> DoctorScientificMembership { get; set; }
    }
}
