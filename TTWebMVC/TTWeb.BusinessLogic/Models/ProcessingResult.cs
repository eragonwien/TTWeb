using System;
using System.Collections.Generic;
using System.Text;

namespace TTWeb.BusinessLogic.Models
{
    public class ProcessingResult
    {
        public bool Succeed { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}
