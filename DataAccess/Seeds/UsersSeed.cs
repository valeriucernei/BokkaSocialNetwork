using Domain.Models.Auth;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Seeds;

public static class UsersSeed
{
    public static async Task Seed(UserManager<User> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new User
            {
                UserName = "test",
                FirstName = "Test", 
                LastName = "User",
                Email = "test@gmail.com"
            };
            
            await userManager.CreateAsync(user, "123321");
        }
    }
}