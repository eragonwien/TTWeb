using System;

namespace TTWeb.BusinessLogic.Exceptions
{
    [Serializable]
    public class ResourceAccessDeniedException : Exception
    {
        public ResourceAccessDeniedException()
            : base(DefaultMessage)
        {
        }

        public ResourceAccessDeniedException(string message)
            : base(message)
        {
        }

        public ResourceAccessDeniedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        private const string DefaultMessage = "This user does not have access to this resource";
    }
}