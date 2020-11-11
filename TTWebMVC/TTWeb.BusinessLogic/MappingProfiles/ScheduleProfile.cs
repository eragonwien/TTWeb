﻿using System;
using AutoMapper;
using System.Linq;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.Data.Models;

namespace TTWeb.BusinessLogic.MappingProfiles
{
    public class ScheduleProfile : Profile
    {
        public ScheduleProfile()
        {
            CreateMap<ScheduleModel, Schedule>()
                .ForMember(b => b.Sender, option => option.Ignore())
                .ForMember(b => b.SenderId, option => option.MapFrom(a => a.Sender.Id))
                .ForMember(b => b.ScheduleReceiverMappings, option => option.MapFrom(a => a.Receivers.Select(m => new ScheduleReceiverMapping { ScheduleId = a.Id, ReceiverId = m.Id })))
                .ForMember(b => b.ScheduleWeekdayMappings, option => option.MapFrom(a => a.Weekdays.Select(m => new ScheduleWeekdayMapping { ScheduleId = a.Id, Weekday = m })))
                .ForMember(b => b.TimeFrames, option => option.MapFrom(a => a.TimeFrames.Select(m => new ScheduleTimeFrame { ScheduleId = a.Id, From = m.From, To = m.To })));

            CreateMap<Schedule, ScheduleModel>()
                .ForMember(b => b.Receivers, option => option.MapFrom(a => a.ScheduleReceiverMappings.Select(m => m.Receiver)))
                .ForMember(b => b.Weekdays, option => option.MapFrom(a => a.ScheduleWeekdayMappings))
                .ForMember(b => b.TimeFrames, option => option.MapFrom(a => a.TimeFrames));

            CreateMap<ScheduleTimeFrame, ScheduleTimeFrameModel>();
            CreateMap<ScheduleWeekdayMapping, DayOfWeek>().ConvertUsing(m => m.Weekday);
        }
    }
}