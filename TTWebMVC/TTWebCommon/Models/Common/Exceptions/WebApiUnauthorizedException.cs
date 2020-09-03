﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TTWebCommon.Models.Common.Exceptions
{
    [Serializable]
    public class WebApiUnauthorizedException : Exception
    {
        public WebApiUnauthorizedException()
        {

        }

        public WebApiUnauthorizedException(string message, params object[] args) : base(string.Format(message, args))
        {

        }
    }
}
