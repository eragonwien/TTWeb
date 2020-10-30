namespace TTWebCommon.Facebook
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
      COMMENT
   }
}
