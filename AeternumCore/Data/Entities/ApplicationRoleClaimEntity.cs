using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AeternumCore.Data.Entities
{
    public class ApplicationRoleClaimEntity : IdentityRoleClaim<string>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        /// <summary>
        /// Navigační vlastnost k ApplicationRoleEntity.
        /// </summary>
        public virtual ApplicationRoleEntity Role { get; set; }

        /// <summary>
        /// Aktualizuje claim.
        /// </summary>
        /// <param name="newClaimType">Nový typ claimu.</param>
        /// <param name="newClaimValue">Nová hodnota claimu.</param>
        public void UpdateClaim(string newClaimType, string newClaimValue)
        {
            if (!string.IsNullOrEmpty(newClaimType) && !string.IsNullOrEmpty(newClaimValue))
            {
                ClaimType = newClaimType;
                ClaimValue = newClaimValue;
                CreatedAt = DateTime.UtcNow; // Aktualizuje čas vytvoření
            }
        }

        /// <summary>
        /// Vrátí podrobnosti o claimu.
        /// </summary>
        public string GetClaimDetails()
        {
            return $"Claim Type: {ClaimType}, Claim Value: {ClaimValue}, Created At: {FormatCreatedAt()}";
        }

        /// <summary>
        /// Zkontroluje, zda je claim platný.
        /// </summary>
        public bool IsClaimValid()
        {
            return !string.IsNullOrEmpty(ClaimType) && !string.IsNullOrEmpty(ClaimValue);
        }

        /// <summary>
        /// Naformátuje datum vytvoření do čitelného formátu.
        /// </summary>
        private string FormatCreatedAt(string format = "yyyy-MM-dd HH:mm:ss")
        {
            return CreatedAt.ToString(format);
        }
    }
}
