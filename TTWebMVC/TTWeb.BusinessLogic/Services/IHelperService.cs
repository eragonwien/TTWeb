namespace TTWeb.BusinessLogic.Services
{
    public interface IHelperService
    {
        string GetRandomString(int length);
        string GetOtpCode(string seed);
    }
}