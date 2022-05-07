namespace Common.Dtos.Photo;

public class PhotoDto
{
    public Guid Id { get; set; }
    public string Extension { get; set; } = String.Empty;
    public Guid PostId { get; set; } 
}