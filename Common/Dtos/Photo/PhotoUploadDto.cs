using System.ComponentModel.DataAnnotations;

namespace Common.Dtos.Photo;

public class PhotoUploadDto
{
    [Required]
    public string Base64 { get; set; } = String.Empty;
    
    [Required]
    public string Extension { get; set; } = "png";
    
    [Required]
    public Guid PostId { get; set; } = Guid.Empty;
}