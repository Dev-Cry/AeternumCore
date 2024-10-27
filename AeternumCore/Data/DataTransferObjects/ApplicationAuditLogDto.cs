namespace AeternumCore.Data.DataTransferObjects
{
    public class ApplicationAuditLogDto
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public string EntityName { get; set; }
        public string EntityId { get; set; }
        public DateTime Timestamp { get; set; }
        public string? ChangedBy { get; set; }
    }
}
