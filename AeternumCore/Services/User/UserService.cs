using AeternumCore.Data.DataTransferObjects;
using AeternumCore.Data.Entities;
using AeternumCore.Services.User;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUserEntity> _userManager;
    private readonly IMapper _mapper;
    private readonly ILogger<UserService> _logger;

    public UserService(UserManager<ApplicationUserEntity> userManager, IMapper mapper, ILogger<UserService> logger)
    {
        _userManager = userManager;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ApplicationUserDto> GetUserByIdAsync(string userId)
    {
        _logger.LogInformation("Fetching user by ID: {UserId}", userId);
        try
        {
            var userEntity = await _userManager.FindByIdAsync(userId);
            if (userEntity == null)
            {
                _logger.LogWarning("User with ID {UserId} not found.", userId);
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }
            return _mapper.Map<ApplicationUserDto>(userEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching user by ID {UserId}", userId);
            throw;
        }
    }

    public async Task<IEnumerable<ApplicationUserDto>> GetAllUsersAsync()
    {
        _logger.LogInformation("Fetching all users");
        try
        {
            var userEntities = await _userManager.Users.ToListAsync();
            return _mapper.Map<IEnumerable<ApplicationUserDto>>(userEntities);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching all users");
            throw;
        }
    }

    public async Task<ApplicationUserDto> CreateUserAsync(ApplicationUserDto userDto)
    {
        _logger.LogInformation("Creating user with email: {Email}", userDto.Email);
        try
        {
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred while creating user with email {Email}", userDto.Email);
            throw;
        }
    }

    public async Task<ApplicationUserDto> UpdateUserAsync(ApplicationUserDto userDto)
    {
        _logger.LogInformation("Updating user with ID: {UserId}", userDto.Id);
        try
        {
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred while updating user with ID {UserId}", userDto.Id);
            throw;
        }
    }

    public async Task DeleteUserAsync(string userId)
    {
        _logger.LogInformation("Deleting user with ID: {UserId}", userId);
        try
        {
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred while deleting user with ID {UserId}", userId);
            throw;
        }
    }
}
