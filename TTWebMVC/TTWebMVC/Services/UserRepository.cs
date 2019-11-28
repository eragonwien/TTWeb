using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;
using TTWebMVC.Models;
using SNGCommon.Common;

namespace TTWebMVC.Services
{
   public interface IUserRepository
   {
      Task Create(AppUser appUser);
      Task Create(Partner partner);
      Task<AppUser> GetUser(string email);
      Task<Partner> GetPartner(string email);
      Task Update(AppUser appUser);
      Task Update(Partner partner);
      Task Delete(long id);
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
            string cmdStr = "INSERT INTO appuser(email, firstname, lastname, facebook_id, access_token, access_token_expiration_date, create_date) VALUES(@email, @firstname, @lastname, @facebook_id, @access_token, @access_token_expiration_date, @create_date)";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               cmd.Parameters.Add(new MySqlParameter("@email", user.Email));
               cmd.Parameters.Add(new MySqlParameter("@firstname", user.Firstname));
               cmd.Parameters.Add(new MySqlParameter("@lastname", user.Lastname));
               cmd.Parameters.Add(new MySqlParameter("@facebook_id", user.FacebookId));
               cmd.Parameters.Add(new MySqlParameter("@access_token", user.AccessToken));
               cmd.Parameters.Add(new MySqlParameter("@access_token_expiration_date", user.AccessTokenExpirationDate));
               cmd.Parameters.Add(new MySqlParameter("@create_date", DateTime.Now));
               await con.OpenAsync();
               await cmd.ExecuteNonQueryAsync();
            }
         }
      }

      public Task Create(Partner partner)
      {
         throw new NotImplementedException();
      }

      public Task Delete(long id)
      {
         throw new NotImplementedException();
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

      public Task<Partner> GetPartner(string email)
      {
         throw new NotImplementedException();
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

      public Task Update(AppUser appUser)
      {
         throw new NotImplementedException();
      }

      public Task Update(Partner partner)
      {
         throw new NotImplementedException();
      }
   }
}
