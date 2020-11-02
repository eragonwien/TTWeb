using System;
using System.Runtime.Serialization;

namespace TTWeb.BusinessLogic.Exceptions
{
    [Serializable]
    public class InvalidInputException : Exception
    {
        public InvalidInputException()
        {
            throw new NotImplementedException();
        }

        public InvalidInputException(string propertyName)
            : base(BuildErrorMessage(propertyName))
        {

        }

        public InvalidInputException(string message, Exception innerException)
            : base(message, innerException)
        {
            throw new NotImplementedException();
        }

        private static string BuildErrorMessage(string propertyName)
        {
            return $"Property '{propertyName}' has invalid value";
        }
    }
}