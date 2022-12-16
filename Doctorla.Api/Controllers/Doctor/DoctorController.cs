using Doctorla.Business.Abstract;
using Doctorla.Core;
using Doctorla.Dto.Shared;
using Doctorla.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Doctorla.Dto.Members;

namespace Doctorla.Api.Controllers.Doctor
{
    /// <summary>
    /// Appointment related endpoints
    /// </summary>
    [ApiController]
    [Route("api/doctor/[controller]")]
    [ApiExplorerSettings(GroupName = Constants.AuthenticationSchemes.Doctor)]
    public class DoctorController : Controller
    {
        private readonly IDoctorOperations _operations;

        public DoctorController(IDoctorOperations operations)
        {
            _operations = operations;
        }

        /// <summary>
        /// Get doctor
        /// </summary>
        [HttpGet("get")]
        public async Task<Result<DoctorDto>> Get(long id)
        {
            return await _operations.GetWithDetails(id);
        }
    }
}
