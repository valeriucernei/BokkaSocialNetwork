using System.Collections.ObjectModel;
using Domain.Models.Auth;

namespace Domain.Models;

public sealed class Post : BaseEntity
{
    public string Title { get; set; } = String.Empty;
    public string? Content { get; set; }
    public ICollection<Photo> Photos { get; set; } = new Collection<Photo>();
    public ICollection<Like> Likes { get; set; } = new Collection<Like>();

    public Guid UserId { get; set; } = Guid.Empty;
    public User User { get; set; } = new User();
}