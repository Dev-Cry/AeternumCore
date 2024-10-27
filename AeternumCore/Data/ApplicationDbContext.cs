using AeternumCore.Data.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AeternumCore.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUserEntity, ApplicationRoleEntity, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<ApplicationUserProfileEntity> UserProfiles { get; set; }
        public DbSet<ApplicationUserRoleEntity> UserRoles { get; set; }
        public DbSet<ApplicationRolePermissionEntity> RolePermissions { get; set; }
        public DbSet<ApplicationRoleClaimEntity> RoleClaims { get; set; }
        public DbSet<ApplicationAuditLogEntity> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Nastavení vztahů
            builder.Entity<ApplicationUserProfileEntity>()
                .HasOne(up => up.User)
                .WithOne(u => u.Profile)
                .HasForeignKey<ApplicationUserProfileEntity>(up => up.UserId);

            builder.Entity<ApplicationUserRoleEntity>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            builder.Entity<ApplicationRoleEntity>()
                .HasMany(r => r.RolePermissions)
                .WithOne(rp => rp.Role)
                .HasForeignKey(rp => rp.RoleId);

            builder.Entity<ApplicationRoleEntity>()
                .HasMany(r => r.RoleClaims)
                .WithOne(rc => rc.Role)
                .HasForeignKey(rc => rc.RoleId);

            // Přidání indexů
            builder.Entity<ApplicationUserRoleEntity>()
                .HasIndex(ur => ur.RoleId);

            builder.Entity<ApplicationRolePermissionEntity>()
                .HasIndex(rp => rp.RoleId);

            builder.Entity<ApplicationRoleClaimEntity>()
                .HasIndex(rc => rc.RoleId);
        }

    }
}