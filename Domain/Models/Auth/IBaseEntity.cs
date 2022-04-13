using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Auth;

public interface IBaseEntity
{
    [Key]
    public Guid Id { get; set; }
}