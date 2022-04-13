using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfiguration;

public class PhotoConfig : IEntityTypeConfiguration<Photo>
{
    public void Configure(EntityTypeBuilder<Photo> builder)
    {
        builder.Property(p => p.Extension)
            .IsRequired()
            .HasMaxLength(6);
        
        builder.Property(p => p.PostId)
            .IsRequired();
        
        builder.Property(p => p.Extension)
            .IsRequired();
    }
}