using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TTWeb.BusinessLogic.Exceptions
{
    [Serializable]
    public class InvalidInputException : Exception, IBadRequestException
    {
        public InvalidInputException()
        {
        }

        public InvalidInputException(string propertyName)
            : base(BuildErrorMessage(propertyName))
        {
        }

        public InvalidInputException(string input, Exception innerException)
            : base(input, innerException)
        {
        }

        public InvalidInputException(ModelStateDictionary modelState) : base(BuildErrorMessage(modelState))
        {
        }

        private static string BuildErrorMessage(ModelStateDictionary modelState)
        {
            if (modelState == null) throw new ArgumentNullException(nameof(modelState));
            var error = modelState.First(m => m.Value.Errors.Any());
            return $"Property '{error.Key}' has invalid value";
        }

        protected static string BuildErrorMessage(string propertyName)
        {
            return $"Property '{propertyName}' has invalid value";
        }
    }
}