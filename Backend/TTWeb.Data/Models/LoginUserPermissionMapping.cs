namespace TTWeb.Data.Models
{
    public class LoginUserPermissionMapping
    {
        public int LoginUserId { get; set; }
        public LoginUser LoginUser { get; set; }
        public UserPermission UserPermission { get; set; }
    }
}