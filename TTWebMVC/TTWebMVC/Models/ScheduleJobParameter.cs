namespace TTWebMVC.Models
{
   public class ScheduleJobParameter
   {
      public long Id { get; set; }
      public string Value { get; set; }
      public ScheduleJob Job { get; set; }
      public ScheduleJobParameterType Type { get; set; }
   }
}