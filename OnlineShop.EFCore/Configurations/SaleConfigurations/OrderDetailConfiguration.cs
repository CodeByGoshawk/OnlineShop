using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Domain.Aggregates.SaleAggregates;
using PublicTools.Constants;

namespace OnlineShop.EFCore.Configurations.SaleConfigurations;

internal class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.ToTable(nameof(OrderDetail), DatabaseConstants.Schemas.Sale);
        builder.HasKey(p => new
        {
            p.OrderHeaderId,
            p.ProductId
        });

        builder.Property(p => p.Quantity).HasColumnType("money").IsRequired();
        builder.Property(p => p.UnitPrice).HasColumnType("money").IsRequired();
    }
}
