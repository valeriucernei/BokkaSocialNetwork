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
                Id = Guid.NewGuid().ToString(),
                UserName = "test",
                Email = "test@gmail.com",
                FirstName = "Test",
                LastName = "User"
            };

            await userManager.CreateAsync(user, "Asd12345");
        }
    }
}