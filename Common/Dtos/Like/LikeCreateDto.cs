using System.ComponentModel.DataAnnotations;

namespace Common.Dtos.Like;

public class LikeCreateDto
{
    [Required]
    public Guid PostId { get; set; }
}