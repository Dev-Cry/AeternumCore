using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace AeternumCore.Data.Entities
{
    /// <summary>
    /// Reprezentuje roli uživatele aplikace s rozšířenými vlastnostmi.
    /// </summary>
    public class ApplicationRoleEntity : IdentityRole
    {
        /// <summary>
        /// Popis role.
        /// </summary>
        [MaxLength(200, ErrorMessage = "Popis nesmí překročit 200 znaků.")]
        public string? Description { get; set; }

        /// <summary>
        /// Datum vytvoření role.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Datum poslední aktualizace role.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

        public ICollection<ApplicationUserRoleEntity> UserRoles { get; set; } = new List<ApplicationUserRoleEntity>();

        /// <summary>
        /// Seznam claimů přidružených k této roli.
        /// </summary>
        public virtual ICollection<ApplicationRoleClaimEntity> RoleClaims { get; set; } = new List<ApplicationRoleClaimEntity>();

        /// <summary>
        /// Seznam oprávnění přidružených k této roli.
        /// </summary>
        public virtual ICollection<ApplicationRolePermissionEntity> RolePermissions { get; set; } = new List<ApplicationRolePermissionEntity>();

        /// <summary>
        /// Aktualizuje popis role.
        /// </summary>
        public void UpdateDescription(string description)
        {
            Description = description;
            UpdateLastModified();
        }

        private void UpdateLastModified()
        {
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Přidá nový claim do role.
        /// </summary>
        public void AddClaim(ApplicationRoleClaimEntity claim)
        {
            if (claim != null && !RoleClaims.Contains(claim))
            {
                RoleClaims.Add(claim);
                UpdateLastModified();
            }
        }

        /// <summary>
        /// Odebere existující claim z role.
        /// </summary>
        public void RemoveClaim(ApplicationRoleClaimEntity claim)
        {
            if (claim != null && RoleClaims.Contains(claim))
            {
                RoleClaims.Remove(claim);
                UpdateLastModified();
            }
        }

        /// <summary>
        /// Přidá nové oprávnění do role.
        /// </summary>
        public void AddPermission(ApplicationRolePermissionEntity permission)
        {
            if (permission != null && !RolePermissions.Contains(permission))
            {
                RolePermissions.Add(permission);
                UpdateLastModified();
            }
        }

        /// <summary>
        /// Odebere existující oprávnění z role.
        /// </summary>
        public void RemovePermission(ApplicationRolePermissionEntity permission)
        {
            if (permission != null && RolePermissions.Contains(permission))
            {
                RolePermissions.Remove(permission);
                UpdateLastModified();
            }
        }

        /// <summary>
        /// Vrátí seznam všech claimů přiřazených k roli.
        /// </summary>
        public IEnumerable<ApplicationRoleClaimEntity> GetClaims() => RoleClaims;

        /// <summary>
        /// Vrátí seznam všech oprávnění přiřazených k roli.
        /// </summary>
        public IEnumerable<ApplicationRolePermissionEntity> GetPermissions() => RolePermissions;
    }
}
