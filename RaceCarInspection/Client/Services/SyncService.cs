using Blazored.LocalStorage;
using RaceCarInspection.Shared.Models;
using System.Net.Http.Json;

namespace RaceCarInspection.Client.Services
{
    public class SyncService : ISyncService
    {
        private readonly HttpClient http;
        private readonly ILocalStorageService localStorage;
        private readonly IInspectionService inspectionService;

        public SyncService(HttpClient Http, 
            ILocalStorageService localStorage,
            IInspectionService inspectionService)
        {
            http = Http;
            this.localStorage = localStorage;
            this.inspectionService = inspectionService;
        }

        public async Task SyncData()
        {
            var manifest = await http.GetFromJsonAsync<SyncManifest>("api/sync/heath");
        }
    }
}
