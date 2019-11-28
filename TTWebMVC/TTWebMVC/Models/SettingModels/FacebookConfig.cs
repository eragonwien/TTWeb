using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTWebMVC.Models.SettingModels
{
   public class FacebookConfig
   {
      public const string Name = "Facebook";
      public string AppId { get; set; }
      public string AppSecret { get; set; }
   }
}
