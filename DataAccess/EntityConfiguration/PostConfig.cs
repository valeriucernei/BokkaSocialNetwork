using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfiguration;

public class PostConfig : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.Property(p => p.Title)
            .IsRequired()
            .IsUnicode()
            .HasMaxLength(64);
        
        builder.Property(p => p.Content)
            .IsUnicode()
            .HasMaxLength(512);
        
        builder.Property(p => p.UserId)
            .IsRequired();
    }
}