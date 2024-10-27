using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AeternumCore.Data.Entity
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
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Datum poslední aktualizace role.
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

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
            RoleClaims.Add(claim);
            UpdateLastModified();
        }

        /// <summary>
        /// Odebere existující claim z role.
        /// </summary>
        public void RemoveClaim(ApplicationRoleClaimEntity claim)
        {
            if (RoleClaims.Contains(claim))
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
            RolePermissions.Add(permission);
            UpdateLastModified();
        }

        /// <summary>
        /// Odebere existující oprávnění z role.
        /// </summary>
        public void RemovePermission(ApplicationRolePermissionEntity permission)
        {
            if (RolePermissions.Contains(permission))
            {
                RolePermissions.Remove(permission);
                UpdateLastModified();
            }
        }

        /// <summary>
        /// Vrátí seznam všech claimů přiřazených k roli.
        /// </summary>
        public IEnumerable<ApplicationRoleClaimEntity> GetClaims()
        {
            return RoleClaims.ToList();
        }

        /// <summary>
        /// Vrátí seznam všech oprávnění přiřazených k roli.
        /// </summary>
        public IEnumerable<ApplicationRolePermissionEntity> GetPermissions()
        {
            return RolePermissions.ToList();
        }
    }
}
