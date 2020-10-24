using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models;
using TTWeb.Data.Database;
using TTWeb.Data.Models;

namespace TTWeb.BusinessLogic.Services
{
    public interface ILoginUserService
    {
        Task<LoginUserModel> GetUserByEmailAsync(string email);
        Task<LoginUserModel> CreateUserAsync(LoginUserModel loginUserModel);
    }

    public class LoginUserService : ILoginUserService
    {
        private readonly TTWebContext db;
        private readonly IMapper mapper;

        public LoginUserService(TTWebContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<LoginUserModel> CreateUserAsync(LoginUserModel loginUserModel)
        {
            if (loginUserModel is null) throw new ArgumentNullException(nameof(loginUserModel));

            var loginUser = new LoginUser
            {
                Email = loginUserModel.Email,
                FirstName = loginUserModel.FirstName,
                LastName = loginUserModel.LastName
            };

            await db.LoginUsers.AddAsync(loginUser);
            await db.SaveChangesAsync();

            return mapper.Map<LoginUserModel>(loginUser);
        }

        public async Task<LoginUserModel> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            var loginUser = await db.LoginUsers
                .Include(u => u.LoginUserPermissionMappings)
                    .ThenInclude(m => m.UserPermission)
                .FirstOrDefaultAsync(u => u.Email == email);

            return mapper.Map<LoginUserModel>(loginUser);
        }
    }
}
