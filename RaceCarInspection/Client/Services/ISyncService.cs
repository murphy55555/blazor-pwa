using Microsoft.JSInterop;
using RaceCarInspection.Client.Models;

namespace RaceCarInspection.Client.Services
{
    public interface ISyncService
    {
        Task SyncData(string deviceName, Func<SyncEventArgs, Task> updateCallback);
        Task SyncData(string deviceName, IJSInProcessRuntime jsRuntime);
    }
}
