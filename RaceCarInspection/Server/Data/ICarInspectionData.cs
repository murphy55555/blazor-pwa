using RaceCarInspection.Shared.Models;

namespace RaceCarInspection.Server.Data
{
    public interface ICarInspectionData
    {
        List<StandardOperatingProcedure> GetStandardOperatingProcedures();
        List<Inspection> GetInspections();
    }
}
