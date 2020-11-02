using System;

namespace TTWeb.BusinessLogic.Exceptions
{
    [Serializable]
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException()
        {
        }

        public ResourceNotFoundException(string message)
            : base(message)
        {
        }

        public ResourceNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ResourceNotFoundException(string resourceName, string id)
            : base(BuildMessage(resourceName, id))
        {
        }

        private static string BuildMessage(string resourceName, string id)
        {
            return $"{resourceName} '{id}' not found";
        }
    }
}