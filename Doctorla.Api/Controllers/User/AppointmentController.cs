using Doctorla.Business.Abstract;
using Doctorla.Core;
using Doctorla.Core.InternalDtos;
using Doctorla.Dto.Shared;
using Doctorla.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Doctorla.Core.Communication;
using Doctorla.Business.Helpers;

namespace Doctorla.Api.Controllers.User
{
    /// <summary>
    /// Appointment related endpoints
    /// </summary>
    [ApiController]
    [Route("api/user/[controller]")]
    [ApiExplorerSettings(GroupName = Constants.AuthenticationSchemes.User)]
    [Authorize(AuthenticationSchemes = Constants.AuthenticationSchemes.User)]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentOperations _operations;

        public AppointmentController(IAppointmentOperations operations)
        {
            _operations = operations;
        }

        /// <summary>
        /// Gets all appointments user has
        /// </summary>
        [HttpGet("getall")]
        public async Task<Result<IEnumerable<AppointmentDto>>> GetAll()
        {
            return await _operations.GetAllForUser();
        }

        /// <summary>
        /// Gets all available appointments of doctor
        /// </summary>
        [HttpGet("getavailableappointments/{doctorId}")]
        public async Task<Result<IEnumerable<AppointmentDto>>> GetAvailableAppointments(long doctorId)
        {
            return await _operations.GetAvailableAppointments(doctorId);
        }

        /// <summary>
        /// Adds an appointment
        /// </summary>
        [HttpPost("create")]
        public async Task<Result<bool>> CreateAppointment(AppointmentDto appointmentDto)
        {
            return await _operations.CreateAppointment(appointmentDto);
        }

        /// <summary>
        /// Adds an appointment
        /// </summary>
        [HttpPut("request")]
        public async Task<Result<bool>> RequestAppointment([FromBody]long appointmentId)
        {
            return await _operations.RequestAppointment(appointmentId);
        }

        /// <summary>
        /// Cancels an appointment
        /// </summary>
        [HttpPut("cancel")]
        public async Task<Result<bool>> CancelAppointment(long appointmentId)
        {
            return await _operations.CancelAppointmentForUser(appointmentId);
        }
    }
}
