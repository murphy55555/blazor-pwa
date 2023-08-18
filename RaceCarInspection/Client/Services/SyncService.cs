using Blazored.LocalStorage;
using Microsoft.JSInterop;
using RaceCarInspection.Client.Models;
using RaceCarInspection.Client.Pages;
using RaceCarInspection.Shared.Models;
using System;
using System.IO;
using System.Net.Http.Json;
using System.Text;

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

        public async Task SyncData(string deviceName, Func<SyncEventArgs,Task> updateCallback)
        {
            await SendUpdate(updateCallback, "Starting Sync");
            var manifest = await http.GetFromJsonAsync<SyncManifest>($"api/sync/{deviceName}");
            await SendUpdate(updateCallback, $"Manifest downloaded. {manifest.NewInspections.Count} new inspections to download, {manifest.NewSOPs.Count} new SOPs to download");

            var totalItems = manifest.NewInspections.Count + manifest.NewSOPs.Count;
            var currentItem = 1;

            // Note: This is just a demo app. We're not really updating/syncing completed inspections
            var localInspections = await inspectionService.GetInspections();
            foreach (var inspection in manifest.NewInspections)
            {
                await inspectionService.SaveOrUpdateInspection(inspection);
                double percentComplete = (double)currentItem / (double)totalItems * 100.0;

                await SendUpdate(updateCallback,
                    $"Added inspection for car #{inspection.CarNumber} - {inspection.Year} {inspection.Make} {inspection.Model}",
                    (int)percentComplete);
                currentItem++;
            }

            foreach (var sop in manifest.NewSOPs)
            {
                var downloadedFileStream = await http.GetStreamAsync($"api/sync/download-sop/{sop.Id}");
                string base64Data;
                using (var binaryReader = new BinaryReader(downloadedFileStream))
                {
                    var bytes = binaryReader.ReadBytes((int)downloadedFileStream.Length);
                    base64Data = $"data:{sop.ContentType};base64,{Convert.ToBase64String(bytes)}";
                }

                await inspectionService.SaveOrUpdateSOP(sop);
                await inspectionService.SaveOrUpdateSOPData(new StandardOperatingProcedureData
                {
                    Id = sop.Id,
                     Base64Data = base64Data
                });

                double percentComplete = (double)currentItem / (double)totalItems * 100.0;
                await SendUpdate(updateCallback, $"Added SOP {sop.FileName}", (int)percentComplete);
                currentItem++;
            }

            await SendUpdate(updateCallback, "Sync Complete", 100);
        }

        public async Task SyncData(string deviceName, IJSInProcessRuntime jsRuntime)
        {
            // Just an example of using the experimental background fetch API. This is not a complete sync, just an example
            var manifest = await http.GetFromJsonAsync<SyncManifest>($"api/sync/{deviceName}");
            var sb = new StringBuilder("[");
            var count = 1;
            foreach (var sop in manifest.NewSOPs.Take(3).ToList())
            {
                if(manifest.NewSOPs.Count == count)
                {
                    sb.Append($"/api/sync/download-sop/{sop.Id}");
                }
                else
                {
                    sb.Append($"/api/sync/download-sop/{sop.Id},");
                }
                count++;
            }

            sb.Append(']');
            await jsRuntime.InvokeVoidAsync("fetchBackground", sb.ToString());
        }

        private async Task SendUpdate(Func<SyncEventArgs, Task> updateCallback, string message, int percentComplete = 0)
        {
            if (updateCallback != null)
            {
                await updateCallback(new SyncEventArgs { UpdateLogMessage = $"{DateTime.Now} {message}", ProgressCompletePercentage = percentComplete });
            }
        }
    }
}
