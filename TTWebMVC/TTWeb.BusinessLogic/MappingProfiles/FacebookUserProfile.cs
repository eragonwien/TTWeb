using AutoMapper;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.Data.Models;

namespace TTWeb.BusinessLogic.MappingProfiles
{
    public class FacebookUserProfile : Profile
    {
        public FacebookUserProfile()
        {
            CreateMap<FacebookUser, FacebookUserModel>()
                .ReverseMap();
        }
    }
}