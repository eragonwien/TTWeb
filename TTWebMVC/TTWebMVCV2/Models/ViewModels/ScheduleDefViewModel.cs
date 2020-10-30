using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using SNGCommon;
using SNGCommon.Extenstions.StringExtensions;
using TTWebCommon.Models;
using TTWebCommon.Models.DataModels;
using TTWebMVCV2.Helpers.ValidationAttributes;

namespace TTWebMVCV2.Models.ViewModels
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
        [ToggleRequiredFields(IntervalTypeEnum.DAILY, "#SelectedDaysOfWeekField", "is-hidden")]
        public IntervalTypeEnum IntervalType { get; set; }

        [RequiredIfDependentHasValue(DependentPropertyName = nameof(IntervalType), DependentDesiredValue = IntervalTypeEnum.DAILY)]
        public List<int> SelectedDaysOfWeek { get; set; } = new List<int>();

        [Required]
        public string TimeFrom { get; set; }

        [Required]
        public string TimeTo { get; set; }

        [Required]
        public string TimeZone { get; set; }

        [Required]
        public bool Active { get; set; }

        public IEnumerable<string> ScheduleTypes { get; set; } = Enumerable.Empty<string>();
        public IEnumerable<SelectListItem> IntervalTypes { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> TimeZones { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Logins { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Friends { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> DaysOfWeek { get; set; } = Enumerable.Empty<SelectListItem>();

        public static ScheduleDefViewModel Default = new ScheduleDefViewModel();

        public ScheduleDefViewModel()
        {
            InitializeSelectLists();
        }

        public ScheduleDefViewModel(ScheduleJobDef scheduleJobDef)
        {
            if (scheduleJobDef != null)
            {
                Id = scheduleJobDef.Id;
                Name = scheduleJobDef.Name;
                FriendId = scheduleJobDef.FriendId;
                FacebookCredentialId = scheduleJobDef.FacebookCredentialId;
                Type = scheduleJobDef.Type;
                IntervalType = scheduleJobDef.IntervalType;
                TimeFrom = scheduleJobDef.TimeFrom;
                TimeTo = scheduleJobDef.TimeTo;
                TimeZone = scheduleJobDef.TimeZone;
                Active = scheduleJobDef.Active;
                SelectedDaysOfWeek = scheduleJobDef.WeekDayIds.ToList();
            }
            InitializeSelectLists();
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
                WeekDayIds = SelectedDaysOfWeek.ToList(),
            };
        }

        public bool HasValidId()
        {
            return Id != null && Id.HasValue && Id.Value > 0;
        }

        private void InitializeSelectLists()
        {
            ScheduleTypes = Helper.GetEnumStrings<ScheduleJobType>(true).Select(s => s.ToStringCapitalized());
            IntervalTypes = Helper.GetEnumStrings<IntervalTypeEnum>(true).Select(s => new SelectListItem(s.ToStringCapitalized(), s));
        }

        public ScheduleDefViewModel SetLogins(IEnumerable<FacebookCredential> logins)
        {
            if (logins != null && logins.Any())
            {
                Logins = logins.Select(l => new SelectListItem(l.Username, l.Id.ToString()));
            }
            return this;
        }

        public ScheduleDefViewModel SetFriends(IEnumerable<FacebookFriend> friends)
        {
            if (friends != null && friends.Any())
            {
                Friends = friends.Select(l => new SelectListItem(l.Name, l.Id.ToString()));
            }
            return this;
        }

        public ScheduleDefViewModel SetTimezoneSelectList(string timezone)
        {
            if (!string.IsNullOrWhiteSpace(timezone))
            {
                TimeZone = timezone;
            }
            TimeZones = TimeZoneInfo.GetSystemTimeZones()
               .Select(tz => new SelectListItem(tz.DisplayName, tz.Id, tz.Id == (TimeZone ?? TimeZoneInfo.Utc.Id)));
            return this;
        }

        public ScheduleDefViewModel SetWeekDaySelectList(IEnumerable<ScheduleWeekDay> weekDays)
        {
            DaysOfWeek = weekDays.Select(d => new SelectListItem(d.DisplayText, d.Id.ToString(), SelectedDaysOfWeek.Contains(d.Id)));
            return this;
        }
    }
}
