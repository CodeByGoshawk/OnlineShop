using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Domain.Aggregates.SaleAggregates;
using PublicTools.Constants;

namespace OnlineShop.EFCore.Configurations.SaleConfigurations;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(nameof(Product), DatabaseConstants.Schemas.Sale);
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Code).IsRequired();
        builder.Property(p => p.Title).IsRequired();
        builder.Property(p => p.UnitPrice).IsRequired();
        builder.Property(p => p.ProductCategoryId).IsRequired();
    }
}
