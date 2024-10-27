using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AeternumCore.Data.DataTransferObjects;
using AeternumCore.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AeternumCore.Services.User.Role
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<ApplicationRoleEntity> _roleManager;
        private readonly IMapper _mapper;
        private readonly ILogger<RoleService> _logger;

        public RoleService(RoleManager<ApplicationRoleEntity> roleManager, IMapper mapper, ILogger<RoleService> logger)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApplicationRoleDto> GetRoleByIdAsync(string roleId)
        {
            _logger.LogInformation("Fetching role by ID: {RoleId}", roleId);
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                _logger.LogWarning("Role with ID {RoleId} not found.", roleId);
                throw new KeyNotFoundException($"Role with ID {roleId} not found.");
            }
            return _mapper.Map<ApplicationRoleDto>(role);
        }

        public async Task<IEnumerable<ApplicationRoleDto>> GetAllRolesAsync()
        {
            _logger.LogInformation("Fetching all roles");
            var roles = await _roleManager.Roles.ToListAsync();
            return _mapper.Map<IEnumerable<ApplicationRoleDto>>(roles);
        }

        public async Task<ApplicationRoleDto> CreateRoleAsync(ApplicationRoleDto roleDto)
        {
            _logger.LogInformation("Creating role with name: {RoleName}", roleDto.Name);
            var roleEntity = _mapper.Map<ApplicationRoleEntity>(roleDto);
            var result = await _roleManager.CreateAsync(roleEntity);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogError("Error creating role {RoleName}: {Errors}", roleDto.Name, errors);
                throw new Exception($"Role creation failed: {errors}");
            }
            return _mapper.Map<ApplicationRoleDto>(roleEntity);
        }

        public async Task<ApplicationRoleDto> UpdateRoleAsync(ApplicationRoleDto roleDto)
        {
            _logger.LogInformation("Updating role with ID: {RoleId}", roleDto.Id);
            var roleEntity = await _roleManager.FindByIdAsync(roleDto.Id);
            if (roleEntity == null)
            {
                _logger.LogWarning("Role with ID {RoleId} not found.", roleDto.Id);
                throw new KeyNotFoundException($"Role with ID {roleDto.Id} not found.");
            }

            _mapper.Map(roleDto, roleEntity);
            var result = await _roleManager.UpdateAsync(roleEntity);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogError("Error updating role {RoleId}: {Errors}", roleDto.Id, errors);
                throw new Exception($"Role update failed: {errors}");
            }
            return _mapper.Map<ApplicationRoleDto>(roleEntity);
        }

        public async Task DeleteRoleAsync(string roleId)
        {
            _logger.LogInformation("Deleting role with ID: {RoleId}", roleId);
            var roleEntity = await _roleManager.FindByIdAsync(roleId);
            if (roleEntity == null)
            {
                _logger.LogWarning("Role with ID {RoleId} not found.", roleId);
                throw new KeyNotFoundException($"Role with ID {roleId} not found.");
            }

            var result = await _roleManager.DeleteAsync(roleEntity);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogError("Error deleting role {RoleId}: {Errors}", roleId, errors);
                throw new Exception($"Role deletion failed: {errors}");
            }
        }

        public Task AddPermissionToRoleAsync(string roleId, string permission)
        {
            throw new NotImplementedException();
        }

        public Task RemovePermissionFromRoleAsync(string roleId, string permission)
        {
            throw new NotImplementedException();
        }

        public Task AddClaimToRoleAsync(string roleId, ApplicationRoleClaimDto claimDto)
        {
            throw new NotImplementedException();
        }

        public Task RemoveClaimFromRoleAsync(string roleId, string claimId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApplicationRolePermissionDto>> GetPermissionsByRoleIdAsync(string roleId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApplicationRoleClaimDto>> GetClaimsByRoleIdAsync(string roleId)
        {
            throw new NotImplementedException();
        }
    }
}
