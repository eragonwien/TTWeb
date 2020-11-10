namespace TTWeb.Data.Models
{
    public enum ProcessingStatus
    {
        New = 1,
        InProgress = 2,
        Retry = 3,
        Error = 4,
        Paused = 5
    }
}