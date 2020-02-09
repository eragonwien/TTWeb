using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTWebInterface.Models
{
   public class TTWebDbContext : DbContext
   {
      public TTWebDbContext(DbContextOptions options) : base(options)
      {
      }

      public DbSet<AppUser> AppUserSet { get; set; }
      public DbSet<ScheduleJob> ScheduleJobSet { get; set; }
      public DbSet<ScheduleJobType> ScheduleJobTypeSet { get; set; }
      public DbSet<ScheduleJobParameterType> ScheduleJobParameterTypeSet { get; set; }

   }
}
