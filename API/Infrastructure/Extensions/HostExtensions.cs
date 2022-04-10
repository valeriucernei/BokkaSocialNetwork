using DataAccess;
using DataAccess.Seeds;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Extensions;

public static class HostExtensions
{
    public static async Task SeedData(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        
        var services = scope.ServiceProvider;
        
        try
        {
            var context = services.GetRequiredService<Context>();
            await context.Database.MigrateAsync();
                
            await UsersSeed.Seed(context);
                
            User user = context.Users.First(u => u.Auth0UserId == "test");
                
            await PostsSeed.Seed(context, user);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "***An error occured during migration");
        }
    }
}