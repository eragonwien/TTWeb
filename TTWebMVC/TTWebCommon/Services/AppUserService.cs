using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SNGCommon.Services;
using SNGCommon.Sql.MySql.Extensions;
using TTWebCommon.Models;
using TTWebCommon.Models.Common.Exceptions;
using TTWebCommon.Models.DataModels;

namespace TTWebCommon.Services
{
    public interface IAppUserService
    {
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByEmailAsync(string email);
        Task CreateUserAsync(AppUser user);
        Task UpdateUserAsync(AppUser user);
        Task DeleteUserAsync(int id);
        Task<bool> IsUserExistAsync(int id);
        Task<bool> IsUserEmailAvailableAsync(string email, int id);
        AppUser LoadContextUser(ClaimsPrincipal user);
        List<Claim> CreateUserClaims(AppUser appUser);
        Task TryUpdateFacebookPasswordAsync(int userId, int id, string username, string password);
        Task DeleteFacebookCredentialAsync(string username, int userId);
        Task<List<FacebookCredential>> GetFacebookCredentialsByUserIdAsync(int userId);
        Task TryUpdateFacebookFriendByUserIdAsync(int userId, int id, string name, string profileLink);
        Task DeleteFacebookFriendByUserIdAsync(string id, int userId);
        Task<List<FacebookFriend>> GetFacebookFriendsByUserIdAsync(int userId);
    }

    public class AppUserService : IAppUserService
    {
        private const string GetUserBaseCmdString =
            "SELECT appuser_id, email, title, firstname, lastname, disabled, active, role_id, role_name, fb_credential_id, fb_username, fb_password FROM v_appuser";

        private readonly TTWebDbContext db;
        private readonly IPasswordHelperService pwd;

        public AppUserService(TTWebDbContext db, IPasswordHelperService pwd)
        {
            this.db = db;
            this.pwd = pwd;
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            AppUser appUser = null;
            var cmdStr = GetUserBaseCmdString + " WHERE appuser_id=@appuser_id";
            await using var cmd = await db.CreateCommand(cmdStr);
            cmd.Parameters.Add(new MySqlParameter("appuser_id", id));
            using var odr = await cmd.ExecuteMySqlReaderAsync();
            while (await odr.ReadAsync()) appUser = await ReadAppUserDataReaderAsync(odr, appUser);
            return appUser;
        }

        public async Task<AppUser> GetUserByEmailAsync(string email)
        {
            AppUser appUser = null;
            var cmdStr = GetUserBaseCmdString + " WHERE email=@email";
            await using var cmd = await db.CreateCommand(cmdStr);
            cmd.Parameters.Add(new MySqlParameter("email", email));
            using var odr = await cmd.ExecuteMySqlReaderAsync();
            while (await odr.ReadAsync()) appUser = await ReadAppUserDataReaderAsync(odr, appUser);
            return appUser;
        }

        public async Task CreateUserAsync(AppUser user)
        {
            var cmdStr =
                "INSERT INTO appuser(email, title, firstname, lastname, active, role_id) VALUES(@email, @title, @firstname, @lastname, @active, @role_id)";
            await using var cmd = await db.CreateCommand(cmdStr);
            cmd.Parameters.Add(new MySqlParameter("email", user.Email));
            cmd.Parameters.Add(new MySqlParameter("title", user.Title));
            cmd.Parameters.Add(new MySqlParameter("firstname", user.Firstname));
            cmd.Parameters.Add(new MySqlParameter("lastname", user.Lastname));
            cmd.Parameters.Add(new MySqlParameter("active", false));
            cmd.Parameters.Add(new MySqlParameter("role_id", (int) UserRole.Standard));
            await cmd.ExecuteNonQueryAsync();
            // return ID hier
            user = await GetUserByEmailAsync(user.Email);
        }

        public async Task UpdateUserAsync(AppUser user)
        {
            await UpdateUserProfileAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            var cmdStr = "DELETE FROM appuser WHERE id=@id";
            await using var cmd = await db.CreateCommand(cmdStr);
            cmd.Parameters.Add(new MySqlParameter("id", id));
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<bool> IsUserExistAsync(int id)
        {
            var cmdStr = "SELECT CASE WHEN EXISTS(SELECT id FROM appuser WHERE id=:id) THEN 1 ELSE 0 FROM DUAL";
            await using var cmd = await db.CreateCommand(cmdStr);
            cmd.Parameters.Add(new MySqlParameter("id", id));
            return await cmd.ReadMySqlScalarBoolean();
        }

        public async Task<bool> IsUserEmailAvailableAsync(string email, int id)
        {
            var cmdStr =
                "SELECT CASE WHEN EXISTS(SELECT id FROM appuser WHERE email=:email AND id!=:id) THEN 1 ELSE 0 FROM DUAL";
            await using var cmd = await db.CreateCommand(cmdStr);
            cmd.Parameters.Add(new MySqlParameter("email", email));
            cmd.Parameters.Add(new MySqlParameter("id", id));
            return await cmd.ReadMySqlScalarBoolean();
        }

        public AppUser LoadContextUser(ClaimsPrincipal contextUser)
        {
            var user = new AppUser
            {
                Id = int.TryParse(contextUser.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId) ? userId : 0,
                Email = contextUser.FindFirst(ClaimTypes.Email)?.Value,
                Role = Enum.TryParse(contextUser.FindFirst(ClaimTypes.Role)?.Value, out UserRole parsedRole)
                    ? parsedRole
                    : UserRole.Standard,
                Firstname = contextUser.FindFirst(ClaimTypes.GivenName)?.Value,
                Lastname = contextUser.FindFirst(ClaimTypes.Surname)?.Value,
            };
            return user;
        }

        public List<Claim> CreateUserClaims(AppUser appUser)
        {
            var claims = new List<Claim>();

            if (appUser == null) return claims;

            claims.Add(new Claim(ClaimTypes.NameIdentifier.ToString(), appUser.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Email.ToString(), appUser.Email));
            claims.Add(new Claim(ClaimTypes.GivenName.ToString(), appUser.Firstname));
            claims.Add(new Claim(ClaimTypes.Surname.ToString(), appUser.Lastname));
            claims.Add(new Claim(ClaimTypes.Role, appUser.Role.ToString()));

            return claims;
        }

        public async Task DeleteFacebookCredentialAsync(string username, int userId)
        {
            var cmdStr = "DELETE FROM facebookcredential WHERE fb_username=@fb_username AND appuser_id=@appuser_id";
            await using var cmd = await db.CreateCommand(cmdStr);
            cmd.Parameters.Add(new MySqlParameter("fb_username", username));
            cmd.Parameters.Add(new MySqlParameter("appuser_id", userId));
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<List<FacebookCredential>> GetFacebookCredentialsByUserIdAsync(int userId)
        {
            var credentials = new List<FacebookCredential>();
            var cmdStr =
                @"SELECT fb_credential_id, fb_username, fb_password FROM v_appuser_facebook WHERE appuser_id=@appuser_id order by fb_credential_id desc";
            await using var cmd = await db.CreateCommand(cmdStr);
            cmd.Parameters.Add(new MySqlParameter("appuser_id", userId));
            using var odr = await cmd.ExecuteMySqlReaderAsync();
            while (await odr.ReadAsync())
            {
                var credential = new FacebookCredential
                {
                    Id = await odr.ReadMySqlIntegerAsync("fb_credential_id"),
                    Username = await odr.ReadMySqlStringAsync("fb_username"),
                    Password = pwd.SimpleDecrypt(await odr.ReadMySqlStringAsync("fb_password"))
                };
                if (credential.Id > 0 &&
                    !credentials.Any(c => c.Id == credential.Id && c.Username == credential.Username))
                    credentials.Add(credential);
            }

            return credentials;
        }

        public async Task TryUpdateFacebookPasswordAsync(int userId, int id, string username, string password)
        {
            if (id > 0)
                await UpdateFacebookCredential(userId, id, username, password);
            else
                await AddFacebookCredential(userId, username, password);
        }

        public async Task TryUpdateFacebookFriendByUserIdAsync(int userId, int id, string name, string profileLink)
        {
            if (id > 0)
                await UpdateFacebookFriend(id, name, profileLink);
            else
                await AddFacebookFriend(userId, name, profileLink);
        }

        public async Task DeleteFacebookFriendByUserIdAsync(string id, int userId)
        {
            var cmdStr = "DELETE FROM friend WHERE id=@id AND appuser_id=@appuser_id";
            await using var cmd = await db.CreateCommand(cmdStr);
            cmd.Parameters.Add(new MySqlParameter("id", id));
            cmd.Parameters.Add(new MySqlParameter("appuser_id", userId));
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<List<FacebookFriend>> GetFacebookFriendsByUserIdAsync(int userId)
        {
            var friends = new List<FacebookFriend>();
            var cmdStr = @"SELECT id, name, profile_link, active, disabled FROM friend WHERE appuser_id=@appuser_id";
            await using var cmd = await db.CreateCommand(cmdStr);
            cmd.Parameters.Add(new MySqlParameter("appuser_id", userId));
            using var odr = await cmd.ExecuteMySqlReaderAsync();
            while (await odr.ReadAsync())
                friends.Add(new FacebookFriend
                {
                    Id = await odr.ReadMySqlIntegerAsync("id"),
                    Name = await odr.ReadMySqlStringAsync("name"),
                    ProfileLink = await odr.ReadMySqlStringAsync("profile_link"),
                    Active = await odr.ReadMySqlBooleanAsync("active"),
                    Disabled = await odr.ReadMySqlBooleanAsync("disabled"),
                });
            return friends;
        }

        private async Task UpdateUserProfileAsync(AppUser appUser)
        {
            var cmdStr =
                "UPDATE appuser SET title=@title, firstname=@firstname, lastname=@lastname WHERE id=@id and email=@email";
            await using var cmd = await db.CreateCommand(cmdStr);
            cmd.Parameters.Add(new MySqlParameter("title", appUser.Title));
            cmd.Parameters.Add(new MySqlParameter("firstname", appUser.Firstname));
            cmd.Parameters.Add(new MySqlParameter("lastname", appUser.Lastname));
            cmd.Parameters.Add(new MySqlParameter("id", appUser.Id));
            cmd.Parameters.Add(new MySqlParameter("email", appUser.Email));
            var count = await cmd.ExecuteNonQueryAsync();

            if (count == 0)
                throw new WebApiNotFoundException("Appuser email={0}, id={1} does not exist", appUser.Email,
                    appUser.Id);
        }

        #region Privat Methods

        private async Task<AppUser> ReadAppUserDataReaderAsync(MySqlDataReader odr, AppUser appUser)
        {
            if (appUser == null)
                appUser = new AppUser
                {
                    Id = await odr.ReadMySqlIntegerAsync("appuser_id"),
                    Email = await odr.ReadMySqlStringAsync("email"),
                    Title = await odr.ReadMySqlStringAsync("title"),
                    Firstname = await odr.ReadMySqlStringAsync("firstname"),
                    Lastname = await odr.ReadMySqlStringAsync("lastname"),
                    Disabled = await odr.ReadMySqlBooleanAsync("disabled"),
                    Active = await odr.ReadMySqlBooleanAsync("active"),
                    RoleId = await odr.ReadMySqlIntegerAsync("role_id"),
                    Role = await odr.ReadMySqlEnumAsync<UserRole>("role_name")
                };

            var credentialId = await odr.ReadMySqlIntegerAsync("fb_credential_id");

            if (credentialId > 0)
            {
                var fbCredential = new FacebookCredential
                {
                    Id = credentialId,
                    Username = await odr.ReadMySqlStringAsync("fb_username"),
                    Password = pwd.SimpleDecrypt(await odr.ReadMySqlStringAsync("fb_password"))
                };
                if (!appUser.FacebookCredentials.Any(c => c.Id == fbCredential.Id))
                    appUser.FacebookCredentials.Add(fbCredential);
            }


            return appUser;
        }

        private async Task AddFacebookCredential(int userId, string username, string password)
        {
            var cmdStr =
                "INSERT INTO facebookcredential(appuser_id, fb_username, fb_password) VALUES(@appuser_id, @fb_username, @fb_password)";
            await using var cmd = await db.CreateCommand(cmdStr);
            cmd.Parameters.Add(new MySqlParameter("appuser_id", userId));
            cmd.Parameters.Add(new MySqlParameter("fb_username", username));
            cmd.Parameters.Add(new MySqlParameter("fb_password", pwd.SimpleEncrypt(password)));
            await cmd.ExecuteNonQueryAsync();
        }

        private async Task UpdateFacebookCredential(int userId, int id, string username, string password)
        {
            var cmdStr =
                "UPDATE facebookcredential SET fb_username=@fb_username, fb_password=@fb_password where id=@id AND appuser_id=@appuser_id";
            await using var cmd = await db.CreateCommand(cmdStr);
            cmd.Parameters.Add(new MySqlParameter("fb_username", username));
            cmd.Parameters.Add(new MySqlParameter("fb_password", pwd.SimpleEncrypt(password)));
            cmd.Parameters.Add(new MySqlParameter("id", id));
            cmd.Parameters.Add(new MySqlParameter("appuser_id", userId));
            await cmd.ExecuteNonQueryAsync();
        }

        private async Task UpdateFacebookFriend(int id, string name, string profileLink)
        {
            var cmdStr = "UPDATE friend SET name=@name, profile_link=@profile_link where id=@id";
            await using var cmd = await db.CreateCommand(cmdStr);
            cmd.Parameters.Add(new MySqlParameter("name", name));
            cmd.Parameters.Add(new MySqlParameter("profile_link", profileLink));
            cmd.Parameters.Add(new MySqlParameter("id", id));
            await cmd.ExecuteNonQueryAsync();
        }

        private async Task AddFacebookFriend(int userId, string name, string profileLink)
        {
            var cmdStr = "INSERT INTO friend(appuser_id, name, profile_link) VALUES(@appuser_id, @name, @profile_link)";
            await using var cmd = await db.CreateCommand(cmdStr);
            cmd.Parameters.Add(new MySqlParameter("appuser_id", userId));
            cmd.Parameters.Add(new MySqlParameter("name", name));
            cmd.Parameters.Add(new MySqlParameter("profile_link", profileLink));
            await cmd.ExecuteNonQueryAsync();
        }

        #endregion
    }
}