namespace TTWeb.BusinessLogic.Models.Entities
{
    public class ScheduleFacebookUserModel : BaseUserOwnedModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string SeedCode { get; set; }

        public string ProfileAddress { get; set; }
    }
}