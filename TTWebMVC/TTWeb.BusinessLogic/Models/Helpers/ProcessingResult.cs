using System;

namespace TTWeb.BusinessLogic.Models.Helpers
{
    public class ProcessingResult<TResult>
    {
        public bool Succeed { get; set; }
        public string Message { get; set; }
        public string Reason
        {
            get => _reason ?? "unknown reason";
            set => _reason = value ?? _reason;
        }

        private string _reason;

        public Exception Exception { get; set; }
        public TResult Result { get; set; }

        public ProcessingResult(string reason = null,
            bool succeed = false,
            string message = null,
            Exception exception = null,
            TResult result = default)
        {
            _reason = reason;
            Succeed = succeed;
            Message = message;
            Exception = exception;
            Result = result;
        }

        public ProcessingResult<TResult> WithReason(string reason)
        {
            Reason = reason;
            return this;
        }

        public ProcessingResult<TResult> WithSuccess(bool succeed = true)
        {
            Succeed = succeed;
            return this;
        }

        public ProcessingResult<TResult> WithResult(TResult result)
        {
            if (result == null)
            {
                Succeed = false;
                Reason = "result is empty";
            }
            Result = result;
            return this;
        }
    }
}