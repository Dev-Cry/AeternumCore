using AeternumCore.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AeternumCore.Data
{
    // ApplicationDbContext rozšiřuje IdentityDbContext pro správu uživatelské identity a role
    public class ApplicationDbContext : IdentityDbContext<ApplicationUserEntity, ApplicationRoleEntity, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // DbSet pro správu profilů uživatelů
        public DbSet<ApplicationUserProfileEntity> UserProfiles { get; set; }

        // DbSet pro many-to-many vztah mezi uživateli a rolemi
        public DbSet<ApplicationUserRoleEntity> UserRoles { get; set; }

        // DbSet pro přiřazení oprávnění rolím
        public DbSet<ApplicationRolePermissionEntity> RolePermissions { get; set; }

        // DbSet pro přiřazení nároků (claims) k rolím
        public DbSet<ApplicationRoleClaimEntity> RoleClaims { get; set; }

        // DbSet pro logování auditovaných změn
        public DbSet<ApplicationAuditLogEntity> AuditLogs { get; set; }

        // Konfigurace entit a vztahů pomocí OnModelCreating
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Základní konfigurace pro identity framework (role, uživatelé)
            base.OnModelCreating(builder);

            // Nastavení vztahu mezi uživatelem a profilem - One-to-One
            builder.Entity<ApplicationUserProfileEntity>()
                .HasOne(up => up.User)
                .WithOne(u => u.Profile)
                .HasForeignKey<ApplicationUserProfileEntity>(up => up.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Pokud se smaže uživatel, smaže se i jeho profil

            // Konfigurace vztahu many-to-many mezi uživateli a rolemi
            builder.Entity<ApplicationUserRoleEntity>()
                .HasKey(ur => new { ur.UserId, ur.RoleId }); // Composite key pro vztah User-Role

            // Nastavení vazby mezi uživateli a jejich rolemi
            builder.Entity<ApplicationUserRoleEntity>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Smazání role u uživatele při odstranění uživatele

            // Nastavení vazby mezi rolemi a uživateli
            builder.Entity<ApplicationUserRoleEntity>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade); // Smazání role u uživatele při odstranění role

            // Vztah mezi rolí a oprávněními - One-to-Many
            builder.Entity<ApplicationRoleEntity>()
                .HasMany(r => r.RolePermissions)
                .WithOne(rp => rp.Role)
                .HasForeignKey(rp => rp.RoleId)
                .OnDelete(DeleteBehavior.Cascade); // Smazání všech oprávnění role při odstranění role

            // Vztah mezi rolí a nároky (claims) - One-to-Many
            builder.Entity<ApplicationRoleEntity>()
                .HasMany(r => r.RoleClaims)
                .WithOne(rc => rc.Role)
                .HasForeignKey(rc => rc.RoleId)
                .OnDelete(DeleteBehavior.Cascade); // Smazání nároků role při odstranění role

            // Optimalizace dotazů pomocí indexů na často používané sloupce
            builder.Entity<ApplicationUserRoleEntity>()
                .HasIndex(ur => ur.UserId); // Index na UserId pro rychlejší vyhledávání

            builder.Entity<ApplicationUserRoleEntity>()
                .HasIndex(ur => ur.RoleId); // Index na RoleId pro rychlejší vyhledávání

            builder.Entity<ApplicationRolePermissionEntity>()
                .HasIndex(rp => rp.RoleId); // Index na RoleId v oprávněních

            builder.Entity<ApplicationRoleClaimEntity>()
                .HasIndex(rc => rc.RoleId); // Index na RoleId v nárocích

            // Audit log - přidání validace a omezení délky pro některá pole
            builder.Entity<ApplicationAuditLogEntity>()
                .Property(a => a.Action)
                .IsRequired() // Pole Action je povinné
                .HasMaxLength(100); // Omezení délky na 100 znaků

            builder.Entity<ApplicationAuditLogEntity>()
                .Property(a => a.Description)
                .HasMaxLength(1000); // Omezení délky popisu na 1000 znaků

            // Časová razítka pro auditované záznamy
            builder.Entity<ApplicationAuditLogEntity>()
                .Property(a => a.CreatedAt)
                .IsRequired(); // Povinné pole CreatedAt pro zaznamenání data vytvoření

            builder.Entity<ApplicationAuditLogEntity>()
                .Property(a => a.UpdatedAt)
                .IsRequired(false); // Nepovinné pole UpdatedAt pro datum poslední úpravy

            // Validace a omezení délky pole LastName v uživatelském profilu
            builder.Entity<ApplicationUserProfileEntity>()
                .Property(up => up.LastName)
                .IsRequired() // Povinné pole pro příjmení
                .HasMaxLength(50); // Omezení délky příjmení na 50 znaků
        }
    }
}
