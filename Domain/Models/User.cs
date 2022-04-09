using System.Collections.ObjectModel;
using Common.Enums;

namespace Domain.Models;

public class User : EntityBase
{
    public string Auth0UserId { get; set; } = String.Empty;
    public string FirstName { get; set; } = String.Empty;
    public string? LastName { get; set; } = null;

    public UserStatus Status { get; set; } = UserStatus.Active;
    public DateTime RegisterDateTime { get; set; } = DateTime.Now;
    public ICollection<Post> Posts { get; set; } = new Collection<Post>();
    public ICollection<Like> Likes { get; set; } = new Collection<Like>();
    public ICollection<Subscription> Subscriptions { get; set; } = new Collection<Subscription>();
    public ICollection<Invoice> Invoices { get; set; } = new Collection<Invoice>();
}