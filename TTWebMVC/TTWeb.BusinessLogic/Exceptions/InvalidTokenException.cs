using System;

namespace TTWeb.BusinessLogic.Exceptions
{
    [Serializable]
    public class InvalidTokenException : Exception, IBadRequestException
    {
        public InvalidTokenException()
            : base("Token is invalid")
        {
        }

        public InvalidTokenException(string input)
            : base(input)
        {
        }

        public InvalidTokenException(string input, Exception innerException)
            : base(input, innerException)
        {
        }
    }
}