using System;

namespace TTWeb.BusinessLogic.Exceptions
{
    [Serializable]
    public class ResourceAccessDeniedException : Exception, IBadRequestException
    {
        public ResourceAccessDeniedException()
            : base(DefaultMessage)
        {
        }

        public ResourceAccessDeniedException(string input)
            : base(input)
        {
        }

        public ResourceAccessDeniedException(string input, Exception innerException)
            : base(input, innerException)
        {
        }

        private const string DefaultMessage = "This user does not have access to this resource";
    }
}