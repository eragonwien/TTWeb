using MySql.Data.MySqlClient;
using SNGCommon.Common;
using System;
using System.Threading.Tasks;
using TTWebMVC.Models;

namespace TTWebMVC.Services
{
   public interface IUserRepository
   {
      Task Create(AppUser appUser);
      Task<AppUser> GetUser(string email);
      Task Update(AppUser appUser);
      Task<bool> Exists(string email);
   }

   public class UserRepository : IUserRepository
   {
      private readonly DatabaseContext db;

      public UserRepository(DatabaseContext db)
      {
         this.db = db;
      }

      public async Task Create(AppUser user)
      {
         using (var con = db.GetConnection())
         {
            string cmdStr = "INSERT INTO appuser(email, firstname, lastname, facebook_id, access_token, access_token_expiration_date) VALUES(@email, @firstname, @lastname, @facebook_id, @access_token, @access_token_expiration_date)";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@email", user.Email));
               cmd.Parameters.Add(new MySqlParameter("@firstname", user.Firstname));
               cmd.Parameters.Add(new MySqlParameter("@lastname", user.Lastname));
               cmd.Parameters.Add(new MySqlParameter("@facebook_id", user.FacebookId));
               cmd.Parameters.Add(new MySqlParameter("@access_token", user.AccessToken));
               cmd.Parameters.Add(new MySqlParameter("@access_token_expiration_date", ((DateTimeOffset)user.AccessTokenExpirationDate).ToTimeStamp()));
               await con.OpenAsync();
               await cmd.ExecuteNonQueryAsync();
            }
         }
      }

      public async Task<bool> Exists(string email)
      {
         using (var con = db.GetConnection())
         {
            string cmdStr = @"SELECT EXISTS(SELECT * FROM appuser WHERE email=@email) FROM DUAL";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@email", email));
               await con.OpenAsync();
               return cmd.ReadScalarBoolean();
            }
         }
      }

      public async Task<AppUser> GetUser(string email)
      {
         AppUser user = new AppUser();
         using (var con = db.GetConnection())
         {
            string cmdStr = @"SELECT id, email, firstname, lastname, facebook_id, access_token, access_token_expiration_date FROM appuser where email=@email";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@email", email));
               await con.OpenAsync();
               using (var odr = await cmd.ExecuteReaderAsync())
               {
                  if (await odr.ReadAsync())
                  {
                     user = new AppUser
                     {
                        Id = await odr.ReadInt64Async("id"),
                        Email = await odr.ReadStringAsync("email"),
                        Firstname = await odr.ReadStringAsync("firstname"),
                        Lastname = await odr.ReadStringAsync("lastname"),
                        FacebookId = await odr.ReadStringAsync("facebook_id"),
                        AccessToken = await odr.ReadStringAsync("access_token"),
                        AccessTokenExpirationDate = await odr.ReadDateTimeAsync("access_token_expiration_date")
                     };
                  }
               }
            }
         }
         return user;
      }

      public async Task Update(AppUser user)
      {
         using (var con = db.GetConnection())
         {
            string cmdStr = "UPDATE appuser set firstname=@firstname, lastname=@lastname, facebook_id=@facebook_id, access_token=@access_token, access_token_expiration_date=@access_token_expiration_date where email=@email";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@firstname", user.Firstname));
               cmd.Parameters.Add(new MySqlParameter("@lastname", user.Lastname));
               cmd.Parameters.Add(new MySqlParameter("@facebook_id", user.FacebookId));
               cmd.Parameters.Add(new MySqlParameter("@access_token", user.AccessToken));
               cmd.Parameters.Add(new MySqlParameter("@access_token_expiration_date", user.AccessTokenExpirationDate));
               cmd.Parameters.Add(new MySqlParameter("@email", user.Email));
               await con.OpenAsync();
               await cmd.ExecuteNonQueryAsync();
            }
         }
      }
   }
}
