using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTWebCommon.Models;

namespace TTWebApi.Services
{
   public interface IAppUserService
   {
      Task<IEnumerable<AppUser>> GetAll();
      Task<AppUser> GetOne(int id);
      Task Create(AppUser user);
      Task Remove(int id);
      Task<bool> Exist(int id);
      Task UpdateProfile(AppUser appUser);
      Task<bool> IsEmailAvailable(string email, int id);
   }
   public class AppUserService : IAppUserService
   {
      private readonly TTWebDbContext db;

      public AppUserService(TTWebDbContext db)
      {
         this.db = db;
      }

      public async Task Create(AppUser user)
      {
         string cmdStr = "INSERT INTO appuser(email, title, firstname, lastname, active) VALUES(:email, :title, :firstname, :lastname, :active)";
         using (MySqlCommand cmd = db.CreateCommand(cmdStr))
         {
            cmd.Parameters.Add(new MySqlParameter("email", user.Email));
            cmd.Parameters.Add(new MySqlParameter("title", user.Title));
            cmd.Parameters.Add(new MySqlParameter("firstname", user.Firstname));
            cmd.Parameters.Add(new MySqlParameter("lastname", user.Lastname));
            cmd.Parameters.Add(new MySqlParameter("active", 0));
            await cmd.ExecuteNonQueryAsync();
         }
      }

      public async Task<bool> Exist(int id)
      {
         string cmdStr = "SELECT CASE WHEN EXISTS(SELECT id FROM appuser WHERE id=:id) THEN 1 ELSE 0 FROM DUAL";
         using (MySqlCommand cmd = db.CreateCommand(cmdStr))
         {
            cmd.Parameters.Add(new MySqlParameter("id", id));
            return await db.ReadScalarBooleanAsync();
         }
      }

      public Task<IEnumerable<AppUser>> GetAll()
      {
         throw new System.NotImplementedException();
      }

      public Task<AppUser> GetOne(int id)
      {
         throw new System.NotImplementedException();
      }

      public async Task<bool> IsEmailAvailable(string email, int id)
      {
         string cmdStr = "SELECT CASE WHEN EXISTS(SELECT id FROM appuser WHERE email=:email AND id!=:id) THEN 1 ELSE 0 FROM DUAL";
         using (MySqlCommand cmd = db.CreateCommand(cmdStr))
         {
            cmd.Parameters.Add(new MySqlParameter("email", email));
            cmd.Parameters.Add(new MySqlParameter("id", id));
            return await db.ReadScalarBooleanAsync();
         }
      }

      public async Task Remove(int id)
      {
         string cmdStr = "DELETE FROM appuser WHERE id=:id";
         using (MySqlCommand cmd = db.CreateCommand(cmdStr))
         {
            cmd.Parameters.Add(new MySqlParameter("id", id));
            await cmd.ExecuteNonQueryAsync();
         }
      }

      public async Task UpdateProfile(AppUser appUser)
      {
         string cmdStr = "UPDATE appuser SET email=:email, title=:title, firstname=:firstname, lastname: lastname, facebook_user, facebook_password WHERE id=:id";
         using (MySqlCommand cmd = db.CreateCommand(cmdStr))
         {
            cmd.Parameters.Add(new MySqlParameter("email", appUser.Email));
            cmd.Parameters.Add(new MySqlParameter("title", appUser.Title));
            cmd.Parameters.Add(new MySqlParameter("firstname", appUser.Firstname));
            cmd.Parameters.Add(new MySqlParameter("lastname", appUser.Lastname));
            cmd.Parameters.Add(new MySqlParameter("facebook_user", appUser.FacebookUser));
            cmd.Parameters.Add(new MySqlParameter("facebook_password", appUser.FacebookPassword));
            cmd.Parameters.Add(new MySqlParameter("id", appUser.Id));
            await cmd.ExecuteNonQueryAsync();
         }
      }
   }
}
