using RaceCarInspection.Server.Data;
using RaceCarInspection.Shared.Models;

namespace RaceCarInspection.Server.Services
{
    public class SyncService : ISyncService
    {
        private readonly ICarInspectionData carInspectionData;

        public SyncService(ICarInspectionData carInspectionData)
        {
            this.carInspectionData = carInspectionData;
        }
        public SyncManifest GetSyncManifest(string deviceName)
        {
            return new SyncManifest
            {
                NewSOPs = carInspectionData.GetStandardOperatingProcedures(),
                NewInspections = carInspectionData.GetInspections()
            };
        }
    }
}
