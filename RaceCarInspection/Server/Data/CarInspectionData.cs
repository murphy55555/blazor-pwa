using Microsoft.Extensions.Caching.Memory;
using RaceCarInspection.Shared.Models;

namespace RaceCarInspection.Server.Data
{
    public class CarInspectionData : ICarInspectionData
    {
        private readonly IMemoryCache _memoryCache;
        private const string devicesSyncing = "devicesSyncing";

        public CarInspectionData(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
        }

        public void AddDeviceToSyncList(string device)
        {
            var devices = _memoryCache.Get<List<string>>(devicesSyncing);
            if (devices == null)
            {
                devices = new List<string>();
                _memoryCache.Set(devicesSyncing, devices, DateTime.Now.AddSeconds(30));
            }

            if (!devices.Any(d => d == device))
            {
                devices.Add($"Device {device} last sync activity {DateTime.Now}");
            }
        }

        public List<string> GetDevicesDoingSync()
        {
            return _memoryCache.Get<List<string>>(devicesSyncing) ?? new List<string>();
        }

        public List<Inspection> GetInspections()
        {
            return new List<Inspection>
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
                    new Inspection()
                    {
                        CarNumber = 86,
                        Make = "Subaru",
                        Model = "BRZ",
                        Year = 2019,
                        Color = "White"
                    },
                    new Inspection()
                    {
                        CarNumber = 3,
                        Make = "Tesla",
                        Model = "Model 3 Performance",
                        Year = 2020,
                        Color = "Blue"
                    }
                };
        }

        public List<StandardOperatingProcedure> GetStandardOperatingProcedures()
        {
            return new List<StandardOperatingProcedure>
                {
                    new StandardOperatingProcedure()
                    {
                        Id = 1,
                        FileName = "Fire Safety.pdf",
                        Description = "Manual that describes the fire safety rules",
                        ContentType = "application/pdf",
                        FullPath = "/SyncContent/Car_Fire_Safety.pdf"
                    },
                    new StandardOperatingProcedure()
                    {
                        Id = 2,
                        FileName = "Inspection Criteria.pdf",
                        Description = "Manual that describes the inspection criteria",
                        ContentType = "application/pdf",
                        FullPath = "/SyncContent/checklist.pdf"
                    },
                    new StandardOperatingProcedure()
                    {
                        Id = 3,
                        FileName = "Worn vs Good Rotors & Pads.jpg",
                        Description = "Good brakes are a must when racing. See examples of good vs bad.",
                        ContentType = "image/jpeg",
                        FullPath = "/SyncContent/brakes.jpg"
                    },
                    new StandardOperatingProcedure()
                    {
                        Id = 4,
                        FileName = "Engine Sound.wav",
                        Description = "Listen to the sound of a great engine",
                        ContentType = "audio/wav",
                        FullPath = "/SyncContent/engine.wav"
                    },
                    new StandardOperatingProcedure()
                    {
                        Id = 5,
                        FileName = "A race example.mp4",
                        Description = "Just in case you are a new instructor, this is racing. =)",
                        ContentType = "video/mp4",
                        FullPath = "/SyncContent/racing.mp4"
                    }
                };
        }
    }
}
