namespace TTWeb.BusinessLogic.Models.Entities.FacebookUser
{
    public class FacebookUserModel
    {
        public int Id { get; set; }
        public int LoginUserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public FacebookUserModel ClearPassword()
        {
            Password = string.Empty;
            return this;
        }
    }
}
