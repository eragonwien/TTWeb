using System;

namespace TTWebCommon.Models.Common.Exceptions
{
    [Serializable]
    public class WebApiNotFoundException : Exception
    {
        public WebApiNotFoundException()
        {
        }

        public WebApiNotFoundException(string message, params object[] args) : base(string.Format(message, args))
        {
        }
    }
}