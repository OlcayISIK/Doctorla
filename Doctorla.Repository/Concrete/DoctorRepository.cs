using Doctorla.Core.Enums;
using Doctorla.Data.EF;
using Doctorla.Data.Entities;
using Doctorla.Data.Members.DoctorEntity;
using Doctorla.Repository.Abstract;
using Doctorla.Repository.QueryObjects;
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

        public DoctorDetailsQueryObject GetDoctorDetails(long doctorId)
        {
            var educations = from x in Context.DoctorEducations where !x.IsDeleted && x.DoctorId == doctorId select x;
            var experiences = from x in Context.DoctorExperiences where !x.IsDeleted && x.DoctorId == doctorId select x;
            var interestes = from x in Context.DoctorMedicalInterests where !x.IsDeleted && x.DoctorId == doctorId select x;
            var memberships = from x in Context.DoctorScientificMemberships where !x.IsDeleted && x.DoctorId == doctorId select x;

            return new DoctorDetailsQueryObject
            {
                DoctorEducations = educations,
                DoctorExperiences = experiences,
                DoctorMedicalInterests = interestes,
                DoctorScientificMembership = memberships,
            };
        }

        public IQueryable<Doctor> GetDoctorWithSpecialities(long specialtyId)
        {
            var doctors = Context.Doctors.Where(x => !x.IsDeleted && x.SpecialtyId == specialtyId);
            return doctors;
        }
    }
}
