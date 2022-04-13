using Microsoft.AspNetCore.Identity;

namespace Domain.Models.Auth;

public class Role : IdentityRole<Guid>
{
    public Role(string roleName) : base(roleName) { }
    public Role() { }
}