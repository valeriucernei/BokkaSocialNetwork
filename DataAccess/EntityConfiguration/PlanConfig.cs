using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfiguration;

public class PlanConfig : IEntityTypeConfiguration<Plan>
{
    public void Configure(EntityTypeBuilder<Plan> builder)
    {
        builder.Property(p => p.Title)
            .IsRequired();
        
        builder.Property(p => p.Description)
            .IsRequired();
        
        builder.Property(p => p.Days)
            .IsRequired();
        
        builder.Property(p => p.Price)
            .IsRequired();
    }
}