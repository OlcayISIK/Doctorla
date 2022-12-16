using Doctorla.Core.Enums;
using Doctorla.Data.EF;
using Doctorla.Data.Entities;
using Doctorla.Data.Members.DoctorEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Repository.Abstract
{
    public class DoctorExperiencesRepository : Repository<DoctorExperience>, IDoctorExperiencesRepository
    {
        public DoctorExperiencesRepository(Context context) : base(context)
        {
        }
    }
}
