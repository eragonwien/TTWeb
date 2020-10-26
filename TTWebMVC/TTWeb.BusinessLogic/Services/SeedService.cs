using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTWeb.Data.Database;
using TTWeb.Data.Models;

namespace TTWeb.BusinessLogic.Services
{
    public interface ISeedService
    {
        void Migrate();
        void Seed();
    }

    public class SeedService : ISeedService
    {
        private readonly TTWebContext db;

        public SeedService(TTWebContext dbContext)
        {
            db = dbContext;
        }

        public void Migrate()
        {
            db.Database.Migrate();
        }

        public void Seed()
        {
            var loginUsers = SeedLoginUserAsync();
            SeedLoginUserPermissionMappingAsync(loginUsers);
        }

        private LoginUser[] SeedLoginUserAsync()
        {
            if (db.LoginUsers.Any())
                return null;

            var loginUsers = new List<LoginUser>
            {
                new LoginUser { Email = "test@test.com", FirstName = "test", LastName = "dev" }
            };

            db.LoginUsers.AddRange(loginUsers);
            db.SaveChanges();
            return loginUsers.ToArray();
        }

        private void SeedLoginUserPermissionMappingAsync(LoginUser[] loginUsers)
        {
            if (loginUsers == null
                || loginUsers.Length == 0
                || db.LoginUserPermissionMappings.Any())
                return;

            var mappings = loginUsers.Select(m => new LoginUserPermissionMapping { LoginUserId = m.Id, UserPermission = UserPermission.GUEST });

            db.LoginUserPermissionMappings.AddRange(mappings);
            db.SaveChanges();
        }
    }
}
