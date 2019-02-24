using EventAppBackend_RestAPI.Integrations;
using EventAppBackend_RestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EventAppBackend_RestAPI.Controllers
{
    [Route("api/SendPushNotification")]
    [ApiController]
    public class PushNotificationController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly FcmService fcmService;

        public PushNotificationController(IConfiguration config, FcmService fcmService)
        {
            this.config = config;
            this.fcmService = fcmService;
        }

        [HttpPost]
        public ActionResult<string> SendPushNotification(PushNotificationRequest req)
        {
            var ocPassword = config.GetValue<string>("OCPassword");

            if (ocPassword == req.OcPassword)
            {
                var notificationTitle = req?.Title;
                var notificationBody = req?.Body;
                if (!string.IsNullOrEmpty(notificationTitle) && !string.IsNullOrEmpty(notificationBody))
                {
                    this.fcmService.SendPushNotification(notificationTitle, notificationBody);
                    return Ok("Push notification sent succesfully");
                }
                else
                {
                    return BadRequest($"Incorrect push notification title: {notificationTitle} or body: {notificationBody}");
                }
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
