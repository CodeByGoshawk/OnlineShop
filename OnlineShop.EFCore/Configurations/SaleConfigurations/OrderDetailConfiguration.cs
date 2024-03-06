using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Domain.Aggregates.SaleAggregates;
using PublicTools.Constants;

namespace OnlineShop.EFCore.Configurations.SaleConfigurations;

internal class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetails>
{
    public void Configure(EntityTypeBuilder<OrderDetails> builder)
    {
        builder.ToTable(nameof(OrderDetails), DatabaseConstants.Schemas.Sale);
        builder.HasKey(p => new
        {
            p.OrderHeaderId,
            p.ProductId
        });
        builder.Property(p => p.Quantity).HasColumnType("money").IsRequired();
        builder.Property(p => p.UnitPrice).HasColumnType("money").IsRequired();
    }
}
