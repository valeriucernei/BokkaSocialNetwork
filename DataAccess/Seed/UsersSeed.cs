using Domain.Models;

namespace DataAccess.Seed;

public class UsersSeed
{
    public static async Task Seed(Context context)
    {
        if (!context.Users.Any())
        {
            var users = new List<User> { 
                new User()
                {
                    FirstName = "Test",
                    LastName = "User"
                },
                
                new User()
                {
                    FirstName = "Test2"
                }
            };

            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();
        }
    }
}