using AutoMapper;
using System;
using System.Linq;
using TTWeb.BusinessLogic.Models.Account;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.Data.Models;

namespace TTWeb.BusinessLogic.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LoginUser, LoginUserModel>()
                .ForMember(b => b.Permissions, o => o.MapFrom(a => a.LoginUserPermissionMappings.Select(m => m.UserPermission)))
                .ReverseMap();

            CreateMap<LoginUserModel, ExternalLoginModel>()
                .ReverseMap();

            CreateMap<FacebookUser, FacebookUserModel>()
                .ReverseMap();

            CreateMap<FacebookUser, ScheduleFacebookUserModel>()
                .ReverseMap();

            CreateMap<ScheduleModel, Schedule>()
                .ForMember(b => b.Sender, o => o.Ignore())
                .ForMember(b => b.SenderId, o => o.MapFrom(a => a.Sender.Id))
                .ForMember(b => b.ScheduleReceiverMappings, o => o.MapFrom(a => a.Receivers.Select(m => new ScheduleReceiverMapping { ScheduleId = a.Id, ReceiverId = m.Id })))
                .ForMember(b => b.ScheduleWeekdayMappings, o => o.MapFrom(a => a.Weekdays.Select(m => new ScheduleWeekdayMapping { ScheduleId = a.Id, Weekday = m })))
                .ForMember(b => b.TimeFrames, o => o.MapFrom(a => a.TimeFrames.Select(m => new ScheduleTimeFrame { ScheduleId = a.Id, From = m.From, To = m.To })));

            CreateMap<Schedule, ScheduleModel>()
                .ForMember(b => b.Receivers, o => o.MapFrom(a => a.ScheduleReceiverMappings.Select(m => m.Receiver)))
                .ForMember(b => b.Weekdays, o => o.MapFrom(a => a.ScheduleWeekdayMappings))
                .ForMember(b => b.TimeFrames, o => o.MapFrom(a => a.TimeFrames));

            CreateMap<ScheduleTimeFrame, ScheduleTimeFrameModel>();

            CreateMap<ScheduleWeekdayMapping, DayOfWeek>()
                .ConvertUsing(m => m.Weekday);

            CreateMap<Schedule, ScheduleJobModel>()
                .ForMember(b => b.Id, o => o.Ignore())
                .ForMember(b => b.ScheduleId, o => o.MapFrom(a => a.Id))
                .ForMember(b => b.Sender, o => o.MapFrom(a => a.Sender))
                .ForMember(b => b.Receiver, o => o.Ignore())
                .ForMember(b => b.Weekdays, o => o.MapFrom(a => a.ScheduleWeekdayMappings));

            CreateMap<ScheduleJobModel, ScheduleJob>()
                .ForMember(b => b.Id, o => o.Ignore())
                .ForMember(b => b.Receiver, o => o.Ignore())
                .ForMember(b => b.ReceiverId, o => o.MapFrom(a => a.Receiver.Id))
                .ForMember(b => b.Sender, o => o.Ignore())
                .ForMember(b => b.SenderId, o => o.MapFrom(a => a.Sender.Id));

            CreateMap<ScheduleJob, ScheduleJobModel>();

            CreateMap<Worker, WorkerModel>()
                .ForMember(b => b.Permissions, o => o.MapFrom(a => a.WorkerPermissionMappings.Select(m => m.UserPermission)))
                .ReverseMap();
        }
    }
}