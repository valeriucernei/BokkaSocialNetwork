namespace Common.Dtos.Photo;

public class PhotoDto
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Extension { get; set; } = String.Empty;
    public Guid PostId { get; set; } = Guid.Empty;
}