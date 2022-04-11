using Microsoft.AspNetCore.Identity;
using System.Collections.ObjectModel;

namespace Domain.Models.Auth
{
    public class User : IdentityUser<string>
    {
        public string FirstName { get; set; } = String.Empty;
        public string? LastName { get; set; }
        public DateTime RegisterDateTime { get; set; } = DateTime.Now;
        
        public ICollection<Post> Posts { get; set; } = new Collection<Post>();
        public ICollection<Like> Likes { get; set; } = new Collection<Like>();
        public ICollection<Subscription> Subscriptions { get; set; } = new Collection<Subscription>();
        public ICollection<Invoice> Invoices { get; set; } = new Collection<Invoice>();
    }
}
