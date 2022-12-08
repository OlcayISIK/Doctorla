using Doctorla.Core.Enums;
using Doctorla.Data.EF;
using Doctorla.Data.Entities;
using Doctorla.Data.Members;
using Doctorla.Data.Shared;
using Doctorla.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Repository.Concrete
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(Context context) : base(context)
        {
        }

        public IQueryable<Appointment> GetAllAvailableAppointments()
        {
            return Context.Appointments.Where(x => !x.IsDeleted && (x.AppointmentStatus == AppointmentStatus.Active || x.AppointmentStatus == AppointmentStatus.Requested));
        }
    }
}
