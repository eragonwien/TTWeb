using System;
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
            CreateMap<Schedule, ScheduleModel>()
                .ForMember(b => b.Sender, option => option.MapFrom(a => a.Sender))
                .ForMember(b => b.Receivers, option => option.MapFrom(a => a.ScheduleReceiverMappings.Select(m => m.Receiver)))
                .ForMember(b => b.Weekdays, option => option.MapFrom(a => a.ScheduleWeekdayMappings))
                .ForMember(b => b.TimeFrames, option => option.MapFrom(a => a.TimeFrames));

            CreateMap<ScheduleModel, Schedule>()
                .ForMember(b => b.Sender, option => option.MapFrom(a => a.Sender))
                .ForMember(b => b.ScheduleReceiverMappings, option => option.MapFrom(a => a.Receivers.Select(m => new ScheduleFacebookUserMappingModel(a.Id, m.Id))))
                .ForMember(b => b.ScheduleWeekdayMappings, option => option.MapFrom(a => a.Weekdays.Select(m => new ScheduleWeekdayMappingModel(a.Id, m))))
                .ForMember(b => b.TimeFrames, option => option.MapFrom(a => a.TimeFrames.Select(m => new ScheduleTimeFrameMappingModel(a.Id, m))));

            CreateMap<ScheduleFacebookUserMappingModel, ScheduleReceiverMapping>();
            CreateMap<ScheduleWeekdayMapping, DayOfWeek>().ConvertUsing(a => a.Weekday);
            CreateMap<ScheduleWeekdayMappingModel, ScheduleWeekdayMapping>();
            CreateMap<ScheduleTimeFrame, ScheduleTimeFrameModel>();
            CreateMap<ScheduleTimeFrame, ScheduleTimeFrameMappingModel>();
        }
    }
}