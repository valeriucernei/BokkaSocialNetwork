using Domain.Models;

namespace DataAccess.Seeds;

public static class PostsSeed
{
    public static async Task Seed(Context context, User user)
    {
        if (!context.Posts.Any())
        {
            //var user = context.Users.First(u => u.Auth0UserId == "test");
            
            var post1 = new Post
            {
                Title = "First Post!",
                Content =
                    "This is my first post on this website, idk what to type, i'm just an intern that is typing it on " +
                    "saturday, I have no weekend at all, I just need to write much more code than I ever wrote.",
                User = user
            };

            var post2 = new Post
            {
                Title = "Second Post",
                Content =
                    "So I've figured out how to seed this project, thx to Oleg's project and some tips from guys in the " +
                    "office. Doubt it's a good way to use seed posts like a personal blog. Btw, who is going to read this...",
                User = user
            };

            var post3 = new Post
            {
                Title = "Third and last post for seeding...",
                Content =
                    "My blog have survived about 15 minutes, while I was handling this f... seeding. Now it works, I'm " +
                    "not gonna spend any more minute writing this sh.. bye.",
                User = user
            };

            await context.Posts.AddAsync(post1);
            await context.Posts.AddAsync(post2);
            await context.Posts.AddAsync(post3);

            await context.SaveChangesAsync();
        }
    }
}