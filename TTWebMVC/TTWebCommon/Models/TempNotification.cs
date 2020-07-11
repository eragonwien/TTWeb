using System;
using System.Collections.Generic;
using System.Text;

namespace TTWebCommon.Models
{
   public class TempNotification
   {
      public string Text { get; set; }
      public TempNotificationType Type { get; set; } = TempNotificationType.DEFAULT;

      public TempNotification(string text, TempNotificationType type = TempNotificationType.DEFAULT)
      {
         Text = text;
         Type = type;
      }

      public string NotificationClass
      {
         get
         {
            switch (Type)
            {
               case TempNotificationType.ERROR:
                  return "is-danger";
               case TempNotificationType.SUCCESS:
                  return "is-success";
               case TempNotificationType.WARNING:
                  return "is-warning";
               case TempNotificationType.DEFAULT:
               default:
                  return string.Empty;
            }
         }
      }
   }

   public enum TempNotificationType
   {
      DEFAULT,
      ERROR,
      SUCCESS,
      WARNING
   }
}
