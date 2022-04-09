using System.ComponentModel.DataAnnotations;

namespace Domain;

public class EntityBase
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public byte[]? RowVersion { get; set; }
}