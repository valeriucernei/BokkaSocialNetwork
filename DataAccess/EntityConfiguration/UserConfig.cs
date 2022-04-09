using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfiguration;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Auth0UserId)
            .HasMaxLength(256);
        
        builder.Property(u => u.FirstName)
            .IsRequired()
            .IsUnicode()
            .HasMaxLength(24);
        
        builder.Property(u => u.LastName)
            .IsUnicode()
            .HasMaxLength(24);
        
        builder.Property(u => u.Status)
            .IsRequired();

        builder.Property(u => u.RegisterDateTime)
            .IsRequired();
        
        builder.Property(u => u.RowVersion)
            .IsRowVersion();
    }
}