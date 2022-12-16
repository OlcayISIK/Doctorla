using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Doctorla.Data.Members;

namespace Doctorla.Data.Entities.Doc
{
    public class DoctorDetail : Entity
    {
        public string Title { get; set; } // Ünvan
        public string HospitalName { get; set; }
        public string UniverstyName { get; set; }
        public string Introduction { get; set; }
        public string facebook { get; set; }
        public string instagram { get; set; }
        public string website { get; set; }
        public int PageView { get; set; }
        [NotMapped]
        public int BlogView { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }

        public List<DoctorEducations> DoctorEducations { get; set; }
        public List<DoctorExperiences> DoctorExperiences { get; set; }
        public List<DoctorMedicalInterests> DoctorMedicalInterests { get; set; }
        public List<DoctorScientificMembership> DoctorScientificMembership { get; set; }
    }
}
