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
        /// Adds an appointment
        /// </summary>
        [HttpPost("request")]
        public async Task<Result<bool>> RequestAppointment(long appointmentId)
        {
            return await _operations.RequestAppointment(appointmentId);
        }

        /// <summary>
        /// Cancels an appointment
        /// </summary>
        [HttpPost("cancel")]
        public async Task<Result<bool>> CancelAppointment(long appointmentId)
        {
            return await _operations.CancelAppointment(appointmentId);
        }
    }
}
