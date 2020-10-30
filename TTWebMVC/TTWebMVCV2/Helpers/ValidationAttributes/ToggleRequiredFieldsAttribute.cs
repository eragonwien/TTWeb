using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TTWebMVCV2.Helpers.ValidationAttributes
{
    public class ToggleRequiredFieldsAttribute : ValidationAttribute, IClientModelValidator
    {
        public object ExpectedValue { get; set; }
        public string TargetFieldName { get; set; }
        public string HiddenClass { get; set; } = "is-hidden";

        public ToggleRequiredFieldsAttribute(object expectedValue, string targetFieldName, string hiddenClass = null)
        {
            ExpectedValue = expectedValue;
            TargetFieldName = targetFieldName;
            HiddenClass = hiddenClass ?? HiddenClass;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            return true;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-togglerequiredfield", ErrorMessage);
            context.Attributes.Add("data-val-togglerequiredfield-expectedvalue", ExpectedValue?.ToString());
            context.Attributes.Add("data-val-togglerequiredfield-targetfieldname", TargetFieldName);
            context.Attributes.Add("data-val-togglerequiredfield-hiddenclass", HiddenClass);
        }
    }
}
