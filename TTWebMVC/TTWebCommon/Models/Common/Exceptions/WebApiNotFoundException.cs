using System;
using System.Collections.Generic;
using System.Text;

namespace TTWebCommon.Models.Common.Exceptions
{
    [Serializable]
    public class WebApiNotFoundException : Exception
    {
        public WebApiNotFoundException()
        {

        }

        public WebApiNotFoundException(string message) : base(message)
        {

        }
    }
}
