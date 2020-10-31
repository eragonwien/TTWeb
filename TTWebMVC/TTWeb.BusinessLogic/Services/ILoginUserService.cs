﻿using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Entities.LoginUser;

namespace TTWeb.BusinessLogic.Services
{
    public interface ILoginUserService
    {
        Task<LoginUserModel> GetUserByEmailAsync(string email);
        Task<LoginUserModel> CreateUserAsync(LoginUserModel loginUserModel);
    }
}
