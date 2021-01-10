namespace TTWeb.Worker.ScheduleRunner.Services
{
    public interface ITwoFactorAuthenticationService
    {
        string GetCode(string seed);
    }
}