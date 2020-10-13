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
        Task CreateUserAsync(LoginUserModel loginUserModel);
        Task SaveChangesAsync();
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

        public async Task CreateUserAsync(LoginUserModel loginUserModel)
        {
            if (loginUserModel is null) throw new ArgumentNullException(nameof(loginUserModel));

            await db.LoginUsers.AddAsync(new LoginUser
            {
                Email = loginUserModel.Email,
                FirstName = loginUserModel.FirstName,
                LastName = loginUserModel.LastName
            });
        }

        public async Task<LoginUserModel> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            var loginUser = await db.LoginUsers
                .FirstOrDefaultAsync(u => u.Email == email);

            return mapper.Map<LoginUserModel>(loginUser);
        }

        public async Task SaveChangesAsync() => await db.SaveChangesAsync();
    }
}
