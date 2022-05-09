namespace Domain.Models;

public class Plan : BaseEntity
{
    public string Title { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public int Days { get; set; }
    public int Price { get; set; }
}