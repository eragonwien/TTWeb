using MySql.Data.MySqlClient;
using SNGCommon;
using SNGCommon.Services;
using SNGCommon.Sql.MySql.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TTWebCommon.Models;

namespace TTWebApi.Services
{
   public interface IAppUserService
   {
      Task<AppUser> GetOne(int id);
      Task<AppUser> GetOne(string email);
      Task Create(AppUser user);
      Task Remove(int id);
      Task<bool> Exist(int id);
      Task UpdateProfile(AppUser appUser);
      Task<bool> IsEmailAvailable(string email, int id);
      AppUser LoadContextUser(ClaimsPrincipal user);
      List<Claim> CreateUserClaims(AppUser appUser);
      Task TryAddFacebookPassword(int userId, int id, string username, string password);
      Task DeleteFacebookCredential(int id, int userId);
      Task<List<FacebookCredential>> FacebookCredentials(int userId);
   }
   public class AppUserService : IAppUserService
   {
      private readonly TTWebDbContext db;
      private readonly IPasswordHelperService pwd;

      public AppUserService(TTWebDbContext db, IPasswordHelperService pwd)
      {
         this.db = db;
         this.pwd = pwd;
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

      public async Task<AppUser> GetOne(int id)
      {
         AppUser appUser = null;
         string cmdStr = "SELECT appuser_id, email, title, firstname, lastname, disabled, active, role_name FROM v_appuser WHERE appuser_id=@appuser_id";
         using MySqlCommand cmd = db.CreateCommand(cmdStr);
         cmd.Parameters.Add(new MySqlParameter("appuser_id", id));
         using MySqlDataReader odr = await cmd.ExecuteMySqlReaderAsync();
         while (await odr.ReadAsync())
         {
            appUser = await ReadAppUserDataReaderAsync(odr);
         }
         return appUser;
      }

      public async Task<AppUser> GetOne(string email)
      {
         AppUser appUser = null;
         string cmdStr = "SELECT appuser_id, email, title, firstname, lastname, disabled, active, role_name FROM v_appuser WHERE email=@email";
         using MySqlCommand cmd = db.CreateCommand(cmdStr);
         cmd.Parameters.Add(new MySqlParameter("email", email));
         using MySqlDataReader odr = await cmd.ExecuteMySqlReaderAsync();
         while (await odr.ReadAsync())
         {
            appUser = await ReadAppUserDataReaderAsync(odr);
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
            Role = await odr.ReadMySqlEnumAsync<UserRole>("role_name")
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
         string cmdStr = "UPDATE appuser SET title=@title, firstname=@firstname, lastname=@lastname WHERE id=@id and email=@email";
         using MySqlCommand cmd = db.CreateCommand(cmdStr);
         cmd.Parameters.Add(new MySqlParameter("title", appUser.Title));
         cmd.Parameters.Add(new MySqlParameter("firstname", appUser.Firstname));
         cmd.Parameters.Add(new MySqlParameter("lastname", appUser.Lastname));
         cmd.Parameters.Add(new MySqlParameter("id", appUser.Id));
         cmd.Parameters.Add(new MySqlParameter("email", appUser.Email));
         await cmd.ExecuteNonQueryAsync();
      }

      public AppUser LoadContextUser(ClaimsPrincipal contextUser)
      {
         var user = new AppUser
         {
            Id = int.TryParse(contextUser.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId) ? userId : 0,
            Email = contextUser.FindFirst(ClaimTypes.Email)?.Value,
            Role = Enum.TryParse(contextUser.FindFirst(ClaimTypes.Role)?.Value, out UserRole parsedRole) ? parsedRole : UserRole.NONE
         };
         return user;
      }

      public List<Claim> CreateUserClaims(AppUser appUser)
      {
         var claims = new List<Claim>();

         if (appUser == null)
         {
            return claims;
         }

         claims.Add(new Claim(ClaimTypes.NameIdentifier.ToString(), appUser.Id.ToString()));
         claims.Add(new Claim(ClaimTypes.Email.ToString(), appUser.Email));
         claims.Add(new Claim(ClaimTypes.Role, appUser.Role.ToString()));

         return claims;
      }

      private async Task AddFacebookCredential(int userId, string username, string password)
      {
         if (!await FacebookCredentialsExists(userId, username))
         {
            string cmdStr = "INSERT INTO facebookcredentials(appuser_id, fb_username, fb_password) VALUES(@appuser_id, @fb_username, @fb_password)";
            using MySqlCommand cmd = db.CreateCommand(cmdStr);
            cmd.Parameters.Add(new MySqlParameter("appuser_id", userId));
            cmd.Parameters.Add(new MySqlParameter("fb_username", username));
            cmd.Parameters.Add(new MySqlParameter("fb_password", pwd.SimpleEncrypt(password)));
            await cmd.ExecuteNonQueryAsync();
         }
      }

      private async Task<bool> FacebookCredentialsExists(int userId, string username)
      {
         string cmdStr = "SELECT CASE WHEN EXISTS(SELECT id FROM facebookcredentials WHERE appuser_id=@appuser_id AND fb_username=@fb_username) THEN 1 ELSE 0 END FROM DUAL";
         using MySqlCommand cmd = db.CreateCommand(cmdStr);
         cmd.Parameters.Add(new MySqlParameter("appuser_id", userId));
         cmd.Parameters.Add(new MySqlParameter("fb_username", username));
         return await cmd.ReadMySqlScalarBoolean();
      }

      private async Task UpdateFacebookPassword(int userId, int id, string username, string password)
      {
         string cmdStr = "UPDATE facebookcredentials SET fb_password=@fb_password where id=@id AND appuser_id=@appuser_id AND fb_username=@fb_username";
         using MySqlCommand cmd = db.CreateCommand(cmdStr);
         cmd.Parameters.Add(new MySqlParameter("fb_password", pwd.SimpleEncrypt(password)));
         cmd.Parameters.Add(new MySqlParameter("id", id));
         cmd.Parameters.Add(new MySqlParameter("appuser_id", userId));
         cmd.Parameters.Add(new MySqlParameter("fb_username", username));
         await cmd.ExecuteNonQueryAsync();
      }

      public async Task DeleteFacebookCredential(int id, int userId)
      {
         string cmdStr = "DELETE FROM facebookcredentials WHERE id=@id AND appuser_id=@appuser_id";
         using MySqlCommand cmd = db.CreateCommand(cmdStr);
         cmd.Parameters.Add(new MySqlParameter("id", id));
         cmd.Parameters.Add(new MySqlParameter("appuser_id", userId));
         await cmd.ExecuteNonQueryAsync();
      }

      public async Task<List<FacebookCredential>> FacebookCredentials(int userId)
      {
         List<FacebookCredential> credentials = new List<FacebookCredential>();
         string cmdStr = @"SELECT appuser_id, fb_username FROM v_appuser_facebook WHERE appuser_id=@appuser_id order by fb_credential_id desc";
         using MySqlCommand cmd = db.CreateCommand(cmdStr);
         cmd.Parameters.Add(new MySqlParameter("appuser_id", userId));
         using MySqlDataReader odr = await cmd.ExecuteMySqlReaderAsync();
         while (await odr.ReadAsync())
         {
            var credential = new FacebookCredential
            {
               Id = await odr.ReadMySqlIntegerAsync("appuser_id"),
               Username = await odr.ReadMySqlStringAsync("fb_username")
            };
            if (credential.Id > 0 && !credentials.Any(c => c.Id == credential.Id && c.Username == credential.Username))
            {
               credentials.Add(credential);
            }
         }
         return credentials;
      }

      public async Task TryAddFacebookPassword(int userId, int id, string username, string password)
      {
         if (!await FacebookCredentialsExists(userId, username))
         {
            await AddFacebookCredential(userId, username, password);
         }
         else
         {
            await UpdateFacebookPassword(userId, id, username, password);
         }
      }
   }
}
