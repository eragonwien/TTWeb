using System.Collections;
using System.ComponentModel.DataAnnotations;
using SNGCommon.Extenstions.StringExtensions;

namespace TTWebMVCV2.Helpers.ValidationAttributes
{
    public class RequiredIfDependentHasValueAttribute : ValidationAttribute
    {
        public string DependentPropertyName { get; set; }
        public object DependentDesiredValue { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (string.IsNullOrWhiteSpace(DependentPropertyName) || context == null)
                return ValidationResult.Success;

            var thisValue = context.ObjectInstance.GetType().GetProperty(context.MemberName).GetValue(context.ObjectInstance, null);
            var dependentValue = context.ObjectInstance.GetType().GetProperty(DependentPropertyName).GetValue(context.ObjectInstance, null);
            var collectionValue = (ICollection)thisValue;

            if (string.IsNullOrWhiteSpace(ErrorMessage))
                ErrorMessage = string.Format("This field is required when option {0} is chosen", DependentDesiredValue?.ToStringCapitalized());

            if (dependentValue?.ToString() == DependentDesiredValue?.ToString() && collectionValue.Count == 0)
            {
                return new ValidationResult(ErrorMessage, new string[] { context.MemberName });
            }

            return ValidationResult.Success;
        }
    }
}
