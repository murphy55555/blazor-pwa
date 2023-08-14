using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceCarInspection.Shared.Models
{
    public class SyncManifest
    {
        public List<StandardOperatingProcedure> NewSOPs { get; set; }
        public List<Inspection> NewInspections { get; set; }
    }
}
