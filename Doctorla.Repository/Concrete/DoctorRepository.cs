using Doctorla.Core.Enums;
using Doctorla.Data.EF;
using Doctorla.Data.Entities;
using Doctorla.Data.Members;
using Doctorla.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Repository.Concrete
{
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(Context context) : base(context)
        {
        }

        public IQueryable<Doctor> GetAllAvailableInGivenDate(DateTime date)
        {
            return from a in Context.Appointments
                   from d in Context.Doctors
                   where !a.IsDeleted && !d.IsDeleted
                   where a.AppointmentStatus == AppointmentStatus.Active || a.AppointmentStatus == AppointmentStatus.Requested
                   where a.AppointmentStartDate.Date == date.Date
                   where d.Id == a.DoctorId
                   select d;
        }
    }
}
