using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Domain.Aggregates.SaleAggregates;
using PublicTools.Constants;

namespace OnlineShop.EFCore.Configurations.SaleConfigurations;

internal class OrderHeaderConfiguration : IEntityTypeConfiguration<OrderHeader>
{
    public void Configure(EntityTypeBuilder<OrderHeader> builder)
    {
        builder.ToTable(nameof(OrderHeader), DatabaseConstants.Schemas.Sale);
        builder.HasKey(p => p.Id);
        builder.Property(p => p.BuyerId).IsRequired();

        // Main Entity Configuration
        builder.Property(p => p.Code).IsRequired();
        builder.HasIndex(p => p.Code).IsUnique();
        builder.Property(p => p.CreatedDateGregorian).IsRequired();
        builder.Property(p => p.CreatedDatePersian).IsRequired();
        builder.Property(p => p.IsModified).IsRequired().HasDefaultValue(false);
        builder.Property(p => p.IsSoftDeleted).IsRequired().HasDefaultValue(false);

        builder.HasOne(oh => oh.Buyer).WithMany().OnDelete(DeleteBehavior.Restrict);
    }
}
