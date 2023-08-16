using Microsoft.AspNetCore.Mvc;
using RaceCarInspection.Server.Data;
using RaceCarInspection.Server.Services;
using RaceCarInspection.Shared.Models;

namespace RaceCarInspection.Server.Controllers
{
    [ApiController]
    [Route("api")]
    public class PushNotifications : Controller
    {
        private readonly IPushRegistrations pushRegistrations;
        private readonly IPushNotificationService pushNotificationService;

        public PushNotifications(IPushRegistrations pushRegistrations,
            IPushNotificationService pushNotificationService)
        {
            this.pushRegistrations = pushRegistrations;
            this.pushNotificationService = pushNotificationService;
        }

        [HttpPost("register-push")]
        public IActionResult RegisterPushNotification(PushRegistration pushRegistration)
        {
            pushRegistrations.AddPushRegistration(pushRegistration);
            return Ok();
        }

        [HttpGet("push")]
        public async Task<IActionResult> Get()
        {
            ViewBag.Sent = false;
            return View(await pushRegistrations.GetAllPushRegistrations());
        }

        [HttpPost("push")]
        public async Task<IActionResult> Post([FromForm] string message)
        {
            await pushNotificationService.SendPushNotifications(message);
            ViewBag.Sent = true;
            return View("Get", await pushRegistrations.GetAllPushRegistrations());
        }
    }
}
