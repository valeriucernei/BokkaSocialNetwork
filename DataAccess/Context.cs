using Domain.Models;
using Domain.Models.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class Context : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
{
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<Like> Likes => Set<Like>();
    public DbSet<Photo> Photos => Set<Photo>();
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Subscription> Subscriptions => Set<Subscription>();
    
    public Context() {}
    
    public Context(DbContextOptions<Context> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.;Database=BokkaSocialNetwork;User Id=sa;Password=Anon-1999;MultipleActiveResultSets=True;");
        base.OnConfiguring(optionsBuilder); // just for migrations
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly); //.Seed()
    }
}