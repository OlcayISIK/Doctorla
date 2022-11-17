using Doctorla.Business.Abstract;
using Doctorla.Core;
using Doctorla.Dto;
using Doctorla.Dto.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Doctorla.Api.Controllers.Admin
{
    /// <summary>
    /// Authentication related endpoints
    /// </summary>
    [ApiController]
    [Route("api/admin/[controller]")]
    [ApiExplorerSettings(GroupName = Constants.AuthenticationSchemes.Admin)]
    public class AuthController
    {
        private readonly IAuthOperations _authOperations;

        /// <inheritdoc />
        public AuthController(IAuthOperations authOperations)
        {
            _authOperations = authOperations;
        }

        /// <summary>
        /// Authenticate via password
        /// </summary>
        [HttpPost("pass")]
        public async Task<Result<TokenDto>> AuthenticateViaPassword([FromBody] LoginDto loginDto)
        {
            return await _authOperations.AdminAuthenticateViaPassword(loginDto);
        }

        /// <summary>
        /// Authenticate via refresh token
        /// </summary>
        [HttpPost("token")]
        public async Task<Result<TokenDto>> AuthenticateViaToken([FromBody] TokenDto token)
        {
            return await _authOperations.AdminAuthenticateViaToken(token.RefreshToken);
        }

        /// <summary>
        /// Logout
        /// </summary>
        [HttpPost("logout")]
        public async Task<Result<bool>> Logout([FromBody] string refreshToken)
        {
            return await _authOperations.AdminLogout(refreshToken);
        }
    }
}
