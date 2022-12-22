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
using Doctorla.Dto.Payment;

namespace Doctorla.Api.Controllers.User
{
    /// <summary>
    /// Appointment related endpoints
    /// </summary>
    [ApiController]
    [Route("api/user/[controller]")]
    [ApiExplorerSettings(GroupName = Constants.AuthenticationSchemes.User)]
    [Authorize(AuthenticationSchemes = Constants.AuthenticationSchemes.User)]
    public class IyzicoController : Controller
    {
        private readonly IIyzicoOperations _operations;
        private readonly AppSettings _appSettings;

        public IyzicoController(IIyzicoOperations operations, AppSettings appSettings)
        {
            _operations = operations;
            _appSettings = appSettings;
        }
        
        [HttpPost("payforappointment")]
        public async Task<Result<bool>> PayForAppointment(PaymentDto paymentDto)
        {
            return await _operations.PayForAppointment(paymentDto);
        }
    }
}
