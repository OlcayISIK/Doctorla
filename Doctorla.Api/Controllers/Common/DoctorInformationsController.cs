using Doctorla.Business.Abstract;
using Doctorla.Dto.Members.DoctorEntity;
using Doctorla.Dto.Shared;
using Doctorla.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doctorla.Api.Controllers.Common
{
    /// <summary>
    /// Authentication related endpoints
    /// </summary>
    [ApiController]
    [Route("api/doctor/[controller]")]
    public class DoctorInformationsController : Controller
    {
            private readonly IDoctorOperations _operations;

            /// <inheritdoc />
            public DoctorInformationsController(IDoctorOperations operations)
            {
                _operations = operations;
            }

            [HttpPost("get/{Id}")]
            public async Task<Result<DoctorDto>> Get(long Id)
            {
                return await _operations.Get(Id);
            }
    }
}
