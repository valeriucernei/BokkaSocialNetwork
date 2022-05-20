using System.Text;
using BL.Interfaces;
using BL.Services;
using DataAccess;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Domain.Models.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static WebApplicationBuilder AddRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IRepository, Repository>();
        builder.Services.AddScoped<IUsersRepository, UsersRepository>();
        builder.Services.AddScoped<IPostsRepository, PostsRepository>();
        builder.Services.AddScoped<ILikesRepository, LikesRepository>();
        builder.Services.AddScoped<IPhotosRepository, PhotosRepository>();
        builder.Services.AddScoped<IInvoicesRepository, InvoicesRepository>();
        builder.Services.AddScoped<ISubscriptionsRepository, SubscriptionsRepository>();

        builder.Services.AddScoped<IUsersService, UsersService>();
        builder.Services.AddScoped<IPostsService, PostsService>();
        builder.Services.AddScoped<ILikesService, LikesService>();
        builder.Services.AddScoped<IPlansService, PlansService>();
        builder.Services.AddScoped<IPhotosService, PhotosService>();
        builder.Services.AddScoped<IInvoicesService, InvoicesService>();
        builder.Services.AddScoped<ISubscriptionsService, SubscriptionsService>();
        
        return builder;
    }

    public static WebApplicationBuilder AddAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentity<User, Role>(options =>
        {
            options.Password.RequiredLength = 6;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequiredUniqueChars = 0;
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789.";
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<Context>()
        .AddDefaultTokenProviders();
        
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = builder.Configuration["JWT:ValidAudience"],
                ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
            };
        });

        return builder;
    }
}