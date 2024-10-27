using System;

namespace AeternumCore.Data.Entity
{
    public class ApplicationAuditLogEntity
    {
        public int Id { get; set; }
        public string Action { get; set; } // Např. "UpdateUser", "AddClaim"
        public string EntityName { get; set; } // Např. "User", "Role"
        public string EntityId { get; set; } // ID upravované entity
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string? ChangedBy { get; set; } // Kdo změnu provedl

        /// <summary>
        /// Vrátí podrobnosti o záznamu auditování.
        /// </summary>
        public string GetAuditLogDetails()
        {
            return $"Action: {Action}, Entity: {EntityName}, Entity ID: {EntityId}, Timestamp: {FormatTimestamp()}, Changed By: {ChangedBy}";
        }

        /// <summary>
        /// Zjistí, zda se změna týká citlivých informací.
        /// </summary>
        public bool IsSensitiveChange()
        {
            // Předpokládejme, že změny uživatelského jména a hesla jsou citlivé
            return Action == "UpdateUser" || Action == "ChangePassword";
        }

        /// <summary>
        /// Naformátuje časové razítko do čitelného formátu.
        /// </summary>
        public string FormatTimestamp()
        {
            return Timestamp.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
