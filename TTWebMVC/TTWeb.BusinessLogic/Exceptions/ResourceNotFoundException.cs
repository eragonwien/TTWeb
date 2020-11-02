using System;

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

        public ResourceNotFoundException(T resource, string id)
            : base(BuildMessage(resource, id))
        {
        }

        private static string BuildMessage(T resource, string id)
        {
            return $"{nameof(resource)} '{id}' not found";
        }
    }
}
