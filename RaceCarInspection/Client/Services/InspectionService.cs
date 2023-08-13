using RaceCarInspection.Client.Data;
using RaceCarInspection.Shared.Models;

namespace RaceCarInspection.Client.Services
{
    public class InspectionService : IInspectionService
    {
        private readonly CarInspectionIndexedDb indexedDb;

        public InspectionService(CarInspectionIndexedDb indexedDb)
        {
            this.indexedDb = indexedDb;
        }

        public async Task CompleteInspection(Inspection inspection)
        {
            await indexedDb.UpdateItems(new List<Inspection> { inspection });
        }

        public async Task<Inspection> GetInspection(int carNumber)
        {
            return await indexedDb.GetByKey<int, Inspection>(carNumber);
        }

        public async Task<List<Inspection>> GetInspections()
        {
            //await AddInspectionsToLocalDB();

            //return new List<Inspection>()
            //{
            //    new Inspection()
            //    {
            //        CarNumber = 1,
            //        Make = "Mitsubishi",
            //        Model = "Lancer Evolution X MR",
            //        Year = 2015,
            //        Color = "Red"
            //    },
            //    new Inspection()
            //    {
            //        CarNumber = 36,
            //        Make = "Nissan",
            //        Model = "GTR",
            //        Year = 2020,
            //        Color = "Grey"
            //    },
            //};
            return await indexedDb.GetAll<Inspection>();
        }

        public async Task AddInspectionsToLocalDB()
        {
            var inspections = new List<Inspection>()
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
            };
            await indexedDb.AddItems(inspections);
        }

        public Task<List<StandardOperatingProcedure>> GetStandardOperatingProcedures()
        {
            return Task.FromResult(new List<StandardOperatingProcedure>()
            {
                new StandardOperatingProcedure()
                {
                    Id = 1,
                    FileName = "Fire Safety",
                    Description = "Manual that describes the fire safety rules",
                    FileType = "pdf",
                    FullPath = "/Car_Fire_Safety.pdf"
                },
                new StandardOperatingProcedure()
                {
                    Id = 2,
                    FileName = "Inspection Criteria",
                    Description = "Manual that describes the inspection criteria",
                    FileType = "pdf",
                    FullPath = "/checklist.pdf"
                },
                new StandardOperatingProcedure()
                {
                    Id = 3,
                    FileName = "Worn vs Good Rotors & Pads",
                    Description = "Good brakes are a must when racing. See examples of good vs bad.",
                    FileType = "jpg",
                    FullPath = "/brakes.jpg"
                },
                new StandardOperatingProcedure()
                {
                    Id = 4,
                    FileName = "Engine Sound",
                    Description = "Listen to the sound of a great engine",
                    FileType = "wav",
                    FullPath = "/engine.wav"
                },
                new StandardOperatingProcedure()
                {
                    Id = 5,
                    FileName = "A race example",
                    Description = "Just in case you are a new instructor, this is racing. =)",
                    FileType = "mp4",
                    FullPath = "/racing.mp4"
                }
            });
        }

        public async Task Initialize()
        {
            await indexedDb.OpenIndexedDb();
        }
    }
}
