using Doctorla.Business.Abstract;
using Doctorla.Business.Concrete;
using Doctorla.Core;
using Doctorla.Core.InternalDtos;
using Doctorla.Dto.Auth;
using Doctorla.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Doctorla.Dto.Shared;
using System.Collections;
using Doctorla.Dto.Members.DoctorEntity;
using Doctorla.Dto.Shared.Blog;
using Doctorla.Dto.Members.Profile;

namespace Doctorla.Api.Controllers.Doctor
{
    /// <summary>
    /// Appointment related endpoints
    /// </summary>
    [ApiController]
    [Route("api/doctor/[controller]")]
    [ApiExplorerSettings(GroupName = Constants.AuthenticationSchemes.Doctor)]
    [Authorize(AuthenticationSchemes = Constants.AuthenticationSchemes.Doctor)]
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
        public async Task<Result<DoctorDto>> Get()
        {
            return await _operations.Get();
        }

        [HttpPut("update")]
        public async Task<Result<bool>> Update(DoctorDto doctorDto)
        {
            return await _operations.UpdateFoDoctor(doctorDto);
        }

        [HttpPut("changepassword")]
        public async Task<Result<bool>> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            return await _operations.ChangePassword(changePasswordDto);
        }
    }
}
