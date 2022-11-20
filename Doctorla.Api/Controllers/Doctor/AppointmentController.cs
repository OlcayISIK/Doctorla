using Doctorla.Business.Abstract;
using Doctorla.Business.Concrete;
using Doctorla.Core;
using Doctorla.Core.InternalDtos;
using Doctorla.Dto.Auth;
using Doctorla.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Doctorla.Dto.Shared;
using System.Collections;
using System.Collections.Generic;

namespace Doctorla.Api.Controllers.Doctor
{
    /// <summary>
    /// Appointment related endpoints
    /// </summary>
    [ApiController]
    [Route("api/doctor/[controller]")]
    [ApiExplorerSettings(GroupName = Constants.AuthenticationSchemes.Doctor)]
    public class AppointmentController : Controller
    {
        private readonly IAppoinmentOperations _operations;

        public AppointmentController(IAppoinmentOperations operations)
        {
            _operations = operations;
        }

        /// <summary>
        /// Gets all appointments doctor has
        /// </summary>
        [HttpGet("getall")]
        public async Task<Result<IEnumerable<AppointmentDto>>> GetAll()
        {
            return await _operations.GetAll();
        }

        /// <summary>
        /// Approve an appointment for doctor
        /// </summary>
        [HttpGet("accept")]
        public async Task<Result<bool>> ApproveAppointment(long appointmentId)
        {
            return await _operations.ApproveAppointment(appointmentId);
        }

        /// <summary>
        /// Rejects an appointment for doctor
        /// </summary>
        [HttpGet("reject")]
        public async Task<Result<bool>> RejectAppointment(long appointmentId)
        {
            return await _operations.RejectAppointment(appointmentId);
        }

    }
}
