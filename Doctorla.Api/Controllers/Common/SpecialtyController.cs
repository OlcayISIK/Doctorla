using Doctorla.Business.Abstract;
using Doctorla.Core;
using Doctorla.Dto;
using Doctorla.Dto.Auth;
using Doctorla.Dto.Members;
using Doctorla.Dto.Members.DoctorEntity;
using Doctorla.Dto.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doctorla.Api.Controllers.Common
{
    /// <summary>
    /// Authentication related endpoints
    /// </summary>
    [ApiController]
    [Route("api/common/[controller]")]
    [ApiExplorerSettings(GroupName = Constants.AuthenticationSchemes.Common)]

    public class SpecialtyController : Controller
    {
        private readonly ISpecialtyOperations _operations;

        /// <inheritdoc />
        public SpecialtyController(ISpecialtyOperations operations)
        {
            _operations = operations;
        }

        [HttpGet("getall")]
        public async Task<Result<IEnumerable<SpecialtyDto>>> GetAll()
        {
            return await _operations.GetAll();
        }

        [HttpPost("getDoctorWithSpecialities/{Id}")]
        public async Task<Result<IEnumerable<DoctorDto>>> GetDoctorWithSpecialities(long Id)
        {
            return await _operations.GetDoctorWithSpecialities(Id);
        }

        [HttpPost("get/{Id}")]
        public async Task<Result<SpecialtyDto>> Get(long Id)
        {
            return await _operations.Get(Id);
        }
    }
}
