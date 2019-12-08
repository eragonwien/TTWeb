using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using TTWebMVC.Models;
using SNGCommon.Common;

namespace TTWebMVC.Services
{
   public interface IJobService
   {
      Task<List<string>> GetAllTypes();
      Task<List<string>> GetAllParameterTypes();
      Task Add(ScheduleJob job);
   }

   public class JobService : IJobService
   {
      private readonly DatabaseContext db;

      public JobService(DatabaseContext db)
      {
         this.db = db;
      }

      public async Task Add(ScheduleJob job)
      {
         throw new NotImplementedException();
      }

      public async Task<List<string>> GetAllParameterTypes()
      {
         List<string> types = new List<string>();
         using (var con = db.GetConnection())
         {
            string cmdStr = @"SELECT name from SCHEDULEJOBPARAMETERTYPE";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               await con.OpenAsync();
               using (var odr = await cmd.ExecuteReaderAsync())
               {
                  while (await odr.ReadAsync())
                  {
                     types.Add(await odr.ReadStringAsync("name"));
                  }
               }
            }
         }
         return types.Distinct().ToList();
      }

      public async Task<List<string>> GetAllTypes()
      {
         List<string> types = new List<string>(); 
         using (var con = db.GetConnection())
         {
            string cmdStr = @"SELECT name from SCHEDULEJOBTYPE";
            using (var cmd = new MySqlCommand(cmdStr, con))
            {
               await con.OpenAsync();
               using (var odr = await cmd.ExecuteReaderAsync())
               {
                  while (await odr.ReadAsync())
                  {
                     types.Add(await odr.ReadStringAsync("name"));
                  }
               }
            }
         }
         return types.Distinct().ToList();
      }
   }
}
