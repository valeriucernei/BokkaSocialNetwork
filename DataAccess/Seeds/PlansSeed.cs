using Domain.Models;

namespace DataAccess.Seeds;

public static class PlansSeed
{
    public static async Task Seed(Context context)
    {
        if (!context.Plans.Any())
        {
            var plan1 = new Plan()
            {
                Title = "Starter plan",
                Description = "A starter plan to accomodate with our website and get benefit of previewing " +
                              "all content with no restrictions.",
                Days = 30,
                Price = 5
            };

            var plan2 = new Plan()
            {
                Title = "Loyal plan",
                Description = "Most popular plan. Great price for convenient subscription period just for " +
                              "all our loyal fans. 3 months for price of 2!",
                Days = 90,
                Price = 10
            };

            var plan3 = new Plan()
            {
                Title = "Premium plan",
                Description =
                    "For professionals by professionals. Buy this plan and have no stress for half an year. " +
                    "Just enjoy the best private content on the web.",
                Days = 180,
                Price = 20
            };

            await context.Plans.AddAsync(plan1);
            await context.Plans.AddAsync(plan2);
            await context.Plans.AddAsync(plan3);

            await context.SaveChangesAsync();
        }
    }
}