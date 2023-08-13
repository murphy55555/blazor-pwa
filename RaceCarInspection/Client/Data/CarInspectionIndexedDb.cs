using DnetIndexedDb;
using Microsoft.JSInterop;

namespace RaceCarInspection.Client.Data
{
    public class CarInspectionIndexedDb : IndexedDbInterop
    {
        public CarInspectionIndexedDb(IJSRuntime jsRuntime, IndexedDbOptions<CarInspectionIndexedDb> options)
        : base(jsRuntime, options)
        {
        }
    }
}
