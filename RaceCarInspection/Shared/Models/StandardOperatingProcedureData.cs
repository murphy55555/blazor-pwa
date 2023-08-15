using DnetIndexedDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceCarInspection.Shared.Models
{
    public class StandardOperatingProcedureData
    {
        [IndexDbKey]
        public int Id { get; set; }

        public string Base64Data { get; set; }
    }
}
