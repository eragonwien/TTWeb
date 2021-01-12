using System;

namespace TTWeb.Worker.ScheduleRunner.Services
{
    public interface IFacebookChromeDriverService
    {
        void Launch();
        void Close();
        void OpenStartPage();
        void Login(string username, string password);
        void AcceptCookieAgreement();
        void Like(int likeCount, int maxPostCount);
        void ByPassTwoFactorAuthentication(string seedCode);
        void NavigateToUserProfile(string userCode);
        void Sleep(TimeSpan? duration = null);
    }
}
