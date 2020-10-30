namespace TTWebCommon.Models
{
   public class Partner
   {
      public int Id { get; set; }
      public int AppUserId { get; set; }
      public AppUser AppUser { get; set; }
      public string Name { get; set; }
      public string FacebookUser { get; set; }
   }
}
