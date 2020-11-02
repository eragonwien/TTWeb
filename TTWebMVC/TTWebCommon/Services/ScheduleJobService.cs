using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SNGCommon.Sql.MySql.Extensions;
using TTWebCommon.Models;
using TTWebCommon.Models.DataModels;

namespace TTWebCommon.Services
{
    public interface IScheduleJobService
    {
        Task AddScheduleJobDef(ScheduleJobDef scheduleJobDef);
        Task<List<ScheduleJobDef>> GetScheduleJobDefs(int userId);
        Task<List<ScheduleJobDef>> GetOpeningScheduleJobDefs();
        Task<ScheduleJobDef> GetScheduleJobDef(int id, int userId);
        Task RemoveScheduleJobDef(int id, int appUserId);
        Task ToggleActive(int id, bool active);
        Task UpdateScheduleJobDef(ScheduleJobDef scheduleJobDef);
        Task<List<ScheduleWeekDay>> GetScheduleWeekDays();
        Task AddScheduleJobDetail(ScheduleJobDetail detail);
    }

    public class ScheduleJobService : IScheduleJobService
    {
        private const string ScheduleJobDefSelect =
            @"SELECT id, appuser_id, friend_id, facebookcredential_id, name, type, 
             interval_type, time_from, time_to, timezone_id, active,
             appuser_email, appuser_title, appuser_firstname, appuser_lastname, appuser_disabled, appuser_active, appuser_role, 
             friend_name, friend_profile_link, friend_disabled, scheduleweekday_ids
             FROM v_schedulejobdef";

        private const string OpeningScheduleJobDefSelect =
            @"SELECT id, appuser_id, friend_id, facebookcredential_id, name, type, 
             interval_type, time_from, time_to, timezone_id, active FROM v_open_schedulejob LIMIT 1";

        private readonly TTWebDbContext db;

        public ScheduleJobService(TTWebDbContext db)
        {
            this.db = db;
        }

        public async Task AddScheduleJobDef(ScheduleJobDef def)
        {
            var cmdStr =
                @"INSERT INTO schedulejobdef(appuser_id, friend_id, facebookcredential_id, name, type, interval_type, time_from, time_to, timezone_id, active) 
            VALUES(@appuser_id, @friend_id, @facebookcredential_id, @name, @type, @interval_type, @time_from, @time_to, @timezone_id, @active)";
            await using var cmd = await db.CreateCommand(cmdStr);
            cmd.Parameters.Add(new MySqlParameter("appuser_id", def.AppUserId));
            cmd.Parameters.Add(new MySqlParameter("friend_id", def.FriendId));
            cmd.Parameters.Add(new MySqlParameter("facebookcredential_id", def.FacebookCredentialId));
            cmd.Parameters.Add(new MySqlParameter("name", def.Name));
            cmd.Parameters.Add(new MySqlParameter("type", def.Type.ToString()));
            cmd.Parameters.Add(new MySqlParameter("interval_type", def.IntervalType.ToString()));
            cmd.Parameters.Add(new MySqlParameter("time_from", def.TimeFrom));
            cmd.Parameters.Add(new MySqlParameter("time_to", def.TimeTo));
            cmd.Parameters.Add(new MySqlParameter("timezone_id", def.TimeZone));
            cmd.Parameters.Add(new MySqlParameter("active", def.Active ? 1 : 0));
            await cmd.ExecuteNonQueryAsync();

            await UpdateScheduleDefJobWeekDays(def);
        }

        public async Task<ScheduleJobDef> GetScheduleJobDef(int id, int userId)
        {
            var scheduleJobDef = new ScheduleJobDef();
            var cmdStr = ScheduleJobDefSelect + " WHERE id=@id AND appuser_id=@appuser_id";
            await using var cmd = await db.CreateCommand(cmdStr);
            cmd.Parameters.Add(new MySqlParameter("id", id));
            cmd.Parameters.Add(new MySqlParameter("appuser_id", userId));
            using var odr = await cmd.ExecuteMySqlReaderAsync();
            if (await odr.ReadAsync()) scheduleJobDef = await ReadScheduleJobDefAsync(odr);
            return scheduleJobDef;
        }

        public async Task<List<ScheduleJobDef>> GetScheduleJobDefs(int userId)
        {
            var defs = new List<ScheduleJobDef>();
            var cmdStr = ScheduleJobDefSelect + " WHERE appuser_id=@appuser_id";
            await using var cmd = await db.CreateCommand(cmdStr);
            cmd.Parameters.Add(new MySqlParameter("appuser_id", userId));
            using var odr = await cmd.ExecuteMySqlReaderAsync();
            while (await odr.ReadAsync())
            {
                var scheduleJobDef = await ReadScheduleJobDefAsync(odr);
                defs.Add(scheduleJobDef);
            }

            return defs.ToList();
        }

        public async Task<List<ScheduleJobDef>> GetOpeningScheduleJobDefs()
        {
            var defs = new List<ScheduleJobDef>();
            var cmdStr = OpeningScheduleJobDefSelect;
            await using var cmd = await db.CreateCommand(cmdStr);
            using var odr = await cmd.ExecuteMySqlReaderAsync();
            while (await odr.ReadAsync())
            {
                var scheduleJobDef = await ReadOpenScheduleJobDefAsync(odr);
                defs.Add(scheduleJobDef);
            }

            return defs.ToList();
        }

        public async Task RemoveScheduleJobDef(int id, int appUserId)
        {
            var cmdStr = @"DELETE FROM schedulejobdef WHERE id=@id AND appuser_id=@appuser_id";
            await using var cmd = await db.CreateCommand(cmdStr);
            cmd.Parameters.Add(new MySqlParameter("id", id));
            cmd.Parameters.Add(new MySqlParameter("appuser_id", appUserId));
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task ToggleActive(int id, bool active)
        {
            var cmdStr = @"UPDATE schedulejobdef SET active=@active WHERE id=@id";
            await using var cmd = await db.CreateCommand(cmdStr);
            cmd.Parameters.Add(new MySqlParameter("active", active));
            cmd.Parameters.Add(new MySqlParameter("id", id));
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateScheduleJobDef(ScheduleJobDef scheduleJobDef)
        {
            var cmdStr =
                @"UPDATE schedulejobdef SET friend_id=@friend_id, facebookcredential_id=@facebookcredential_id, 
            name=@name, type=@type, interval_type=@interval_type, time_from=@time_from, time_to=@time_to, 
            timezone_id=@timezone_id, active=@active WHERE id=@id and appuser_id=@appuser_id";
            await using var cmd = await db.CreateCommand(cmdStr);
            cmd.Parameters.Add(new MySqlParameter("friend_id", scheduleJobDef.FriendId));
            cmd.Parameters.Add(new MySqlParameter("facebookcredential_id", scheduleJobDef.FacebookCredentialId));
            cmd.Parameters.Add(new MySqlParameter("name", scheduleJobDef.Name));
            cmd.Parameters.Add(new MySqlParameter("type", scheduleJobDef.Type));
            cmd.Parameters.Add(new MySqlParameter("interval_type", scheduleJobDef.IntervalType));
            cmd.Parameters.Add(new MySqlParameter("time_from", scheduleJobDef.TimeFrom));
            cmd.Parameters.Add(new MySqlParameter("time_to", scheduleJobDef.TimeTo));
            cmd.Parameters.Add(new MySqlParameter("timezone_id", scheduleJobDef.TimeZone));
            cmd.Parameters.Add(new MySqlParameter("id", scheduleJobDef.Id));
            cmd.Parameters.Add(new MySqlParameter("active", scheduleJobDef.Active ? 1 : 0));
            cmd.Parameters.Add(new MySqlParameter("appuser_id", scheduleJobDef.AppUserId));
            await cmd.ExecuteNonQueryAsync();

            await UpdateScheduleDefJobWeekDays(scheduleJobDef);
        }

        public async Task<List<ScheduleWeekDay>> GetScheduleWeekDays()
        {
            var weekdays = new List<ScheduleWeekDay>();
            var cmdStr = @"SELECT id, name, display_text FROM scheduleweekday";
            await using var cmd = await db.CreateCommand(cmdStr);
            await using var odr = await cmd.ExecuteMySqlReaderAsync();
            while (await odr.ReadAsync())
                weekdays.Add(new ScheduleWeekDay
                {
                    Id = await odr.ReadMySqlIntegerAsync("id"),
                    Name = await odr.ReadMySqlEnumAsync<WeekDayEnum>("name"),
                    DisplayText = await odr.ReadMySqlStringAsync("display_text"),
                });
            return weekdays;
        }

        public Task AddScheduleJobDetail(ScheduleJobDetail detail)
        {
            throw new NotImplementedException();
        }

        private async Task<ScheduleJobDef> ReadScheduleJobDefAsync(MySqlDataReader odr)
        {
            return new ScheduleJobDef
            {
                Id = await odr.ReadMySqlIntegerAsync("id"),
                AppUserId = await odr.ReadMySqlIntegerAsync("appuser_id"),
                Name = await odr.ReadMySqlStringAsync("name"),
                FriendId = await odr.ReadMySqlIntegerAsync("friend_id"),
                FacebookCredentialId = await odr.ReadMySqlIntegerAsync("facebookcredential_id"),
                Type = await odr.ReadMySqlEnumAsync<ScheduleJobType>("type"),
                IntervalType = await odr.ReadMySqlEnumAsync<IntervalTypeEnum>("interval_type"),
                TimeFrom = await odr.ReadMySqlStringAsync("time_from"),
                TimeTo = await odr.ReadMySqlStringAsync("time_to"),
                TimeZone = await odr.ReadMySqlStringAsync("timezone_id"),
                Active = await odr.ReadMySqlBooleanAsync("active"),
                AppUser = new AppUser
                {
                    Id = await odr.ReadMySqlIntegerAsync("appuser_id"),
                    Email = await odr.ReadMySqlStringAsync("appuser_email"),
                    Title = await odr.ReadMySqlStringAsync("appuser_title"),
                    Firstname = await odr.ReadMySqlStringAsync("appuser_firstname"),
                    Lastname = await odr.ReadMySqlStringAsync("appuser_lastname"),
                    Disabled = await odr.ReadMySqlBooleanAsync("appuser_disabled"),
                    Active = await odr.ReadMySqlBooleanAsync("appuser_active"),
                    Role = await odr.ReadMySqlEnumAsync<UserRole>("appuser_role"),
                },
                Friend = new FacebookFriend
                {
                    Id = await odr.ReadMySqlIntegerAsync("friend_id"),
                    Name = await odr.ReadMySqlStringAsync("friend_name"),
                    ProfileLink = await odr.ReadMySqlStringAsync("friend_profile_link"),
                    Disabled = await odr.ReadMySqlBooleanAsync("friend_disabled"),
                },
                WeekDayIds = (await odr.ReadMySqlStringAsync("scheduleweekday_ids", string.Empty))
                    .Split(',')
                    .Where(wd => int.TryParse(wd, out var parsedId))
                    .Select(wd => Convert.ToInt32(wd))
                    .ToList(),
            };
        }

        private async Task<ScheduleJobDef> ReadOpenScheduleJobDefAsync(MySqlDataReader odr)
        {
            return new ScheduleJobDef
            {
                Id = await odr.ReadMySqlIntegerAsync("id"),
                AppUserId = await odr.ReadMySqlIntegerAsync("appuser_id"),
                Name = await odr.ReadMySqlStringAsync("name"),
                FriendId = await odr.ReadMySqlIntegerAsync("friend_id"),
                FacebookCredentialId = await odr.ReadMySqlIntegerAsync("facebookcredential_id"),
                Type = await odr.ReadMySqlEnumAsync<ScheduleJobType>("type"),
                IntervalType = await odr.ReadMySqlEnumAsync<IntervalTypeEnum>("interval_type"),
                TimeFrom = await odr.ReadMySqlStringAsync("time_from"),
                TimeTo = await odr.ReadMySqlStringAsync("time_to"),
                TimeZone = await odr.ReadMySqlStringAsync("timezone_id"),
                Active = await odr.ReadMySqlBooleanAsync("active"),
            };
        }

        private async Task UpdateScheduleDefJobWeekDays(ScheduleJobDef def)
        {
            await DeleteScheduleDefJobWeekDays(def.Id);

            if (def.WeekDayIds.Count == 0 || def.IntervalType != IntervalTypeEnum.DAILY)
                return;

            var cmdStr = new StringBuilder(@"INSERT INTO jobweekday(schedulejobdef_id, scheduleweekday_id) values ");
            for (var i = 0; i < def.WeekDayIds.Count; i++)
            {
                cmdStr.Append($"(@schedulejobdef_id_{i}, @scheduleweekday_id_{i})");
                if ((i + 1) < def.WeekDayIds.Count)
                    cmdStr.Append(", ");
            }

            await using var cmd = await db.CreateCommand(cmdStr);

            for (var i = 0; i < def.WeekDayIds.Count; i++)
            {
                cmd.Parameters.Add(new MySqlParameter($"schedulejobdef_id_{i}", def.Id));
                cmd.Parameters.Add(new MySqlParameter($"scheduleweekday_id_{i}", def.WeekDayIds[i]));
            }

            await cmd.ExecuteNonQueryAsync();
        }

        private async Task DeleteScheduleDefJobWeekDays(int scheduleJobDefId)
        {
            var cmdStr = @"DELETE FROM jobweekday where schedulejobdef_id=@schedulejobdef_id";
            await using var cmd = await db.CreateCommand(cmdStr);
            cmd.Parameters.Add(new MySqlParameter("schedulejobdef_id", scheduleJobDefId));
            await cmd.ExecuteNonQueryAsync();
        }
    }
}