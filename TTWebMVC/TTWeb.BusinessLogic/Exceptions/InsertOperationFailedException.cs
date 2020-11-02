using System;

namespace TTWeb.BusinessLogic.Exceptions
{
    [Serializable]
    public class InsertOperationFailedException : Exception
    {
        public InsertOperationFailedException()
            : base("Token is invalid")
        {
        }

        public InsertOperationFailedException(string message)
            : base(message)
        {
        }

        public InsertOperationFailedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}