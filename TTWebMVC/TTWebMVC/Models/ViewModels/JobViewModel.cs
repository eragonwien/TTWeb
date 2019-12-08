using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTWebMVC.Models.ViewModels
{
   public class JobViewModel
   {
      public ScheduleJobType Type { get; set; }
      public string Name { get; set; }
      public List<ScheduleJobParameter> Parameters { get; set; } = new List<ScheduleJobParameter>();
      public List<SelectListItem> Types { get; set; } = new List<SelectListItem>();
      public List<SelectListItem> ParameterTypes { get; set; } = new List<SelectListItem>();
   }
}
