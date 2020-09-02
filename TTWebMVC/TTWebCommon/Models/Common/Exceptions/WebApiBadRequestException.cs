using System;
using System.Collections.Generic;
using System.Text;

namespace TTWebCommon.Models.Common.Exceptions
{
    [Serializable]
    public class WebApiBadRequestException : Exception
    {
        public WebApiBadRequestException()
        {

        }

        public WebApiBadRequestException(string message) : base(message)
        {

        }
    }
}
