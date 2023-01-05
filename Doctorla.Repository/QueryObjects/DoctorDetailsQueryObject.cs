using Doctorla.Data.Members.DoctorEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Repository.QueryObjects
{
    public class DoctorDetailsQueryObject
    {
        public IQueryable<DoctorEducation> DoctorEducations { get; set; }
        public IQueryable<DoctorExperience> DoctorExperiences { get; set; }
        public IQueryable<DoctorMedicalInterest> DoctorMedicalInterests { get; set; }
        public IQueryable<DoctorScientificMembership> DoctorScientificMembership { get; set; }
    }
}
