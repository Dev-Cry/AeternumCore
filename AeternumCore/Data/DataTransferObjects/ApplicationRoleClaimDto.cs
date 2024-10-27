namespace AeternumCore.Data.DataTransferObjects
{
    public class ApplicationRoleClaimDto
    {
        public string Id { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
