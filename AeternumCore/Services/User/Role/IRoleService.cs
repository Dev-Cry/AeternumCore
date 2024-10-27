using System.Collections.Generic;
using System.Threading.Tasks;
using AeternumCore.Data.DataTransferObjects;

namespace AeternumCore.Services.User.Role
{
    public interface IRoleService
    {
        /// <summary>
        /// Získá roli podle jejího ID.
        /// </summary>
        Task<ApplicationRoleDto> GetRoleByIdAsync(string roleId);

        /// <summary>
        /// Získá všechny role.
        /// </summary>
        Task<IEnumerable<ApplicationRoleDto>> GetAllRolesAsync();

        /// <summary>
        /// Vytvoří novou roli.
        /// </summary>
        Task<ApplicationRoleDto> CreateRoleAsync(ApplicationRoleDto roleDto);

        /// <summary>
        /// Aktualizuje existující roli.
        /// </summary>
        Task<ApplicationRoleDto> UpdateRoleAsync(ApplicationRoleDto roleDto);

        /// <summary>
        /// Smaže roli podle jejího ID.
        /// </summary>
        Task DeleteRoleAsync(string roleId);

        /// <summary>
        /// Přidá oprávnění do role.
        /// </summary>
        Task AddPermissionToRoleAsync(string roleId, string permission);

        /// <summary>
        /// Odebere oprávnění z role.
        /// </summary>
        Task RemovePermissionFromRoleAsync(string roleId, string permission);

        /// <summary>
        /// Přidá claim do role.
        /// </summary>
        Task AddClaimToRoleAsync(string roleId, ApplicationRoleClaimDto claimDto);

        /// <summary>
        /// Odebere claim z role.
        /// </summary>
        Task RemoveClaimFromRoleAsync(string roleId, string claimId);

        /// <summary>
        /// Získá všechny oprávnění přiřazené k roli.
        /// </summary>
        Task<IEnumerable<ApplicationRolePermissionDto>> GetPermissionsByRoleIdAsync(string roleId);

        /// <summary>
        /// Získá všechny claimy přiřazené k roli.
        /// </summary>
        Task<IEnumerable<ApplicationRoleClaimDto>> GetClaimsByRoleIdAsync(string roleId);
    }
}
