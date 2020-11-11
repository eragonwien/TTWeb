namespace TTWeb.Data.Models
{
    public interface IUserOwnedEntity
    {
        int OwnerId { get; set; }
        LoginUser Owner { get; set; }
    }
}
