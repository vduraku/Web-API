using Aralytiks.Domain.DTOs;
using Aralytiks.Domain.Entities;

namespace Aralytiks.Infrastructure.Mapping;

public interface IPostMapper
{
    PostDTO MapToDTO(Post post);
    Post MapToEntity(PostDTO postDto, bool isUpdate = false);
} 