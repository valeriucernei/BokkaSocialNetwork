namespace Common.Dtos.Plan;

public class PlanDto
{
    public string Id { get; set; } = String.Empty;
    public string Title { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public int Days { get; set; }
    public int Price { get; set; }
    public string PriceId { get; set; } = String.Empty;
}