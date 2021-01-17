using System;
using TTWeb.BusinessLogic.Models.Entities;

namespace TTWeb.Worker.ScheduleRunner.Services
{
    public interface IFacebookChromeDriverService
    {
        void Launch();
        void Close();
        void OpenStartPage();
        void Login(string username, string password);
        void AcceptCookieAgreement();
        void LikeNewestStory();
        void ByPassTwoFactorAuthentication(string seedCode);
        void NavigateToUserProfile(string userCode);
        void Sleep(TimeSpan? duration = null);
        string BuildLogMessage();
        void Start(FacebookUserModel sender);
        void Comment();
    }
}
