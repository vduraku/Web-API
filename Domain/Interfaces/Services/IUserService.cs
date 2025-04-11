using Aralytiks.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aralytiks.Domain.Interfaces.Services;

public interface IUserService
{
    Task<IEnumerable<UserDTO>> GetAllUsersAsync(int pageNumber = 1, int pageSize = 10);
    Task<UserDTO> GetUserByIdAsync(int id);
    Task<UserDTO> CreateUserAsync(UserDTO userDto);
    Task UpdateUserAsync(int id, UserDTO userDto);
    Task DeleteUserAsync(int id);
} 