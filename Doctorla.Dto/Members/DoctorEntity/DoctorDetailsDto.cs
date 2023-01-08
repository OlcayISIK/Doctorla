using Doctorla.Data.Members.DoctorEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Dto.Members.DoctorEntity
{
    public class DoctorDetailsDto
    {
        public List<DoctorEducationDto> Educations { get; set; }
        public List<DoctorExperienceDto> Experiences { get; set; }
        public List<DoctorMedicalInterestDto> MedicalInterests { get; set; }
        public List<DoctorScientificMembershipDto> ScientificMemberships { get; set; }
    }
}
