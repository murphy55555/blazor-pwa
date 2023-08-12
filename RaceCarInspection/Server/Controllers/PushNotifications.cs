using Microsoft.AspNetCore.Mvc;
using RaceCarInspection.Shared.Models;

namespace RaceCarInspection.Server.Controllers
{
    [ApiController]
    [Route("api")]
    public class PushNotifications : Controller
    {
        [HttpPost("register-push")]
        public IActionResult RegisterPushNotification(PushRegistration pushRegistration)
        {
            return Ok();
        }
    }
}
