using Domain.Models.Auth;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Seeds;

public static class UsersSeed
{
    public static async Task SeedIdentityRoles(RoleManager<Role> roleManager)
    {
        if (!roleManager.Roles.Any())
        {
            await roleManager.CreateAsync(new Role() { Name = "User" });
            await roleManager.CreateAsync(new Role() { Name = "PaidUser" });
            await roleManager.CreateAsync(new Role() { Name = "Admin" });
        }
    }
    public static async Task Seed(UserManager<User> userManager)
    {
        // if (!userManager.Users.Any())
        // {
            var user = new User
            {
                UserName = "test",
                FirstName = "Test", 
                LastName = "User",
                Email = "test@gmail.com"
            };
            
            await userManager.CreateAsync(user, "123321");
            await userManager.AddToRoleAsync(user, "Admin");
        // }
    }
}