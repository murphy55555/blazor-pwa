using RaceCarInspection.Client.Models;

namespace RaceCarInspection.Client.Services
{
    public class InspectionService : IInspectionService
    {
        public Task<List<Inspection>> GetInspections()
        {
            return Task.FromResult(new List<Inspection>()
            {
                new Inspection()
                {
                    CarNumber = 1,
                    Make = "Mitsubishi",
                    Model = "Lancer Evolution X MR",
                    Year = 2015,
                    Color = "Red"
                },
                new Inspection()
                {
                    CarNumber = 36,
                    Make = "Nissan",
                    Model = "GTR",
                    Year = 2020,
                    Color = "Grey"
                },
            });
        }
    }
}
