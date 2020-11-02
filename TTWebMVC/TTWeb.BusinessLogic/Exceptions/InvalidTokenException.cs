using System;

namespace TTWeb.BusinessLogic.Exceptions
{
    [Serializable]
    public class InvalidTokenException : Exception
    {
        public InvalidTokenException()
            : base("Token is invalid")
        {
        }

        public InvalidTokenException(string message)
            : base(message)
        {
        }

        public InvalidTokenException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}