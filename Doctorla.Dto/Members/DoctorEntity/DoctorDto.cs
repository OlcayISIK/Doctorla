using Doctorla.Data.Members.DoctorEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Dto.Members.DoctorEntity
{
    public class DoctorDto
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Title { get; set; }
        public string IdentificationNumber { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Introduction { get; set; }
        public string Specialty { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Website { get; set; }
        public BatchDto<DoctorEducationDto> EducationsBatch { get; set; }
        public BatchDto<DoctorExperienceDto> ExperienceBatch { get; set; }
        public BatchDto<DoctorMedicalInterestDto> MedicalInterestBatch { get; set; }
        public BatchDto<DoctorScientificMembershipDto> ScientificMembershipBatch { get; set; }
    }
}
