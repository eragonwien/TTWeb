using System.Linq;
using AutoMapper;
using TTWeb.BusinessLogic.Models;
using TTWeb.Data.Models;

namespace TTWeb.BusinessLogic.MappingProfiles
{
    public class LoginUserProfile : Profile
    {
        public LoginUserProfile()
        {
            CreateMap<LoginUser, LoginUserModel>()
                .ForMember(m => m.UserPermissions, b => b.MapFrom(u => u.LoginUserPermissionMappings.Select(m => m.UserPermission)));
        }
    }
}
