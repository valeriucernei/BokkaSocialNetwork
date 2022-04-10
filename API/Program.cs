using DataAccess;
using DataAccess.Seeds;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(Context))));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using(var scope = app.Services.CreateScope())
{
    using (var context = scope.ServiceProvider.GetRequiredService<Context>())
    {
        //UsersSeed.Seed(context);
        User user = context.Users.First(u => u.Auth0UserId == "test");
        //Console.WriteLine(user);
        PostsSeed.Seed(context, user);
    }
}

app.Run();