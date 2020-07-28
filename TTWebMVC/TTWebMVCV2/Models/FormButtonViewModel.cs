using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTWebMVCV2.Models
{
    public class FormButtonViewModel
    {
        public string FormId { get; set; }
        public bool DisplayDeleteButton { get; set; } = false;
        public bool DisplayUpdateButton { get; set; } = false;
        public bool DisplayCreateButton { get; set; } = false;
        public bool DisplayCancelButton { get; set; } = false;
        public FormSubmitType Type { get; set; } = FormSubmitType.CREATE;
        public string ReturnUrl { get; set; }

        public FormButtonViewModel(FormSubmitType type, string returnUrl)
        {
            DisplayCancelButton = true;
            switch (type)
            {
                case FormSubmitType.UPDATE:
                    DisplayDeleteButton = true;
                    DisplayUpdateButton = true;
                    break;
                case FormSubmitType.CREATE:
                default:
                    DisplayCreateButton = true;
                    break;
            }
            ReturnUrl = returnUrl;
        }
    }

    public enum FormSubmitType
    {
        CREATE,
        UPDATE
    }
}
