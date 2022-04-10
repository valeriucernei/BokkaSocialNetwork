using System.ComponentModel.DataAnnotations;

namespace Common.Dtos.Post;

public class PostForUpdateDto
{
    [Required]
    [StringLength(64,MinimumLength = 6)]
    public string Title { get; set; } = String.Empty;
    
    [StringLength(512)]
    public string Content { get; set; } = String.Empty;
    
    [Required]
    public DateTime CreatedDateTime { get; set; }
    
    [Required]
    public Guid UserId { get; set; }
}