using System.ComponentModel.DataAnnotations;

namespace Aralytiks.Domain.Entities;

public class Post : BaseEntity
{
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string Content { get; set; } = string.Empty;
    
    [StringLength(200)]
    public string Slug { get; set; } = string.Empty;

    public int AuthorId { get; set; }
    public virtual User Author { get; set; } = null!;
} 