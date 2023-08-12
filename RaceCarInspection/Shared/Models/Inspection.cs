namespace RaceCarInspection.Shared.Models
{
    public class Inspection
    {
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int CarNumber { get; set; }
        public string Color { get; set; }

        public InspectionResult InspectionResult { get; set; } = new InspectionResult();
    }
}