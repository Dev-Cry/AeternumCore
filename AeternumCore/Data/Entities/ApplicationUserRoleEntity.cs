using Microsoft.AspNetCore.Identity;
using System;

namespace AeternumCore.Data.Entities
{
    public class ApplicationUserRoleEntity : IdentityUserRole<string>
    {
        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Navigační vlastnost pro uživatele.
        /// </summary>
        public virtual ApplicationUserEntity User { get; set; }

        /// <summary>
        /// Navigační vlastnost pro roli.
        /// </summary>
        public virtual ApplicationRoleEntity Role { get; set; }

        /// <summary>
        /// Přiřadí roli uživateli a aktualizuje čas přiřazení.
        /// </summary>
        public void AssignRole(ApplicationRoleEntity role)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role), "Role nemůže být null.");
            }

            Role = role;
            AssignedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Odebere roli z uživatele.
        /// </summary>
        public void RemoveRole()
        {
            Role = null; // Můžete také zvážit, zda uchovat historii
        }

        /// <summary>
        /// Vrátí název přiřazené role.
        /// </summary>
        public string GetRoleName()
        {
            return Role?.Name; // Vrátí null, pokud je Role null
        }

        /// <summary>
        /// Zjistí, zda je uživateli přiřazena role.
        /// </summary>
        public bool IsRoleAssigned()
        {
            return Role != null;
        }

        /// <summary>
        /// Aktualizuje přiřazenou roli a aktualizuje čas přiřazení.
        /// </summary>
        public void UpdateRole(ApplicationRoleEntity role)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role), "Role nemůže být null.");
            }

            Role = role;
            AssignedAt = DateTime.UtcNow;
        }
    }
}
