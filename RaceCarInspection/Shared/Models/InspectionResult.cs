using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceCarInspection.Shared.Models
{
    public class InspectionResult
    {
        public bool TiresPass { get; set; }
        public bool ChassisPass { get; set; }
        public bool BodyPass { get; set; }
        public bool SteeringPass { get; set; }
        public bool EnginePass { get; set; }
        public bool HelmetPass { get; set; }
        public string OverallComments { get; set; }
        public Geolocation Geolocation { get; set; } = new Geolocation();
        public DateTime? Started { get; set; }
        public DateTime? Completed { get; set; }

        public List<SupportingMediaFile> SupportingMediaFiles { get; set; } = new List<SupportingMediaFile>();
    }
}
