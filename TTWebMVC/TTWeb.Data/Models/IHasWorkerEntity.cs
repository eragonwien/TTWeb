namespace TTWeb.Data.Models
{
    public interface IHasWorkerEntity
    {
        int? WorkerId { get; set; }
        Worker Worker { get; set; }
    }
}