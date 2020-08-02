using System;
using System.Collections.Generic;
using System.Text;

namespace TTWebCommon.Models
{
	public enum AuthenticationMethod
	{
		JWT
	}

	public enum ScheduleJobType
	{
		NONE,
		LIKE
	}

	public enum IntervalTypeEnum
   {
		NONE,
		DAILY,
		WEEKLY,
		MONTHLY,
		YEARLY
   }

   public enum ScheduleJobStatus
   {
		NEW
   }

	public enum UserRole
	{
		NONE,
		ADMIN,
	}

    public enum WeekDayEnum
    {
		Monday = 1,
		Tuesday = 2,
		Wednesday = 3,
		Thursday = 4,
		Friday = 5,
		Saturday = 6,
		Sunday = 7,
	}
}
