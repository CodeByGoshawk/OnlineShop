using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Domain.Aggregates.SaleAggregates;
using PublicTools.Constants;
using PublicTools.Tools;

namespace OnlineShop.EFCore.Configurations.SaleConfigurations;

internal class OrderHeaderConfiguration : IEntityTypeConfiguration<OrderHeader>
{
    public void Configure(EntityTypeBuilder<OrderHeader> builder)
    {
        builder.ToTable(nameof(OrderHeader), DatabaseConstants.Schemas.Sale);
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Code).IsRequired();
        builder.Property(p => p.SellerId).IsRequired();
        builder.Property(p => p.BuyerId).IsRequired();

        // Main Entity Configuration
        builder.Property(p => p.Code).IsRequired();
        builder.HasIndex(p => p.Code).IsUnique();
        builder.Property(p => p.CreatedDateGregorian).IsRequired().HasDefaultValue(DateTime.Now);
        builder.Property(p => p.CreatedDatePersian).IsRequired().HasDefaultValue(DateTime.Now.ConvertToPersian());
        builder.Property(p => p.IsModified).IsRequired().HasDefaultValue(false);
        builder.Property(p => p.IsSoftDeleted).IsRequired().HasDefaultValue(false);

        builder.HasOne(oh => oh.Buyer).WithMany().OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(oh => oh.Seller).WithMany().OnDelete(DeleteBehavior.Restrict);
        builder.HasQueryFilter(p => !p.IsSoftDeleted);
    }
}
