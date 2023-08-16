using Microsoft.Extensions.Caching.Memory;
using RaceCarInspection.Shared.Models;

namespace RaceCarInspection.Server.Data
{
    public class PushRegistrations : IPushRegistrations
    {
        private readonly IMemoryCache _memoryCache;
        private const string pushRegistrationsKey = "pushRegistrations";

        public PushRegistrations(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task AddPushRegistration(PushRegistration pushRegistration)
        {
            var pushRegistrations = _memoryCache.Get<List<PushRegistration>>(pushRegistrationsKey);
            if (pushRegistrations == null)
            {
                pushRegistrations = new List<PushRegistration>();
                _memoryCache.Set(pushRegistrationsKey, pushRegistrations);
            }

            if (!pushRegistrations.Any(p => p.DeviceName == pushRegistration.DeviceName))
            {
                pushRegistrations.Add(pushRegistration);
            }
        }

        public async Task<List<PushRegistration>> GetAllPushRegistrations()
        {
            return _memoryCache.Get<List<PushRegistration>>(pushRegistrationsKey) ?? new List<PushRegistration>();
        }
    }
}
