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
                .ForMember(m => m.Receivers,
                    o => o.MapFrom(e => e.ScheduleReceiverMappings
                        .Select(rm => new ScheduleReceiverModel { Id = rm.ReceiverId })))
                .ForMember(m => m.Weekdays,
                    o => o.MapFrom(e => e.ScheduleWeekdayMappings.Select(wm => wm.Weekday)))
                .ForMember(m => m.TimeFrames,
                    o => o.MapFrom(e => e.TimeFrames.Select(tf => new ScheduleTimeFrameModel { Id = tf.Id, From = tf.From, To = tf.To })));

            CreateMap<ScheduleModel, Schedule>()
                    .ForMember(e => e.ScheduleReceiverMappings,
                        o => o.MapFrom(m => m.Receivers
                            .Select(rm => new ScheduleReceiverMapping { ReceiverId = rm.Id, ScheduleId = m.Id })))
                    .ForMember(e => e.ScheduleWeekdayMappings,
                        o => o.MapFrom(m => m.Weekdays.Select(wm => new ScheduleWeekdayMapping { Weekday = wm, ScheduleId = m.Id })))
                    .ForMember(e => e.TimeFrames,
                        o => o.MapFrom(m => m.TimeFrames.Select(tf => new ScheduleTimeFrame { Id = tf.Id, From = tf.From, To = tf.To, ScheduleId = m.Id })));
        }
    }
}