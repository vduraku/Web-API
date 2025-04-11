using Aralytiks.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aralytiks.Domain.Interfaces.Services;

public interface IPostService
{
    Task<IEnumerable<PostDTO>> GetAllPostsAsync(int pageNumber = 1, int pageSize = 10);
    Task<PostDTO> GetPostByIdAsync(int id);
    Task<PostDTO> CreatePostAsync(PostDTO postDto);
    Task UpdatePostAsync(int id, PostDTO postDto);
    Task DeletePostAsync(int id);
} 