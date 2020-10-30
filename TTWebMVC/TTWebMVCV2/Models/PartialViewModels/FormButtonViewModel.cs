namespace TTWebMVCV2.Models.PartialViewModels
{
    public class FormButtonViewModel
    {
        public string FormId { get; set; }
        public bool DisplayDeleteButton { get; set; } = false;
        public bool DisplayUpdateButton { get; set; } = false;
        public bool DisplayCreateButton { get; set; } = false;
        public bool DisplayCancelButton { get; set; } = false;
        public string ReturnUrl { get; set; }
        public string CreateUrl { get; set; }
        public string UpdateUrl { get; set; }
        public string DeleteUrl { get; set; }

        public FormButtonViewModel(string createUrl = null, string updateUrl = null, string deleteUrl = null, string returnUrl = null)
        {
            DisplayCancelButton = true;
            CreateUrl = createUrl;
            UpdateUrl = updateUrl;
            DeleteUrl = deleteUrl;
            ReturnUrl = returnUrl;

            DisplayCreateButton = !string.IsNullOrWhiteSpace(CreateUrl);
            DisplayUpdateButton = !string.IsNullOrWhiteSpace(UpdateUrl);
            DisplayDeleteButton = !string.IsNullOrWhiteSpace(DeleteUrl);
            DisplayCancelButton = !string.IsNullOrWhiteSpace(ReturnUrl);
        }
    }

    public enum FormSubmitType
    {
        CREATE,
        UPDATE
    }
}
