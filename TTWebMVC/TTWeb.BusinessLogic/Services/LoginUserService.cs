using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTWeb.Data.Database;
using TTWeb.Data.Models;

namespace TTWeb.BusinessLogic.Services
{
    public interface ILoginUserService
    {
        Task<LoginUser> GetUser(int id);
    }

    public class LoginUserService : ILoginUserService
    {
        private readonly TTWebContext db;

        public LoginUserService(TTWebContext db)
        {
            this.db = db;
        }

        public async Task<LoginUser> GetUser(int id)
        {
            return await db.LoginUsers
                .FirstOrDefaultAsync(user => user.Id == id);
        }
    }
}
