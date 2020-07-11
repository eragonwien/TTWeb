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
}
