using System.ComponentModel.DataAnnotations;

namespace Common.Dtos.Post;

public class PostForUpdateDto
{
    [Required]
    [StringLength(64, MinimumLength = 5)]
    public string Title { get; set; } = String.Empty;
    
    [MaxLength(512)]
    public string Content { get; set; } = String.Empty;
}