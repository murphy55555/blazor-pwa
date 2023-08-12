using RaceCarInspection.Shared.Models;

namespace RaceCarInspection.Client.Services
{
    public interface IInspectionService
    {
        Task<List<Inspection>> GetInspections();
        Task<Inspection> GetInspection(int carNumber);
        Task<List<StandardOperatingProcedure>> GetStandardOperatingProcedures();
    }
}
