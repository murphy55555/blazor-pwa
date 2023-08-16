namespace RaceCarInspection.Server.Services
{
    public interface IPushNotificationService
    {
        Task SendPushNotifications(string message);
    }
}
