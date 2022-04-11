using System.ComponentModel.DataAnnotations;

namespace Domain;

public class BaseEntity : IBaseEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public byte[]? RowVersion { get; set; }
}