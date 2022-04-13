using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models.Auth;

public class User : IdentityUser<Guid>, IBaseEntity
{
    public override Guid Id { get; set; }
    
    public string FirstName { get; set; } = String.Empty;
    public string? LastName { get; set; } = null;
    
    public override string? Email { get; set; }
    
    public DateTime RegisterDateTime { get; set; } = DateTime.Now;
    
    public ICollection<Post> Posts { get; set; } = new Collection<Post>();
    public ICollection<Like> Likes { get; set; } = new Collection<Like>();
    public ICollection<Subscription> Subscriptions { get; set; } = new Collection<Subscription>();
    public ICollection<Invoice> Invoices { get; set; } = new Collection<Invoice>();

    public override string ToString()
    {
        return $"***User Info***" +
               $"\n- ID: {Id.ToString()}" +
               $"\n- FirstName: {FirstName}" +
               $"\n- LastName: {LastName}" +
               $"\n- Email: {Email}" +
               $"\n- RegisterDateTime: {RegisterDateTime}";
    }
}