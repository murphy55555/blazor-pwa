using RaceCarInspection.Server.Data;
using WebPush;

namespace RaceCarInspection.Server.Services
{
    public class PushNotificationService : IPushNotificationService
    {
        private readonly IPushRegistrations pushRegistrationData;

        public PushNotificationService(IPushRegistrations pushRegistrationData)
        {
            this.pushRegistrationData = pushRegistrationData;
        }

        public async Task SendPushNotifications(string message)
        {
            var subject = "mailto: <hmurphy55@hotmail.com>";
            var publicKey = "BMkGVTjSaIl7trQZkVeJmy9tswIjvz4woufuCoDPR37hKTDyLYcgRyi0svlz9bORzdnOOt5HVK5n9uIkU4hee-g";
            var privateKey = "dy9vRgTSKkoSbXsAAxPiNiHCABWmH1LYPVOHLRm8ysw";
            var vapidDetails = new VapidDetails(subject, publicKey, privateKey);
            var pushRegistrations = await pushRegistrationData.GetAllPushRegistrations();
            var webPushClient = new WebPushClient();

            foreach (var registration in pushRegistrations)
            {
                var subscription = new PushSubscription
                {
                    Endpoint = registration.Endpoint,
                    P256DH = registration.P256DH,
                    Auth = registration.Auth
                };
                await webPushClient.SendNotificationAsync(subscription, message, vapidDetails);
            }
        }
    }
}
