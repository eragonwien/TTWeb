using Microsoft.AspNetCore.Mvc.Rendering;
using SNGCommon;
using SNGCommon.Extenstions.StringExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TTWebCommon.Models;

namespace TTWebMVCV2.Models
{
   public class ScheduleDefViewModel
   {
      public int? Id { get; set; }
      [StringLength(16)]
      public string Name { get; set; }
      [Required]
      [Display(Name = "Friend")]
      public int? FriendId { get; set; }
      [Required]
      [Display(Name = "Login")]
      public int? FacebookCredentialId { get; set; }
      [Required]
      public ScheduleJobType Type { get; set; }
      [Required]
      [Display(Name = "Interval")]
      public IntervalTypeEnum IntervalType { get; set; }
      [Required]
      public string TimeFrom { get; set; }
      [Required]
      public string TimeTo { get; set; }
      [Required]
      public string TimeZone { get; set; }
      [Required]
      public bool Active { get; set; }

      public static ScheduleDefViewModel Default = new ScheduleDefViewModel();

      public ScheduleDefViewModel()
      {

      }

      public ScheduleDefViewModel(ScheduleJobDef model)
      {
         if (model == null)
         {
            return;
         }
         Id = model.Id;
         FriendId = model.FriendId;
         FacebookCredentialId = model.FacebookCredentialId;
         Type = model.Type;
         IntervalType = model.IntervalType;
         TimeFrom = model.TimeFrom;
         TimeTo = model.TimeTo;
         TimeZone = model.TimeZone;
         Active = model.Active;
      }

      public ScheduleJobDef ToScheduleJobDef(int appuserId)
      {
         return new ScheduleJobDef
         {
            Id = Id != null && Id.HasValue ? Id.Value : 0,
            Name = Name,
            AppUserId = appuserId,
            FriendId = FriendId.Value,
            FacebookCredentialId = FacebookCredentialId.Value,
            Type = Type,
            IntervalType = IntervalType,
            TimeFrom = TimeFrom,
            TimeTo = TimeTo,
            TimeZone = TimeZone,
            Active = Active,
         };
      }
   }

   public class ScheduleDefModalViewModel : ScheduleDefViewModel
   {
      public IEnumerable<string> ScheduleTypes { get; set; }
      public IEnumerable<string> IntervalTypes { get; set; }
      public IEnumerable<SelectListItem> TimeZones { get; set; }

      public ScheduleDefModalViewModel()
      {
         InitializeSelectLists();
      }

      public ScheduleDefModalViewModel(ScheduleJobDef scheduleJobDef) : base(scheduleJobDef)
      {
         InitializeSelectLists();
      }

      public bool HasValidId()
      {
         return Id != null && Id.HasValue && Id.Value > 0;
      }

      private void InitializeSelectLists()
      {
         ScheduleTypes = Helper.GetEnumStrings<ScheduleJobType>(true).Select(s => s.ToStringCapitalized());
         IntervalTypes = Helper.GetEnumStrings<IntervalTypeEnum>(true).Select(s => s.ToStringCapitalized());

         TimeZones = TimeZoneInfo.GetSystemTimeZones()
            .Select(tz => new SelectListItem(tz.DisplayName, tz.Id, tz.Id == TimeZoneInfo.Utc.Id));
      }
   }
}
