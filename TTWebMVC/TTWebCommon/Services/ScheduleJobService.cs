using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTWebCommon.Models;

namespace TTWebApi.Services
{
   public interface IScheduleJobService
   {
      void AddScheduleJobDef(ScheduleJobDef scheduleJobDef);
      IQueryable<ScheduleJobDef> GetScheduleJobDefs();
      IQueryable<ScheduleJobDef> GetScheduleJobDef(int id);
      Task RemoveScheduleJobDef(int id, int appUserId);
      Task<bool> HasAccessToScheduleJobDef(int id, int appUserId);
      Task SaveChangesAsync();
   }

   public class ScheduleJobService : IScheduleJobService
   {
      private readonly TTWebDbContext db;

      public ScheduleJobService(TTWebDbContext db)
      {
         this.db = db;
      }

      public void AddScheduleJobDef(ScheduleJobDef scheduleJobDef)
      {
         db.ScheduleJobDefSet.Add(scheduleJobDef);
      }

      public IQueryable<ScheduleJobDef> GetScheduleJobDef(int id)
      {
         return GetScheduleJobDefs()
            .Where(d => d.Id == id);
      }

      public IQueryable<ScheduleJobDef> GetScheduleJobDefs()
      {
         return db.ScheduleJobDefSet
            .Where(d => d.Active)
            .Include(d => d.AppUser)
            .Include(d => d.ScheduleJobPartners)
               .ThenInclude(p => p.Partner)
              .Include(d => d.JobWeekDays)
                  .ThenInclude(w => w.WeekDay);
      }

      public Task<bool> HasAccessToScheduleJobDef(int id, int appUserId)
      {
         return db.ScheduleJobDefSet.AnyAsync(d => d.Id == id && d.AppUserId == appUserId);
      }

      public async Task RemoveScheduleJobDef(int id, int appUserId)
      {
         if (await HasAccessToScheduleJobDef(id, appUserId))
         {
            ScheduleJobDef def = new ScheduleJobDef { Id = id };
            db.ScheduleJobDefSet.Attach(def);
            db.Remove(def);
         }
      }

      public Task SaveChangesAsync()
      {
         return db.SaveChangesAsync();
      }
   }
}
