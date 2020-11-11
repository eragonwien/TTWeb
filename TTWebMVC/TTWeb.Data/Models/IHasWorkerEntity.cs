namespace TTWeb.Data.Models
{
    public interface IHasWorkerEntity
    {
        int? WorkerId { get; set; }
        LoginUser Worker { get; set; }
    }
}