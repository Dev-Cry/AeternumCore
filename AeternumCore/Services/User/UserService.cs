using AeternumCore.Data.DataTransferObjects;
using AeternumCore.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AeternumCore.Services.User.Role;

namespace AeternumCore.Services.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUserEntity> _userManager;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(UserManager<ApplicationUserEntity> userManager, IRoleService roleService, IMapper mapper, ILogger<UserService> logger)
        {
            _userManager = userManager;
            _roleService = roleService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApplicationUserDto> GetUserByIdAsync(string userId)
        {
            _logger.LogInformation("Fetching user by ID: {UserId}", userId);
            var userEntity = await _userManager.FindByIdAsync(userId);
            if (userEntity == null)
            {
                _logger.LogWarning("User with ID {UserId} not found.", userId);
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }
            return _mapper.Map<ApplicationUserDto>(userEntity);
        }

        public async Task<IEnumerable<ApplicationUserDto>> GetAllUsersAsync()
        {
            _logger.LogInformation("Fetching all users");
            var userEntities = await _userManager.Users.ToListAsync();
            return _mapper.Map<IEnumerable<ApplicationUserDto>>(userEntities);
        }

        public async Task<ApplicationUserDto> CreateUserAsync(ApplicationUserDto userDto)
        {
            _logger.LogInformation("Creating user with email: {Email}", userDto.Email);
            var userEntity = _mapper.Map<ApplicationUserEntity>(userDto);
            var result = await _userManager.CreateAsync(userEntity);
            if (result.Succeeded)
            {
                _logger.LogInformation("User created successfully: {UserId}", userEntity.Id);
                return _mapper.Map<ApplicationUserDto>(userEntity);
            }

            var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
            _logger.LogError("Error creating user {Email}: {Errors}", userDto.Email, errorMessages);
            throw new Exception($"User creation failed: {errorMessages}");
        }

        public async Task<ApplicationUserDto> UpdateUserAsync(ApplicationUserDto userDto)
        {
            _logger.LogInformation("Updating user with ID: {UserId}", userDto.Id);
            var userEntity = await _userManager.FindByIdAsync(userDto.Id);
            if (userEntity == null)
            {
                _logger.LogWarning("User with ID {UserId} not found.", userDto.Id);
                throw new KeyNotFoundException($"User with ID {userDto.Id} not found.");
            }

            _mapper.Map(userDto, userEntity);
            var result = await _userManager.UpdateAsync(userEntity);
            if (result.Succeeded)
            {
                _logger.LogInformation("User updated successfully: {UserId}", userEntity.Id);
                return _mapper.Map<ApplicationUserDto>(userEntity);
            }

            var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
            _logger.LogError("Error updating user {UserId}: {Errors}", userDto.Id, errorMessages);
            throw new Exception($"User update failed: {errorMessages}");
        }

        public async Task DeleteUserAsync(string userId)
        {
            _logger.LogInformation("Deleting user with ID: {UserId}", userId);
            var userEntity = await _userManager.FindByIdAsync(userId);
            if (userEntity == null)
            {
                _logger.LogWarning("User with ID {UserId} not found.", userId);
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            var result = await _userManager.DeleteAsync(userEntity);
            if (result.Succeeded)
            {
                _logger.LogInformation("User deleted successfully: {UserId}", userId);
            }
            else
            {
                var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogError("Error deleting user {UserId}: {Errors}", userId, errorMessages);
                throw new Exception($"User deletion failed: {errorMessages}");
            }
        }

        public async Task<ApplicationUserDto> AssignRoleToUserAsync(string userId, string roleId)
        {
            _logger.LogInformation("Assigning role '{RoleId}' to user '{UserId}'", roleId, userId);
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning("User with ID {UserId} not found.", userId);
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            var role = await _roleService.GetRoleByIdAsync(roleId);
            if (role == null)
            {
                _logger.LogWarning("Role with ID {RoleId} not found.", roleId);
                throw new KeyNotFoundException($"Role with ID {roleId} not found.");
            }

            var result = await _userManager.AddToRoleAsync(user, role.Name);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogError("Error assigning role '{RoleId}' to user '{UserId}': {Errors}", roleId, userId, errors);
                throw new Exception($"Failed to assign role: {errors}");
            }

            _logger.LogInformation("Role '{RoleId}' assigned to user '{UserId}' successfully", roleId, userId);
            return _mapper.Map<ApplicationUserDto>(user);
        }

        public async Task<ApplicationUserDto> RemoveRoleFromUserAsync(string userId, string roleId)
        {
            _logger.LogInformation("Removing role '{RoleId}' from user '{UserId}'", roleId, userId);
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning("User with ID {UserId} not found.", userId);
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            var result = await _userManager.RemoveFromRoleAsync(user, roleId);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogError("Error removing role '{RoleId}' from user '{UserId}': {Errors}", roleId, userId, errors);
                throw new Exception($"Failed to remove role: {errors}");
            }

            _logger.LogInformation("Role '{RoleId}' removed from user '{UserId}' successfully", roleId, userId);
            return _mapper.Map<ApplicationUserDto>(user);
        }
    }
}
