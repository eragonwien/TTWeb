using System;
using System.Collections.Generic;
using System.Text;
using TTWeb.BusinessLogic.Models;
using TTWeb.Data.Models;
using AutoMapper;
using System.Linq;

namespace TTWeb.BusinessLogic.MappingProfiles
{
    public class LoginUserProfile : Profile
    {
        public LoginUserProfile()
        {
            CreateMap<LoginUser, LoginUserModel>()
                .ForMember(m => m.UserPermissions, b => b.MapFrom(u => u.LoginUserPermissionMappings.Select(m => m.UserPermission.Value)));
        }
    }
}
