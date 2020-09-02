﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
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

	[JsonConverter(typeof(StringEnumConverter))]
	public enum UserRole
	{
		[EnumMember(Value = "Standard")]
		Standard = 1,

		[EnumMember(Value = "Administrator")]
		Administrator = 2,
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
