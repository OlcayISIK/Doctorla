using Doctorla.Business.Abstract;
using Doctorla.Core;
using Doctorla.Dto;
using Doctorla.Dto.Auth;
using Doctorla.Dto.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doctorla.Api.Controllers.Admin
{
    /// <summary>
    /// Authentication related endpoints
    /// </summary>
    [ApiController]
    [Route("api/admin/[controller]")]
    [ApiExplorerSettings(GroupName = Constants.AuthenticationSchemes.Admin)]
    [Authorize(AuthenticationSchemes = Constants.AuthenticationSchemes.Admin)]
    public class SpecialtyController
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

        [HttpPost("add")]
        public async Task<Result<bool>> Add(SpecialtyDto specialtyDto)
        {
            return await _operations.Add(specialtyDto);
        }

        [HttpPut("update")]
        public async Task<Result<bool>> Update(SpecialtyDto specialtyDto)
        {
            return await _operations.Update(specialtyDto);
        }

        [HttpDelete("delete")]
        public async Task<Result<bool>> Delete(long id)
        {
            return await _operations.Delete(id);
        }
    }
}
