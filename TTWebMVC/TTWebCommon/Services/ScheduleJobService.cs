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
      Task<IEnumerable<ScheduleJobDef>> GetScheduleJobDefs(int userId);
      Task<ScheduleJobDef> GetScheduleJobDef(int id);
      Task RemoveScheduleJobDef(int id, int appUserId);
      Task<bool> HasAccessToScheduleJobDef(int id, int appUserId);
   }

   public class ScheduleJobService : IScheduleJobService
   {
      private readonly TTWebDbContext db;

      public ScheduleJobService(TTWebDbContext db)
      {
         this.db = db;
      }

      public async Task AddScheduleJobDef(ScheduleJobDef def)
      {
         string cmdStr = @"INSERT INTO schedulejobdef(appuser_id, name, type, interval_type, time_from, time_to, timezone_id, active) 
            VALUES(@appuser_id, @name, @type, @interval_type, @time_from, @time_to, @timezone_id, @active)";
         using MySqlCommand cmd = db.CreateCommand(cmdStr);
         cmd.Parameters.Add(new MySqlParameter("appuser_id", def.AppUserId));
         cmd.Parameters.Add(new MySqlParameter("name", def.Name));
         cmd.Parameters.Add(new MySqlParameter("type", def.Type.ToString()));
         cmd.Parameters.Add(new MySqlParameter("interval_type", def.IntervalType.ToString()));
         cmd.Parameters.Add(new MySqlParameter("time_from", def.TimeFrom));
         cmd.Parameters.Add(new MySqlParameter("time_to", def.TimeTo));
         cmd.Parameters.Add(new MySqlParameter("timezone_id", def.TimeZone));
         cmd.Parameters.Add(new MySqlParameter("active", def.Active ? 1 : 0));
         await cmd.ExecuteNonQueryAsync();
      }

      public Task<ScheduleJobDef> GetScheduleJobDef(int id)
      {
         throw new NotImplementedException();
      }

      public async Task<IEnumerable<ScheduleJobDef>> GetScheduleJobDefs(int userId)
      {
         List<ScheduleJobDef> defs = new List<ScheduleJobDef>();
         string cmdStr = @"SELECT id, appuser_id, name, type, interval_type, time_from, time_to, timezone_id, active 
            FROM schedulejobdef WHERE appuser_id=@appuser_id";
         using MySqlCommand cmd = db.CreateCommand(cmdStr);
         cmd.Parameters.Add(new MySqlParameter("appuser_id", userId));
         using var odr = await cmd.ExecuteMySqlReaderAsync();
         while (await odr.ReadAsync())
         {
            defs.Add(new ScheduleJobDef
            {
               Id = await odr.ReadMySqlIntegerAsync("id"),
               AppUserId = userId,
               Name = await odr.ReadMySqlStringAsync("name"),
               Type = await odr.ReadMySqlEnumAsync<ScheduleJobType>("type"),
               IntervalType = await odr.ReadMySqlEnumAsync<IntervalTypeEnum>("interval_type"),
               TimeFrom = await odr.ReadMySqlStringAsync("time_from"),
               TimeTo = await odr.ReadMySqlStringAsync("time_to"),
               TimeZone = await odr.ReadMySqlStringAsync("timezone_id"),
               Active = await odr.ReadMySqlBooleanAsync("active"),
            });
         }
         return defs;
      }

      public Task<bool> HasAccessToScheduleJobDef(int id, int appUserId)
      {
         throw new NotImplementedException();
      }

      public Task RemoveScheduleJobDef(int id, int appUserId)
      {
         throw new NotImplementedException();
      }
   }
}
