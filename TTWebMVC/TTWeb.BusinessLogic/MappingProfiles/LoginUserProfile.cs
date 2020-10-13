using System;
using System.Collections.Generic;
using System.Text;
using TTWeb.BusinessLogic.Models;
using TTWeb.Data.Models;
using AutoMapper;

namespace TTWeb.BusinessLogic.MappingProfiles
{
    public class LoginUserProfile : Profile
    {
        public LoginUserProfile()
        {
            CreateMap<LoginUser, LoginUserModel>();
        }
    }
}
