using RaceCarInspection.Shared.Models;

namespace RaceCarInspection.Server.Services
{
    public interface ISyncService
    {
        SyncManifest GetSyncManifest(string deviceName);
    }
}
