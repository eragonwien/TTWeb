using MySql.Data.MySqlClient;
using SNGCommon.Sql.MySql.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using TTWebCommon.Models;

namespace TTWebApi.Services
{
   public interface IAppUserService
   {
      Task<AppUser> GetOne(string email);
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
         string cmdStr = "INSERT INTO appuser(email, title, firstname, lastname, active) VALUES(@email, @title, @firstname, @lastname, @active)";
         using MySqlCommand cmd = db.CreateCommand(cmdStr);
         cmd.Parameters.Add(new MySqlParameter("email", user.Email));
         cmd.Parameters.Add(new MySqlParameter("title", user.Title));
         cmd.Parameters.Add(new MySqlParameter("firstname", user.Firstname));
         cmd.Parameters.Add(new MySqlParameter("lastname", user.Lastname));
         cmd.Parameters.Add(new MySqlParameter("active", false));
         await cmd.ExecuteNonQueryAsync();
      }

      public async Task<bool> Exist(int id)
      {
         string cmdStr = "SELECT CASE WHEN EXISTS(SELECT id FROM appuser WHERE id=:id) THEN 1 ELSE 0 FROM DUAL";
         using MySqlCommand cmd = db.CreateCommand(cmdStr);
         cmd.Parameters.Add(new MySqlParameter("id", id));
         return await cmd.ReadMySqlScalarBoolean();
      }

      public async Task<AppUser> GetOne(string email)
      {
         AppUser appUser = null;
         string cmdStr = "SELECT appuser_id, email, title, firstname, lastname, disabled, active, facebook_user, role_name FROM v_appuser WHERE email=@email";
         using MySqlCommand cmd = db.CreateCommand(cmdStr);
         cmd.Parameters.Add(new MySqlParameter("email", email));
         using MySqlDataReader odr = await cmd.ExecuteMySqlReaderAsync();
         while (await odr.ReadAsync())
         {
            appUser ??= await ReadAppUserDataReaderAsync(odr);
            UserRoleEnum role = await odr.ReadMySqlEnumAsync("role_name", UserRoleEnum.ERROR);
            if (role != UserRoleEnum.ERROR && !appUser.Roles.Contains(role))
            {
               appUser.Roles.Add(role);
            }
         }
         return appUser;
      }

      private async Task<AppUser> ReadAppUserDataReaderAsync(MySqlDataReader odr)
      {
         var appUser = new AppUser
         {
            Id = await odr.ReadMySqlIntegerAsync("appuser_id"),
            Email = await odr.ReadMySqlStringAsync("email"),
            Title = await odr.ReadMySqlStringAsync("title"),
            Firstname = await odr.ReadMySqlStringAsync("firstname"),
            Lastname = await odr.ReadMySqlStringAsync("lastname"),
            Disabled = await odr.ReadMySqlBooleanAsync("disabled"),
            Active = await odr.ReadMySqlBooleanAsync("active"),
            FacebookUser = await odr.ReadMySqlStringAsync("facebook_user")
         };
         return appUser;
      }

      public async Task<bool> IsEmailAvailable(string email, int id)
      {
         string cmdStr = "SELECT CASE WHEN EXISTS(SELECT id FROM appuser WHERE email=:email AND id!=:id) THEN 1 ELSE 0 FROM DUAL";
         using MySqlCommand cmd = db.CreateCommand(cmdStr);
         cmd.Parameters.Add(new MySqlParameter("email", email));
         cmd.Parameters.Add(new MySqlParameter("id", id));
         return await cmd.ReadMySqlScalarBoolean();
      }

      public async Task Remove(int id)
      {
         string cmdStr = "DELETE FROM appuser WHERE id=@id";
         using MySqlCommand cmd = db.CreateCommand(cmdStr);
         cmd.Parameters.Add(new MySqlParameter("id", id));
         await cmd.ExecuteNonQueryAsync();
      }

      public async Task UpdateProfile(AppUser appUser)
      {
         string cmdStr = "UPDATE appuser SET email=@email, title=@title, firstname=@firstname, lastname=@lastname, facebook_user=@facebook_user, facebook_password=@facebook_password WHERE id=@id";
         using MySqlCommand cmd = db.CreateCommand(cmdStr);
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
