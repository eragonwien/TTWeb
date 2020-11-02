using System;

namespace TTWebCommon.Models.Common.Exceptions
{
    [Serializable]
    public class WebApiDuplicateInsertException : Exception
    {
        public WebApiDuplicateInsertException()
        {
        }

        public WebApiDuplicateInsertException(string message, params object[] args) : base(string.Format(message, args))
        {
        }
    }
}