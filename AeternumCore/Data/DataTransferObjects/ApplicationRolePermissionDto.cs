namespace AeternumCore.Data.DataTransferObjects
{
    public class ApplicationRolePermissionDto
    {
        public string RoleId { get; set; }
        public string Permission { get; set; }
        public DateTime AssignedAt { get; set; }
    }

}
