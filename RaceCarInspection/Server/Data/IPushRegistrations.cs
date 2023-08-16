using RaceCarInspection.Shared.Models;

namespace RaceCarInspection.Server.Data
{
    public interface IPushRegistrations
    {
        Task AddPushRegistration(PushRegistration pushRegistration);
        Task<List<PushRegistration>> GetAllPushRegistrations();
    }
}
