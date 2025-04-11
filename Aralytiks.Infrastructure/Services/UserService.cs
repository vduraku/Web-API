using Aralytiks.Domain.DTOs;
using Aralytiks.Domain.Entities;
using Aralytiks.Domain.Interfaces;
using Aralytiks.Domain.Interfaces.Services;
using Aralytiks.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aralytiks.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> _userRepository;
    private readonly IUserMapper _userMapper;

    public UserService(IRepository<User> userRepository, IUserMapper userMapper)
    {
        _userRepository = userRepository;
        _userMapper = userMapper;
    }

    public async Task<IEnumerable<UserDTO>> GetAllUsersAsync(int pageNumber = 1, int pageSize = 10)
    {
        var users = await _userRepository.GetAllAsync(pageNumber, pageSize);
        return users.Select(u => _userMapper.MapToDTO(u));
    }

    public async Task<UserDTO> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            return null;
        }
        return _userMapper.MapToDTO(user);
    }

    public async Task<UserDTO> CreateUserAsync(UserDTO userDto)
    {
        var user = _userMapper.MapToEntity(userDto, isUpdate: false);
        user.CreatedAt = DateTime.UtcNow;
        user.CreatedBy = "System";//loggedIn user
        user.UpdatedAt = null;
        user.UpdatedBy = null;

        var createdUser = await _userRepository.AddAsync(user);
        return _userMapper.MapToDTO(createdUser);
    }

    public async Task UpdateUserAsync(int id, UserDTO userDto)
    {
        if (id != userDto.Id)
        {
            throw new ArgumentException("Id not the same!");
        }

        var existingUser = await _userRepository.GetByIdAsync(id);
        if (existingUser == null)
        {
            throw new KeyNotFoundException($"User with id {id} not found");
        }

        existingUser.Username = userDto.Username;
        existingUser.Password = userDto.Password;
        existingUser.FirstName = userDto.FirstName;
        existingUser.LastName = userDto.LastName;
        existingUser.DateOfBirth = userDto.DateOfBirth;
        existingUser.SettingsJson = userDto.SettingsJson;
        existingUser.UpdatedAt = DateTime.UtcNow;
        existingUser.UpdatedBy = "System";//loggedIn user

        await _userRepository.UpdateAsync(existingUser);
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with id {id} not found");
        }

        await _userRepository.DeleteAsync(user);
    }
}