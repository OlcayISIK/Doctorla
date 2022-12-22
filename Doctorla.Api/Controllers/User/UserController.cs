using Doctorla.Core;
using Doctorla.Dto.Members;
using Doctorla.Dto.Members.Profile;
using Doctorla.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Doctorla.Business.Abstract;
using Microsoft.AspNetCore.Authorization;

namespace Doctorla.Api.Controllers.User
{
    /// <summary>
    /// Appointment related endpoints
    /// </summary>
    [ApiController]
    [Route("api/user/[controller]")]
    [ApiExplorerSettings(GroupName = Constants.AuthenticationSchemes.User)]
    [Authorize(AuthenticationSchemes = Constants.AuthenticationSchemes.User)]
    public class UserController : Controller
    {

        private readonly IUserOperations _operations;

        public UserController(IUserOperations operations)
        {
            _operations = operations;
        }

        /// <summary>
        /// Get doctor
        /// </summary>
        [HttpGet("get")]
        public async Task<Result<UserDto>> Get()
        {
            return await _operations.Get();
        }

        [HttpPut("update")]
        public async Task<Result<bool>> Update(UserDto userDto)
        {
            return await _operations.UpdateForUser(userDto);
        }

        [HttpPut("changepassword")]
        public async Task<Result<bool>> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            return await _operations.ChangePassword(changePasswordDto);
        }
    }
}
