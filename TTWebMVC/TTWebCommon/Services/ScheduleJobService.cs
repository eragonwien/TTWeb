using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTWebCommon.Models;

namespace TTWebApi.Services
{
   public interface IScheduleJobService
   {
      Task AddScheduleJobDef(ScheduleJobDef scheduleJobDef);
      Task<IEnumerable<ScheduleJobDef>> GetScheduleJobDefs();
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

      public Task AddScheduleJobDef(ScheduleJobDef scheduleJobDef)
      {
         throw new NotImplementedException();
      }

      public Task<ScheduleJobDef> GetScheduleJobDef(int id)
      {
         throw new NotImplementedException();
      }

      public Task<IEnumerable<ScheduleJobDef>> GetScheduleJobDefs()
      {
         throw new NotImplementedException();
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
