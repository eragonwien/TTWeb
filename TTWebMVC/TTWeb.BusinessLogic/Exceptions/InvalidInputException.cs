using System;

namespace TTWeb.BusinessLogic.Exceptions
{
    [Serializable]
    public class InvalidInputException : Exception
    {
        public InvalidInputException()
        {

        }

        public InvalidInputException(string message)
            : base(message)
        {

        }

        public InvalidInputException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}