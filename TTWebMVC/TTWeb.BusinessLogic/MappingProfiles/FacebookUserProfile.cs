using AutoMapper;
using TTWeb.BusinessLogic.Models.Entities.FacebookUser;
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