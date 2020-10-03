using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TTWeb.Data.Models;

namespace TTWeb.Data.Database
{
    public class TTWebContext : DbContext
    {
        public TTWebContext(DbContextOptions<TTWebContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<LoginUser> LoginUsers { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<LoginUserPermissionMapping> LoginUserPermissionMappings { get; set; }
    }
}
