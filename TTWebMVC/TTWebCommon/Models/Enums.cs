﻿using System;
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
		LOGIN,
		LIKE
	}

	public enum IntervalType
   {
		DAILY,
		WEEKLY,
		MONTHLY,
		YEARLY
   }

   public enum ScheduleJobStatus
   {
		NEW
   }
}
