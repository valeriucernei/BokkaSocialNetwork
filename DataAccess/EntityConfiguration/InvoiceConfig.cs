using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfiguration;

public class InvoiceConfig : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.Property(i => i.Amount)
            .IsRequired();
        
        builder.Property(i => i.CreatedDateTime)
            .IsRequired();
        
        builder.Property(i => i.Status)
            .IsRequired();
        
        builder.Property(i => i.UserId)
            .IsRequired();
        
        builder.Property(i => i.RowVersion)
            .IsRowVersion();
    }
}