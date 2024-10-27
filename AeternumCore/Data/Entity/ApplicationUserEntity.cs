﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace AeternumCore.Data.Entity
{
    /// <summary>
    /// Reprezentuje uživatele aplikace s rozšířenými vlastnostmi.
    /// </summary>
    public class ApplicationUserEntity : IdentityUser
    {
        public bool IsActive { get; set; } = true;
        public bool IsBlocked { get; set; } = false;
        public DateTime? BlockedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginDate { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; } = new List<IdentityUserClaim<string>>();
        public virtual ApplicationUserProfileEntity Profile { get; set; }
        public virtual ICollection<ApplicationUserRoleEntity> UserRoles { get; set; } = new List<ApplicationUserRoleEntity>();

        /// <summary>
        /// Aktualizuje informace o uživateli.
        /// </summary>
        public void UpdateUserInfo(string firstName, string lastName, bool isActive)
        {
            Profile.FirstName = firstName;
            Profile.LastName = lastName;
            IsActive = isActive;
            UpdatedAt = DateTime.UtcNow; // Automatická aktualizace
        }

        /// <summary>
        /// Blokuje uživatele.
        /// </summary>
        public void BlockUser()
        {
            IsBlocked = true;
            BlockedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow; // Automatická aktualizace
        }

        /// <summary>
        /// Odblokovává uživatele.
        /// </summary>
        public void UnblockUser()
        {
            IsBlocked = false;
            BlockedAt = null;
            UpdatedAt = DateTime.UtcNow; // Automatická aktualizace
        }
    }
}