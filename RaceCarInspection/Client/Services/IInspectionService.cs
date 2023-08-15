using RaceCarInspection.Shared.Models;

namespace RaceCarInspection.Client.Services
{
    public interface IInspectionService
    {
        Task Initialize();
        Task<List<Inspection>> GetInspections();
        Task<Inspection> GetInspection(int carNumber);
        Task<List<StandardOperatingProcedure>> GetStandardOperatingProcedures();
        Task<StandardOperatingProcedure> GetStandardOperatingProcedure(int id);
        Task<StandardOperatingProcedureData> GetStandardOperatingProcedureData(int id);
        Task CompleteInspection(Inspection inspection);
        Task SaveOrUpdateInspection(Inspection inspection);
        Task SaveOrUpdateSOP(StandardOperatingProcedure sop);
        Task SaveOrUpdateSOPData(StandardOperatingProcedureData standardOperatingProcedureData);
    }
}
