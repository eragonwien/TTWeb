﻿namespace TTWeb.Data.Models
{
    public class ScheduleJobResult
    {
        public int Id { get; set; }
        public int ScheduleJobId { get; set; }
        public ScheduleJob ScheduleJob { get; set; }
    }
}