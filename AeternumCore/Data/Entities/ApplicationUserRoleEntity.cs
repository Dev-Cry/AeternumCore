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
            Role = role;
            AssignedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Odebere roli z uživatele.
        /// </summary>
        public void RemoveRole()
        {
            Role = null; // Můžeš také zvážit, zda uchovat historii
        }

        /// <summary>
        /// Aktualizuje přiřazenou roli a aktualizuje čas přiřazení.
        /// </summary>
        public void UpdateRole(ApplicationRoleEntity role)
        {
            Role = role;
            AssignedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Vrátí název přiřazené role.
        /// </summary>
        public string GetRoleName()
        {
            return Role?.Name ?? "Žádná role"; // nebo můžeš vrátit null, pokud je Role null
        }

        /// <summary>
        /// Zjistí, zda je uživateli přiřazena role.
        /// </summary>
        public bool IsRoleAssigned()
        {
            return Role != null;
        }
    }
}
