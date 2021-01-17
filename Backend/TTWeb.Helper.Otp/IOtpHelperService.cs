namespace TTWeb.Helper.Otp
{
    public interface IOtpHelperService
    {
        string GetCode(string seed);
    }
}