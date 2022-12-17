using Doctorla.Core;
using Microsoft.AspNetCore.Mvc;

namespace Doctorla.Api.Controllers.User
{
    /// <summary>
    /// Appointment related endpoints
    /// </summary>
    [ApiController]
    [Route("api/user/[controller]")]
    [ApiExplorerSettings(GroupName = Constants.AuthenticationSchemes.User)]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
