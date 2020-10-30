using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SNGCommon.Services;
using SNGCommon.Sql.MySql.Extensions;
using TTWebCommon.Models;
using TTWebCommon.Models.Common.Exceptions;

namespace TTWebCommon.Services
{
    public interface IFacebookCredentialService
    {
        Task<IEnumerable<FacebookCredential>> GetFacebookCredentialsAsync();
        Task<FacebookCredential> GetFacebookCredentialByIdAsync(int id, int appuserId);
        Task CreateFacebookCredentialAsync(FacebookCredential facebookCredential);
        Task UpdateFacebookCredentialAsync(FacebookCredential facebookCredential);
        Task DeleteFacebookCredentialAsync(int id, int appuserId);
    }
    public class FacebookCredentialService : IFacebookCredentialService
    {
        private readonly TTWebDbContext db;
        private readonly IPasswordHelperService pwd;

        public FacebookCredentialService(TTWebDbContext db, IPasswordHelperService pwd)
        {
            this.db = db;
            this.pwd = pwd;
        }

        public async Task<IEnumerable<FacebookCredential>> GetFacebookCredentialsAsync()
        {
            List<FacebookCredential> credentials = new List<FacebookCredential>();
            string cmdStr = @"SELECT appuser_id, fb_credential_id, fb_username, fb_password FROM v_appuser WHERE fb_credential_id is not null ORDER BY fb_credential_id DESC";
            await using MySqlCommand cmd = await db.CreateCommand(cmdStr);
            using MySqlDataReader odr = await cmd.ExecuteMySqlReaderAsync();
            while (await odr.ReadAsync())
                credentials.Add(await ReadFacebookCredential(odr));

            return credentials;
        }

        public async Task<FacebookCredential> GetFacebookCredentialByIdAsync(int id, int appuserId)
        {
            string cmdStr = @"SELECT appuser_id, fb_credential_id, fb_username, fb_password FROM v_appuser WHERE fb_credential_id=@fb_credential_id AND appuser_id=@appuser_id ORDER BY fb_credential_id DESC";
            await using MySqlCommand cmd = await db.CreateCommand(cmdStr);
            cmd.Parameters.Add(new MySqlParameter("fb_credential_id", id)); 
            cmd.Parameters.Add(new MySqlParameter("appuser_id", appuserId)); 
            using MySqlDataReader odr = await cmd.ExecuteMySqlReaderAsync();
            if (await odr.ReadAsync())
                return await ReadFacebookCredential(odr);

            return null;
        }

        public async Task CreateFacebookCredentialAsync(FacebookCredential facebookCredential)
        {
            string cmdStr = "INSERT INTO facebookcredential(appuser_id, fb_username, fb_password) VALUES(@appuser_id, @fb_username, @fb_password)";
            await using MySqlCommand cmd = await db.CreateCommand(cmdStr);
            cmd.Parameters.Add(new MySqlParameter("appuser_id", facebookCredential.AppUserId));
            cmd.Parameters.Add(new MySqlParameter("fb_username", facebookCredential.Username));
            cmd.Parameters.Add(new MySqlParameter("fb_password", pwd.SimpleEncrypt(facebookCredential.Password)));
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateFacebookCredentialAsync(FacebookCredential facebookCredential)
        {
            string cmdStr = "UPDATE facebookcredential SET fb_username=@fb_username, fb_password=@fb_password where id=@id AND appuser_id=@appuser_id";
            await using MySqlCommand cmd = await db.CreateCommand(cmdStr);
            cmd.Parameters.Add(new MySqlParameter("fb_username", facebookCredential.Username));
            cmd.Parameters.Add(new MySqlParameter("fb_password", pwd.SimpleEncrypt(facebookCredential.Password)));
            cmd.Parameters.Add(new MySqlParameter("id", facebookCredential.Id));
            cmd.Parameters.Add(new MySqlParameter("appuser_id", facebookCredential.AppUserId));
            int updateCount = await cmd.ExecuteNonQueryAsync();

            if (updateCount == 0)
                throw new WebApiNotFoundException("Facebook credentials with id={0} and appuser_id={1} does not exist", facebookCredential.Id, facebookCredential.AppUserId);
        }

        public async Task DeleteFacebookCredentialAsync(int id, int appuserId)
        {
            string cmdStr = "DELETE FROM facebookcredential WHERE id=@id AND appuser_id=@appuser_id";
            await using MySqlCommand cmd = await db.CreateCommand(cmdStr);
            cmd.Parameters.Add(new MySqlParameter("id", id));
            cmd.Parameters.Add(new MySqlParameter("appuser_id", appuserId));
            await cmd.ExecuteNonQueryAsync();
        }

        private async Task<FacebookCredential> ReadFacebookCredential(MySqlDataReader odr)
        {
            return new FacebookCredential
            {
                Id = await odr.ReadMySqlIntegerAsync("fb_credential_id"),
                Username = await odr.ReadMySqlStringAsync("fb_username"),
                Password = pwd.SimpleDecrypt(await odr.ReadMySqlStringAsync("fb_password")),
                AppUserId = await odr.ReadMySqlIntegerAsync("appuser_id"),
            };
        }
    }
}
