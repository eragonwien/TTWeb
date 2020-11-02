using System;

namespace TTWeb.BusinessLogic.Models.Helpers
{
    public class ProcessingResult
    {
        public bool Succeed { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}