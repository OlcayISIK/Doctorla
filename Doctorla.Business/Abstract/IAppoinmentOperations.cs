using Doctorla.Data.Entities.SystemAppoinments;
using Doctorla.Dto;
using Doctorla.Dto.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Business.Abstract
{
    public interface IAppointmentOperations
    {
        #region User
        Task<Result<IEnumerable<AppointmentDto>>> GetAllForUser();
        Task<Result<bool>> RequestAppointment(long appointmentId);
        Task<Result<bool>> CancelAppointment(long appointmentId);
        #endregion

        #region Doctor
        Task<Result<IEnumerable<AppointmentDto>>> GetAllForDoctor();
        Task<Result<bool>> CreateAppointment(AppointmentDto appointmentDto);
        Task<Result<bool>> ApproveAppointment(long appointmentId);
        Task<Result<bool>> RejectAppointment(long appointmentId);
        #endregion
    }
}
