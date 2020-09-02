using System;
using System.Collections.Generic;
using System.Text;

namespace TTWebCommon.Models.Common.Exceptions
{
    [Serializable]
    public class WebApiForbiddenException : Exception
    {
        public WebApiForbiddenException()
        {

        }

        public WebApiForbiddenException(string message) : base(message)
        {

        }
    }
}
