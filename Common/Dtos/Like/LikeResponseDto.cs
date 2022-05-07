namespace Common.Dtos.Like;

public class LikeResponseDto
{
    public bool IsLiked { get; set; }
    public string Message { get; set; } = String.Empty;
    public int LikesCount { get; set; }
}