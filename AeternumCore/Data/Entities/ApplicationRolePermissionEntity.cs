using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AeternumCore.Data.Entities
{
    public class ApplicationRolePermissionEntity
    {
        [Required(ErrorMessage = "RoleId je povinné.")]
        public string RoleId { get; set; }

        /// <summary>
        /// Např. "CanEditArticle", "CanDeleteUser".
        /// </summary>
        [Required(ErrorMessage = "Oprávnění je povinné.")]
        [MaxLength(100, ErrorMessage = "Oprávnění nesmí překročit 100 znaků.")]
        public string Permission { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Navigační vlastnost k ApplicationRoleEntity.
        /// </summary>
        public virtual ApplicationRoleEntity Role { get; set; }

        /// <summary>
        /// Aktualizuje oprávnění.
        /// </summary>
        public void UpdatePermission(string permission)
        {
            AssignPermission(permission); // Tímto zjednodušíme kontrolu a přiřazení
        }

        /// <summary>
        /// Kontroluje, zda je dané oprávnění platné.
        /// </summary>
        public static bool IsPermissionValid(string permission)
        {
            // Můžete mít seznam platných oprávnění
            var validPermissions = new[] { "CanEditArticle", "CanDeleteUser", "CanViewReports" };
            return Array.Exists(validPermissions, p => p.Equals(permission, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Přiřadí nové oprávnění.
        /// </summary>
        public void AssignPermission(string permission)
        {
            if (!IsPermissionValid(permission))
            {
                throw new ArgumentException("Neplatné oprávnění.");
            }

            Permission = permission;
            AssignedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Zruší oprávnění.
        /// </summary>
        public void RevokePermission()
        {
            Permission = null; // nebo můžete nastavit na nějaké výchozí oprávnění
            AssignedAt = DateTime.UtcNow;
        }
    }
}
