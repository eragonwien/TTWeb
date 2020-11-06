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
            // TODO: maps weekday, time frame
            CreateMap<Schedule, ScheduleModel>()
                .ForMember(m => m.Receivers, o => o.MapFrom(e => e.ScheduleReceiverMappings.Select(rm => new ScheduleReceiverModel { Id = rm.Receiver.Id, Username = rm.Receiver.Username })));

            CreateMap<ScheduleModel, Schedule>()
                    .ForMember(e => e.ScheduleReceiverMappings, o => o.MapFrom(m => m.Receivers.Select(rm => new ScheduleReceiverMapping { ReceiverId = rm.Id, ScheduleId = m.Id })));
        }
    }
}