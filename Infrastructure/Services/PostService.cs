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

public class PostService : IPostService
{
    private readonly IRepository<Post> _postRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IPostMapper _postMapper;

    public PostService(IRepository<Post> postRepository, IRepository<User> userRepository, IPostMapper postMapper)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
        _postMapper = postMapper;
    }

    public async Task<IEnumerable<PostDTO>> GetAllPostsAsync(int pageNumber = 1, int pageSize = 10)
    {
        var posts = await _postRepository.GetAllAsync(pageNumber, pageSize);
        return posts.Select(p => _postMapper.MapToDTO(p));
    }

    public async Task<PostDTO> GetPostByIdAsync(int id)
    {
        var post = await _postRepository.GetByIdAsync(id);
        if (post == null)
        {
            return null;
        }
        return _postMapper.MapToDTO(post);
    }

    public async Task<PostDTO> CreatePostAsync(PostDTO postDto)
    {
        // Check that the author exists first
        var author = await _userRepository.GetByIdAsync(postDto.AuthorId);
        if (author == null)
        {
            throw new ArgumentException("Author not found");
        }

        var post = _postMapper.MapToEntity(postDto, isUpdate: false);
        post.CreatedAt = DateTime.UtcNow;
        post.CreatedBy = "System";
        post.UpdatedAt = null;
        post.UpdatedBy = null;

        var createdPost = await _postRepository.AddAsync(post);
        return _postMapper.MapToDTO(createdPost);
    }

    public async Task UpdatePostAsync(int id, PostDTO postDto)
    {
        if (id != postDto.Id)
        {
            throw new ArgumentException("Id mismatch");
        }

        var existingPost = await _postRepository.GetByIdAsync(id);
        if (existingPost == null)
        {
            throw new KeyNotFoundException($"Post with id {id} not found");
        }

        // Check that the author exists first
        var author = await _userRepository.GetByIdAsync(postDto.AuthorId);
        if (author == null)
        {
            throw new ArgumentException("Author not found");
        }

        existingPost.Title = postDto.Title;
        existingPost.Description = postDto.Description;
        existingPost.Content = postDto.Content;
        existingPost.Slug = postDto.Slug;
        existingPost.AuthorId = postDto.AuthorId;
        existingPost.UpdatedAt = DateTime.UtcNow;
        existingPost.UpdatedBy = "System";

        await _postRepository.UpdateAsync(existingPost);
    }

    public async Task DeletePostAsync(int id)
    {
        var post = await _postRepository.GetByIdAsync(id);
        if (post == null)
        {
            throw new KeyNotFoundException($"Post with id {id} not found");
        }

        await _postRepository.DeleteAsync(post);
    }
} 