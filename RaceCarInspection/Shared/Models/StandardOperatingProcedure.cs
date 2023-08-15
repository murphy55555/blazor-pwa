using DnetIndexedDb;

namespace RaceCarInspection.Shared.Models
{
    public class StandardOperatingProcedure
    {
        [IndexDbKey]
        public int Id { get; set; }

        public string FileName { get; set; }
        public string Description { get; set; }
        public string ContentType { get; set; }
        public string FullPath { get; set; }

    }
}
