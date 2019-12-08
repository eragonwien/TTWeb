using System;
using System.Collections.Generic;
using TTWebMVC.Models.ViewModels;

namespace TTWebMVC.Models
{
   public class ScheduleJob
   {
      public long Id { get; set; }
      public string Name { get; set; }
      public AppUser AppUser { get; set; }
      public ScheduleJobType Type { get; set; }
      public List<ScheduleJobParameter> Parameters { get; set; } = new List<ScheduleJobParameter>();

      public static ScheduleJob FromJobViewModel(JobViewModel model)
      {
         return new ScheduleJob
         {
            Type = model.Type,
            Name = model.Name,
            Parameters = model.Parameters
         };
      }

      public ScheduleJob AddAppUser(AppUser appUser)
      {
         AppUser = appUser;
         return this;
      }
   }
}