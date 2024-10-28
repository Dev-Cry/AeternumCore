using AeternumCore.Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AeternumCore.Data.Entities
{
    public class ApplicationAuditLogEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Action { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string EntityName { get; set; }
        public string EntityId { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public string? ChangedBy { get; set; }

        public string GetAuditLogDetails()
        {
            return $"Action: {Action}, Entity: {EntityName}, Entity ID: {EntityId}, Timestamp: {FormatTimestamp()}, Changed By: {ChangedBy}";
        }

        public bool IsSensitiveChange()
        {
            return Action == nameof(AuditAction.UpdateUser) || Action == nameof(AuditAction.ChangePassword);
        }

        public string FormatTimestamp()
        {
            return Timestamp.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public void UpdateTimestamp() => Timestamp = DateTime.UtcNow;
    }
}
