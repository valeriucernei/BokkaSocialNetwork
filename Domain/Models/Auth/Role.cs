using Microsoft.AspNetCore.Identity;

namespace Domain.Models.Auth
{
    public class Role : IdentityRole<string>
    {
        public Role(string roleName) : base(roleName) { }
        public Role() : base() { }
    }
}
