using DataAccess;
using DataAccess.Seeds;
using Domain.Models.Auth;
using Microsoft.AspNetCore.Identity;

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
            var userManager = services.GetRequiredService<UserManager<User>>();

            await UsersSeed.Seed(userManager);
            await PostsSeed.Seed(context);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occured during migration");
        }
    }
}