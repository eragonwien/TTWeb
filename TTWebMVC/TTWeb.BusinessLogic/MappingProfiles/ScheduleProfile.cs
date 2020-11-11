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
                .ForMember(b => b.Receivers, option => option.MapFrom(a => a.ScheduleReceiverMappings))
                .ForMember(b => b.Weekdays, option => option.MapFrom(a => a.ScheduleWeekdayMappings))
                .ForMember(b => b.TimeFrames, option => option.MapFrom(a => a.TimeFrames));

            CreateMap<ScheduleModel, Schedule>()
                .ForMember(b => b.ScheduleReceiverMappings, option => option.MapFrom(a => a.Receivers.Select(m => new ScheduleFacebookUserMappingModel(a.Id, m.Id))))
                .ForMember(b => b.ScheduleWeekdayMappings, option => option.MapFrom(a => a.Weekdays.Select(m => new ScheduleWeekdayMappingModel(a.Id, m))))
                .ForMember(b => b.TimeFrames, option => option.MapFrom(a => a.TimeFrames.Select(m => new ScheduleTimeFrameMappingModel(a.Id, m))));

            CreateMap<ScheduleFacebookUserMappingModel, ScheduleReceiverMapping>()
                .ReverseMap();

            CreateMap<ScheduleWeekdayMappingModel, ScheduleWeekdayMapping>()
                .ReverseMap();

            CreateMap<ScheduleTimeFrame, ScheduleTimeFrameModel>()
                .ReverseMap();

            CreateMap<ScheduleTimeFrame, ScheduleTimeFrameMappingModel>()
                .ReverseMap();
        }
    }
}