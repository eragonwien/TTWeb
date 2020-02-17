using System;
using System.Collections.Generic;
using System.Text;

namespace TTWebNetCommon.Facebook
{
   public class FacebookServiceParameter
   {
      public string Email { get; set; }
      public string Password { get; set; }
      public string TargetUrl { get; set; }
      public FacebookServiceActionType ActionType { get; set; }
   }

   public enum FacebookServiceActionType
   {
      LOGIN,
      LIKE,
      COMMENT,
      TEST_HTML
   }
}
