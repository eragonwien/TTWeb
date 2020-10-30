using System;

namespace TTWebCommon.Models.Common.Exceptions
{
    [Serializable]
    public class WebApiForbiddenException : Exception
    {
        public WebApiForbiddenException()
        {

        }

        public WebApiForbiddenException(string message, params object[] args) : base(string.Format(message, args))
        {

        }
    }
}
