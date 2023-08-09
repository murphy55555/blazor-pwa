using RaceCarInspection.Client.Models;

namespace RaceCarInspection.Client.Services
{
    public interface IInspectionService
    {
        Task<List<Inspection>> GetInspections();
    }
}
