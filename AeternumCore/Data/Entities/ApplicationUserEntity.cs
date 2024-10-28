using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace AeternumCore.Data.Entities
{
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

        // Konstruktor
        public ApplicationUserEntity(string firstName, string lastName, DateTime dateOfBirth)
        {
            Profile = new ApplicationUserProfileEntity
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth
            };
        }

        public void UpdateUserInfo(string firstName, string lastName, bool isActive)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("First name and last name cannot be empty.");
            }

            Profile.FirstName = firstName;
            Profile.LastName = lastName;
            IsActive = isActive;
            UpdateLastModified(); // Volání samostatné metody
        }

        public void BlockUser()
        {
            IsBlocked = true;
            BlockedAt = DateTime.UtcNow;
            UpdateLastModified(); // Volání samostatné metody
        }

        public void UnblockUser()
        {
            IsBlocked = false;
            BlockedAt = null;
            UpdateLastModified(); // Volání samostatné metody
        }

        public void AddRole(ApplicationUserRoleEntity userRole)
        {
            if (!UserRoles.Contains(userRole))
            {
                UserRoles.Add(userRole);
                UpdateLastModified(); // Volání samostatné metody
            }
        }

        public void RemoveRole(ApplicationUserRoleEntity userRole)
        {
            if (UserRoles.Contains(userRole))
            {
                UserRoles.Remove(userRole);
                UpdateLastModified(); // Volání samostatné metody
            }
        }

        private void UpdateLastModified()
        {
            UpdatedAt = DateTime.UtcNow; // Automatická aktualizace
        }
    }
}
