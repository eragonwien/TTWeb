using MySql.Data.MySqlClient;
using SNGCommon.Sql.MySql.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTWebCommon.Models;

namespace TTWebCommon.Services
{
   public interface IScheduleJobService
   {
      Task AddScheduleJobDef(ScheduleJobDef scheduleJobDef);
      Task<List<ScheduleJobDef>> GetScheduleJobDefs(int userId);
      Task<ScheduleJobDef> GetScheduleJobDef(int id, int userId);
      Task RemoveScheduleJobDef(int id, int appUserId);
      Task ToggleActive(int id, bool active);
      Task UpdateScheduleJobDef(ScheduleJobDef scheduleJobDef);
   }

   public class ScheduleJobService : IScheduleJobService
   {
      private readonly TTWebDbContext db;

      public ScheduleJobService(TTWebDbContext db)
      {
         this.db = db;
      }

      private const string ScheduleJobDefSelect = @"SELECT id, appuser_id, friend_id, facebookcredential_id, name, type, interval_type, time_from, time_to, timezone_id, active FROM schedulejobdef";

      public async Task AddScheduleJobDef(ScheduleJobDef def)
      {
         string cmdStr = @"INSERT INTO schedulejobdef(appuser_id, friend_id, facebookcredential_id, name, type, interval_type, time_from, time_to, timezone_id, active) 
            VALUES(@appuser_id, @friend_id, @facebookcredential_id, @name, @type, @interval_type, @time_from, @time_to, @timezone_id, @active)";
         using MySqlCommand cmd = db.CreateCommand(cmdStr);
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
      }

      public async Task<ScheduleJobDef> GetScheduleJobDef(int id, int userId)
      {
         ScheduleJobDef scheduleJobDef = new ScheduleJobDef();
         string cmdStr = ScheduleJobDefSelect + " WHERE id=@id AND appuser_id=@appuser_id";
         using MySqlCommand cmd = db.CreateCommand(cmdStr);
         cmd.Parameters.Add(new MySqlParameter("id", id));
         cmd.Parameters.Add(new MySqlParameter("appuser_id", userId));
         using var odr = await cmd.ExecuteMySqlReaderAsync();
         if (await odr.ReadAsync())
         {
            scheduleJobDef = await ReadScheduleJobDefAsync(odr);
         }
         return scheduleJobDef;
      }

      public async Task<List<ScheduleJobDef>> GetScheduleJobDefs(int userId)
      {
         List<ScheduleJobDef> defs = new List<ScheduleJobDef>();
         string cmdStr = ScheduleJobDefSelect + " WHERE appuser_id=@appuser_id";
         using MySqlCommand cmd = db.CreateCommand(cmdStr);
         cmd.Parameters.Add(new MySqlParameter("appuser_id", userId));
         using var odr = await cmd.ExecuteMySqlReaderAsync();
         while (await odr.ReadAsync())
         {
            var scheduleJobDef = await ReadScheduleJobDefAsync(odr);
            defs.Add(scheduleJobDef);
         }
         return defs.ToList();
      }

      public Task RemoveScheduleJobDef(int id, int appUserId)
      {
         throw new NotImplementedException();
      }

      public async Task ToggleActive(int id, bool active)
      {
         string cmdStr = @"UPDATE schedulejobdef SET active=@active WHERE id=@id";
         using MySqlCommand cmd = db.CreateCommand(cmdStr);
         cmd.Parameters.Add(new MySqlParameter("active", active));
         cmd.Parameters.Add(new MySqlParameter("id", id));
         await cmd.ExecuteNonQueryAsync();
      }

      public async Task UpdateScheduleJobDef(ScheduleJobDef scheduleJobDef)
      {
         string cmdStr = @"UPDATE schedulejobdef SET friend_id=@friend_id, facebookcredential_id=@facebookcredential_id, 
            name=@name, type=@type, interval_type=@interval_type, time_from=@time_from, time_to=@time_to, 
            timezone_id=@timezone_id WHERE id=@id and appuser_id=@appuser_id";
         using MySqlCommand cmd = db.CreateCommand(cmdStr);
         cmd.Parameters.Add(new MySqlParameter("friend_id", scheduleJobDef.FriendId));
         cmd.Parameters.Add(new MySqlParameter("facebookcredential_id", scheduleJobDef.FacebookCredentialId));
         cmd.Parameters.Add(new MySqlParameter("name", scheduleJobDef.Name));
         cmd.Parameters.Add(new MySqlParameter("type", scheduleJobDef.Type));
         cmd.Parameters.Add(new MySqlParameter("interval_type", scheduleJobDef.IntervalType));
         cmd.Parameters.Add(new MySqlParameter("time_from", scheduleJobDef.TimeFrom));
         cmd.Parameters.Add(new MySqlParameter("time_to", scheduleJobDef.TimeTo));
         cmd.Parameters.Add(new MySqlParameter("timezone_id", scheduleJobDef.TimeZone));
         cmd.Parameters.Add(new MySqlParameter("id", scheduleJobDef.Id));
         cmd.Parameters.Add(new MySqlParameter("appuser_id", scheduleJobDef.AppUserId));
         await cmd.ExecuteNonQueryAsync();
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
         };
      }
   }
}
