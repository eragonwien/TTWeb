using System;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

        public InvalidInputException(ModelStateDictionary modelState) : base(BuildErrorMessage(modelState))
        {
            
        }

        private static string BuildErrorMessage(ModelStateDictionary modelState)
        {
            if (modelState == null) throw new ArgumentNullException(nameof(modelState));
            var error = modelState.First(m => m.Value.Errors.Any());
            return  $"Property '{error.Key}' has invalid value";
        }

        private static string BuildErrorMessage(string propertyName)
        {
            return $"Property '{propertyName}' has invalid value";
        }
    }
}