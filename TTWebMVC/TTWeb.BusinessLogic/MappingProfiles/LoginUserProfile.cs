using System.Linq;
using AutoMapper;
using TTWeb.BusinessLogic.Models.Account;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.Data.Models;

namespace TTWeb.BusinessLogic.MappingProfiles
{
    public class LoginUserProfile : Profile
    {
        public LoginUserProfile()
        {
            CreateMap<LoginUser, LoginUserModel>()
                .ForMember(b => b.UserPermissions, option => option.MapFrom(a => a.LoginUserPermissionMappings.Select(m => m.UserPermission)))
                .ReverseMap();

            CreateMap<LoginUserModel, ExternalLoginModel>()
                .ReverseMap();
        }
    }
}