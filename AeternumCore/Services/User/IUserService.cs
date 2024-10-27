using AeternumCore.Data.DataTransferObjects;

namespace AeternumCore.Services.User
{
    public interface IUserService
    {
        Task<ApplicationUserDto> GetUserByIdAsync(string userId);
        Task<IEnumerable<ApplicationUserDto>> GetAllUsersAsync();
        Task<ApplicationUserDto> CreateUserAsync(ApplicationUserDto userDto);
        Task<ApplicationUserDto> UpdateUserAsync(ApplicationUserDto userDto);
        Task DeleteUserAsync(string userId);
    }
}
