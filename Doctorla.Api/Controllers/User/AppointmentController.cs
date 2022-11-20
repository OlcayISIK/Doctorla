using Doctorla.Core;
using Doctorla.Core.InternalDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        IyzicoAPI configIyzicoAPI;
        //private readonly IAppointmentService _appointmentService;
    }
}
