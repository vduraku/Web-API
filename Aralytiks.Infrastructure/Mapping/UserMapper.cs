using Aralytiks.Domain.DTOs;
using Aralytiks.Domain.Entities;

namespace Aralytiks.Infrastructure.Mapping;

public class UserMapper : IUserMapper
{
    public UserDTO MapToDTO(User user)
    {
        return new UserDTO
        {
            Id = user.Id,
            Username = user.Username,
            FirstName = user.FirstName,
            LastName = user.LastName,
            DateOfBirth = user.DateOfBirth,
            SettingsJson = user.SettingsJson,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
            CreatedBy = user.CreatedBy,
            UpdatedBy = user.UpdatedBy
        };
    }

    public User MapToEntity(UserDTO userDto, bool isUpdate = false)
    {
        var user = new User
        {
            Username = userDto.Username,
            Password = userDto.Password,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            DateOfBirth = userDto.DateOfBirth,
            SettingsJson = userDto.SettingsJson,
            CreatedAt = userDto.CreatedAt,
            UpdatedAt = userDto.UpdatedAt,
            CreatedBy = userDto.CreatedBy,
            UpdatedBy = userDto.UpdatedBy
        };

        // Only set the Id if we're updating an existing entity
        if (isUpdate)
        {
            user.Id = userDto.Id;
        }

        return user;
    }
} 