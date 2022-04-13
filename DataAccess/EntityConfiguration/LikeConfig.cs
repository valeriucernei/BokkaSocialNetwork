using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfiguration;

public class LikeConfig : IEntityTypeConfiguration<Like>
{
    public void Configure(EntityTypeBuilder<Like> builder)
    {
        builder.Property(l => l.CreatedDateTime)
            .IsRequired();
        
        builder.Property(l => l.PostId)
            .IsRequired();
        
        builder.Property(l => l.UserId)
            .IsRequired();
        
        builder.HasOne(l => l.User)
            .WithMany(u => u.Likes)
            .OnDelete(DeleteBehavior.NoAction);
    }
}