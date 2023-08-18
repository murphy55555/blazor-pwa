using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.X509;
using RaceCarInspection.Server.Data;
using RaceCarInspection.Server.Services;

namespace RaceCarInspection.Server.Controllers
{
    [ApiController]
    [Route("api/sync")]
    public class SyncController : Controller
    {
        private readonly ISyncService syncService;
        private readonly ICarInspectionData carInspectionData;

        public SyncController(ISyncService syncService, ICarInspectionData carInspectionData)
        {
            this.syncService = syncService;
            this.carInspectionData = carInspectionData;
        }

        [HttpGet("{devicename}")]
        public IActionResult Sync(string devicename)
        {
            // Note: This is just a demo app. Sync is hard and this flow doesn't account for many real-world concerns
            carInspectionData.AddDeviceToSyncList(devicename);
            return Ok(syncService.GetSyncManifest(devicename));
        }

        [HttpGet("download-sop/{id}")]
        public IActionResult DownloadSOP(int id)
        {
            var sop = carInspectionData.GetStandardOperatingProcedures().FirstOrDefault(s => s.Id == id);
            return File(sop.FullPath, sop.ContentType, sop.FileName);
        }

        [HttpGet("inspection/{carnumber}")]
        public IActionResult GetInspection(int carnumber)
        {
            var inspection = carInspectionData.GetInspections().FirstOrDefault(i => i.CarNumber == carnumber);
            return Ok(inspection);
        }

        [HttpPost("inspection-result/{deviceName}")]
        public IActionResult PostInspectionResults(string deviceName)
        {
            carInspectionData.AddDeviceToSyncList(deviceName);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetDevicesSyncing()
        {
            var devices = carInspectionData.GetDevicesDoingSync();
            return View("DevicesSyncing", devices);
        }
    }
}
