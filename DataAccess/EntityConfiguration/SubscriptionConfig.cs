using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfiguration;

public class SubscriptionConfig : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.Property(s => s.StartDateTime)
            .IsRequired();
        
        builder.Property(s => s.EndDateTime)
            .IsRequired();
        
        builder.Property(s => s.UserId)
            .IsRequired();
    }
}