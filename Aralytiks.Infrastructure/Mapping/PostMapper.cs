using Aralytiks.Domain.DTOs;
using Aralytiks.Domain.Entities;

namespace Aralytiks.Infrastructure.Mapping;

public class PostMapper : IPostMapper
{
    public PostDTO MapToDTO(Post post)
    {
        return new PostDTO
        {
            Id = post.Id,
            Title = post.Title,
            Description = post.Description,
            Content = post.Content,
            Slug = post.Slug,
            AuthorId = post.AuthorId,
            Author = post.Author != null ? new UserDTO
            {
                Id = post.Author.Id,
                Username = post.Author.Username,
                FirstName = post.Author.FirstName,
                LastName = post.Author.LastName,
                DateOfBirth = post.Author.DateOfBirth,
                SettingsJson = post.Author.SettingsJson,
                CreatedAt = post.Author.CreatedAt,
                UpdatedAt = post.Author.UpdatedAt,
                CreatedBy = post.Author.CreatedBy,
                UpdatedBy = post.Author.UpdatedBy
            } : null,
            CreatedAt = post.CreatedAt,
            UpdatedAt = post.UpdatedAt,
            CreatedBy = post.CreatedBy,
            UpdatedBy = post.UpdatedBy
        };
    }

    public Post MapToEntity(PostDTO postDto, bool isUpdate = false)
    {
        var post = new Post
        {
            Title = postDto.Title,
            Description = postDto.Description,
            Content = postDto.Content,
            Slug = postDto.Slug,
            AuthorId = postDto.AuthorId,
            CreatedAt = postDto.CreatedAt,
            UpdatedAt = postDto.UpdatedAt,
            CreatedBy = postDto.CreatedBy,
            UpdatedBy = postDto.UpdatedBy
        };
                
        if (isUpdate)
        {
            post.Id = postDto.Id;
        }

        return post;
    }
} 