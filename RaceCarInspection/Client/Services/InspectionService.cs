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
            return await indexedDb.GetAll<Inspection>();
        }

        public async Task SaveOrUpdateInspection(Inspection inspection)
        {
            var localCopy = await indexedDb.GetByKey<int, Inspection>(inspection.CarNumber);
            if (localCopy != null)
            {
                await indexedDb.DeleteByKey<int, Inspection>(inspection.CarNumber);
            }

            await indexedDb.AddItems(new List<Inspection> { inspection });
        }

        public async Task<List<StandardOperatingProcedure>> GetStandardOperatingProcedures()
        {
            return await indexedDb.GetAll<StandardOperatingProcedure>();
        }

        public async Task<StandardOperatingProcedure> GetStandardOperatingProcedure(int id)
        {
            return await indexedDb.GetByKey<int, StandardOperatingProcedure>(id);
        }

        public async Task Initialize()
        {
            await indexedDb.OpenIndexedDb();
        }

        public async Task SaveOrUpdateSOP(StandardOperatingProcedure sop)
        {
            var localCopy = await indexedDb.GetByKey<int, StandardOperatingProcedure>(sop.Id);
            if (localCopy == null)
            {
                await indexedDb.AddItems(new List<StandardOperatingProcedure> { sop });
            }
        }

        public async Task SaveOrUpdateSOPData(StandardOperatingProcedureData standardOperatingProcedureData)
        {
            var localCopy = await indexedDb.GetByKey<int, StandardOperatingProcedureData>(standardOperatingProcedureData.Id);
            if (localCopy == null)
            {
                await indexedDb.AddItems(new List<StandardOperatingProcedureData> { standardOperatingProcedureData });
            }
        }

        public async Task<StandardOperatingProcedureData> GetStandardOperatingProcedureData(int id)
        {
            return await indexedDb.GetByKey<int, StandardOperatingProcedureData>(id);
        }
    }
}
