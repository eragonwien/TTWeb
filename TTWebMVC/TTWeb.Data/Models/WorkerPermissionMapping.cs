namespace TTWeb.Data.Models
{
    public class WorkerPermissionMapping
    {
        public int WorkerId { get; set; }
        public Worker Worker { get; set; }
        public UserPermission UserPermission { get; set; }
    }
}
