using RaceCarInspection.Client.Models;
using RaceCarInspection.Shared.Models;

namespace RaceCarInspection.Client.Services
{
    public interface ISyncService
    {
        Task SyncData(string deviceName, Func<SyncEventArgs, Task> updateCallback);
    }
}
