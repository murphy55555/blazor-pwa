using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceCarInspection.Shared.Models
{
    public class BatteryStatus
    {
        public bool Charging { get; set; }
        public int DischargingTime { get; set; }
        public double Level { get; set; }
    }
}
