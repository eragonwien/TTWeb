using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using System.Threading.Tasks;
using TTWebCommon.Models;

namespace TTWebApi.Services
{
   public interface IAppUserService
   {
      IQueryable<AppUser> GetAll();
      Task<AppUser> GetOne(int id);
      void Create(AppUser user);
      void Remove(AppUser user);
      Task<bool> Exist(int id);
      Task UpdateProfile(AppUser appUser);
      bool IsEmailAvailable(string email, int id);
      Task SaveChangeAsync();
   }
   public class AppUserService : IAppUserService
   {
      private readonly TTWebDbContext db;

      public AppUserService(TTWebDbContext db)
      {
         this.db = db;
      }

      public void Create(AppUser appUser)
      {
         db.AppUserSet.Add(appUser);
      }

      public async Task<bool> Exist(int id)
      {
         return await GetOne(id) != null;
      }

      public Task<AppUser> GetOne(int id)
      {
         return GetAll()
            .Where(u => u.Id == id && u.Active == 1 && u.Disabled == 0)
            .FirstOrDefaultAsync();
      }

      public void Remove(AppUser user)
      {
         db.AppUserSet.Attach(user);
         db.AppUserSet.Remove(user);
      }

      public Task UpdateProfile(AppUser appUser)
      {
         db.Entry(appUser).Property(u => u.Title).IsModified = true;
         db.Entry(appUser).Property(u => u.Firstname).IsModified = true;
         db.Entry(appUser).Property(u => u.Lastname).IsModified = true;
         db.Entry(appUser).Property(u => u.Email).IsModified = true;
         return db.SaveChangesAsync();
      }

      public bool IsEmailAvailable(string email, int id)
      {
         var occupiedUser = GetAll().Where(u => u.Email == email && u.Id != id).FirstOrDefault();
         return occupiedUser == null;
      }

      public IQueryable<AppUser> GetAll()
      {
         return db.AppUserSet
            .Include(u => u.AppUserRoles)
               .ThenInclude(r => r.Role)
            .AsNoTracking();
      }

      public Task SaveChangeAsync()
      {
         return db.SaveChangesAsync();
      }
   }
}
