using System.Collections.Generic;

namespace TTWebCommon.Models.DataModels
{
    public class ScheduleJobDef
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; } = new AppUser();
        public int FriendId { get; set; }
        public FacebookFriend Friend { get; set; } = new FacebookFriend();
        public int FacebookCredentialId { get; set; }
        public ScheduleJobType Type { get; set; } = ScheduleJobType.NONE;
        public IntervalTypeEnum IntervalType { get; set; } = IntervalTypeEnum.NONE;
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
        public string TimeZone { get; set; }
        public bool Active { get; set; } = false;
        public List<int> WeekDayIds { get; set; } = new List<int>();

        public ScheduleJobDef()
        {

        }
    }
}
