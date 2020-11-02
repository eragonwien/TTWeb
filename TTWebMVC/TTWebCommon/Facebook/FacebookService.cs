using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using OpenQA.Selenium.Firefox;

namespace TTWebCommon.Facebook
{
    public interface IFacebookService
    {
        void Execute(FacebookServiceParameter parameter);
    }

    public class FacebookService : IFacebookService
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public FacebookService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
#if !DEBUG
         Options.AddArgument("--headless");
#endif
            Options.AddArgument("--disable-notifications");
        }

        public FirefoxOptions Options { get; set; } = new FirefoxOptions();

        public void Execute(FacebookServiceParameter parameter)
        {
            using (var browser = new FirefoxDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                switch (parameter.ActionType)
                {
                    case FacebookServiceActionType.LOGIN:
                        break;
                    case FacebookServiceActionType.LIKE:
                        break;
                    case FacebookServiceActionType.COMMENT:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}