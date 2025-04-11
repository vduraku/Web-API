using Aralytiks.Domain.DTOs;
using Aralytiks.Domain.Entities;

namespace Aralytiks.Infrastructure.Mapping;

public interface IUserMapper
{
    UserDTO MapToDTO(User user);
    User MapToEntity(UserDTO userDto, bool isUpdate = false);
} 