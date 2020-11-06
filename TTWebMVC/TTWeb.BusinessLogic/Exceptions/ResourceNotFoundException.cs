using System;

namespace TTWeb.BusinessLogic.Exceptions
{
    [Serializable]
    public class ResourceNotFoundException : Exception, IBadRequestException
    {
        public ResourceNotFoundException()
        {
        }

        public ResourceNotFoundException(string input)
            : base(input)
        {
        }

        public ResourceNotFoundException(string input, Exception innerException)
            : base(input, innerException)
        {
        }

        public ResourceNotFoundException(string resourceName, string id)
            : base(BuildMessage(resourceName, id))
        {
        }

        public ResourceNotFoundException(string resourceName, int id)
            : base(BuildMessage(resourceName, id.ToString()))
        {
        }

        private static string BuildMessage(string resourceName, string id)
        {
            return $"Resource {resourceName} of ID '{id}' not found";
        }
    }
}