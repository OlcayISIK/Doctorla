using Doctorla.Data.Entities;
using Doctorla.Data.Entities.SystemUsers;
using Doctorla.Data.Members.DoctorEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Repository.Abstract
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        IQueryable<Doctor> GetAllAvailableInGivenDate(DateTime date);
        IQueryable<Doctor> GetDoctorWithSpecialities(long specialtyId);
    }
}
