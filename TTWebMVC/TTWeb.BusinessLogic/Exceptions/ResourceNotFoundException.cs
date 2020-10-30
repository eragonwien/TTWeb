using System;
using System.Runtime.Serialization;

namespace TTWeb.BusinessLogic.Exceptions
{
    [Serializable]
    public class ResourceNotFoundException<T> : Exception
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

        public ResourceNotFoundException(T resource) : base(BuildMessage(resource))
        {
        }

        private static string BuildMessage(T resource)
        {
            return $"Resource '{nameof(resource)}' not found";
        }
    }
}
