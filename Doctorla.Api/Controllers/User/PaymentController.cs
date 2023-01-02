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
    public class PaymentController : Controller
    {
        private readonly IIyzicoOperations _operations;

        public PaymentController(IIyzicoOperations operations)
        {
            _operations = operations;
        }
        
        [HttpPost("payforappointment")]
        public async Task<Result<bool>> PayForAppointment(PaymentDto paymentDto)
        {
            return await _operations.PayForAppointment(paymentDto);
        }
    }
}
