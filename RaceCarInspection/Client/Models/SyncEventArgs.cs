namespace RaceCarInspection.Client.Models
{
    public class SyncEventArgs : EventArgs
    {
        public int ProgressCompletePercentage { get; set; }
        public string UpdateLogMessage { get; set; }
    }
}
