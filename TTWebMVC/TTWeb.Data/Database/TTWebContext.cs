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

        }

        public DbSet<LoginUser> LoginUsers { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<LoginUserPermissionMapping> LoginUserPermissionMappings { get; set; }

        private const int _maxLengthShortString = 8;
        private const int _maxLengthMediumString = 64;
        private const int _maxLengthLongtring = 256;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region LoginUser

            modelBuilder.Entity<LoginUser>()
               .HasKey(m => m.Id);

            modelBuilder.Entity<LoginUser>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<LoginUser>().Property(e => e.Email).HasMaxLength(_maxLengthMediumString);
            modelBuilder.Entity<LoginUser>().Property(e => e.FirstName).HasMaxLength(_maxLengthMediumString);
            modelBuilder.Entity<LoginUser>().Property(e => e.LastName).HasMaxLength(_maxLengthMediumString);

            #endregion

            #region UserPermission

            modelBuilder.Entity<UserPermission>()
               .HasKey(m => m.Id);

            modelBuilder.Entity<UserPermission>()
                .Property(e => e.Value)
                .HasConversion(
                    v => v.ToString(),
                    v => (UserPermissionEnum)Enum.Parse(typeof(UserPermissionEnum), v));

            modelBuilder.Entity<UserPermission>().Property(e => e.Description).HasMaxLength(_maxLengthLongtring);

            #endregion

            #region LoginUserPermissionMapping

            modelBuilder.Entity<LoginUserPermissionMapping>()
                .HasKey(m => new { m.LoginUserId, m.UserPermissionId });

            modelBuilder.Entity<LoginUserPermissionMapping>()
               .HasOne(m => m.LoginUser)
               .WithMany(u => u.LoginUserPermissionMappings)
               .HasForeignKey(m => m.LoginUserId);

            modelBuilder.Entity<LoginUserPermissionMapping>()
               .HasOne(m => m.UserPermission)
               .WithMany(p => p.LoginUserPermissionMappings)
               .HasForeignKey(m => m.UserPermissionId);

            #endregion


        }
    }
}
