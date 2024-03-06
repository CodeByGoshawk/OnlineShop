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
        builder.Property(p => p.Code).IsRequired();
        builder.Property(p => p.SellerId).IsRequired();
        builder.Property(p => p.BuyerId).IsRequired();
        builder.Property(p => p.CreatedDateGregorian).IsRequired().HasDefaultValue(DateTime.Now);
        builder.Property(p => p.CreatedDatePersian).IsRequired();
    }
}
