using Domain.Models;

namespace DataAccess.Seeds;

public static class UsersSeed
{
    public static async Task Seed(Context context)
    {
        if (!context.Users.Any())
        {
            // var user = new User
            // {
            //     Auth0UserId = "test", 
            //     FirstName = "Test", 
            //     LastName = "User"
            // };
            //
            // await context.Users.AddAsync(user);
            //
            // await context.SaveChangesAsync();
        }
    }
}