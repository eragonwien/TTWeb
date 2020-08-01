using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTWebMVCV2.Models
{
   public class EmptyStateViewModel
   {
      public string Title { get; set; }
      public string Subtitle { get; set; }
      public string CallToActionButtonName { get; set; }
      public string CallToActionHref { get; set; }

      public EmptyStateViewModel(string title = null, string subtitle = null, string callToActionButtonName = null, string callToActionHref = null)
      {
         Title = title;
         Subtitle = subtitle;
         CallToActionButtonName = callToActionButtonName;
         CallToActionHref = callToActionHref;
      }
   }
}
