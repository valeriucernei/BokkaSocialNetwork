using Domain.Models;
using Domain.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfiguration;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(32);
        
        builder.Property(u => u.FirstName)
            .IsRequired()
            .IsUnicode()
            .HasMaxLength(24);
        
        builder.Property(u => u.LastName)
            .IsUnicode()
            .HasMaxLength(24);
        
        builder.Property(u => u.Email)
            .HasMaxLength(128);

        builder.Property(u => u.RegisterDateTime)
            .IsRequired();
    }
}