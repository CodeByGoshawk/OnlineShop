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
        builder.Property(p => p.SellerId).IsRequired();
        builder.Property(p => p.Title).IsRequired();
        builder.Property(p => p.UnitPrice).IsRequired();
        builder.Property(p => p.ProductCategoryId).IsRequired();

        // Main Entity Configuration
        builder.Property(p => p.Code).IsRequired();
        builder.HasIndex(p => p.Code).IsUnique();
        builder.Property(p => p.CreatedDateGregorian).IsRequired();
        builder.Property(p => p.CreatedDatePersian).IsRequired();
        builder.Property(p => p.IsModified).IsRequired().HasDefaultValue(false);
        builder.Property(p => p.IsSoftDeleted).IsRequired().HasDefaultValue(false);
    }
}
