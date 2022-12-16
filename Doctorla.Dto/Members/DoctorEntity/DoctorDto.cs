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
        public BatchDto<DoctorEducationDto> EducationsBatch { get; set; }
        public BatchDto<DoctorExperienceDto> ExperienceBatch { get; set; }
        public BatchDto<DoctorMedicalInterestDto> MedicalInterestBatch { get; set; }
        public BatchDto<DoctorScientificMembershipDto> ScientificMembershipBatch { get; set; }
    }
}
