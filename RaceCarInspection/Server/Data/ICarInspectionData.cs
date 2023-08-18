using RaceCarInspection.Shared.Models;

namespace RaceCarInspection.Server.Data
{
    public interface ICarInspectionData
    {
        List<StandardOperatingProcedure> GetStandardOperatingProcedures();
        List<Inspection> GetInspections();
        List<string> GetDevicesDoingSync();
        void AddDeviceToSyncList(string device);
    }
}
