using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Domain.Aggregates.UserManagementAggregates;
using PublicTools.Constants;

namespace OnlineShop.EFCore.Configurations.UserManagementConfigurations;

internal class OnlineShopRoleConfiguration : IEntityTypeConfiguration<OnlineShopRole>
{
    public void Configure(EntityTypeBuilder<OnlineShopRole> builder)
    {
        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.NormalizedName).IsRequired();

        builder.ToTable(nameof(OnlineShopRole)).HasData(
            new OnlineShopRole
            {
                Id = DatabaseConstants.DefaultRoles.GodAdminId,
                Name = DatabaseConstants.DefaultRoles.GodAdminName,
                NormalizedName = DatabaseConstants.DefaultRoles.GodAdminNormalizedName,
                ConcurrencyStamp = DatabaseConstants.DefaultRoles.GodAdminConcurrencyStamp
            },
            new OnlineShopRole
            {
                Id = DatabaseConstants.DefaultRoles.AdminId,
                Name = DatabaseConstants.DefaultRoles.AdminName,
                NormalizedName = DatabaseConstants.DefaultRoles.AdminNormalizedName,
                ConcurrencyStamp = DatabaseConstants.DefaultRoles.AdminConcurrencyStamp
            },
            new OnlineShopRole
            {
                Id = DatabaseConstants.DefaultRoles.SellerId,
                Name = DatabaseConstants.DefaultRoles.SellerName,
                NormalizedName = DatabaseConstants.DefaultRoles.SellerNormalizedName,
                ConcurrencyStamp = DatabaseConstants.DefaultRoles.SellerConcurrencyStamp
            },
            new OnlineShopRole
            {
                Id = DatabaseConstants.DefaultRoles.BuyerId,
                Name = DatabaseConstants.DefaultRoles.BuyerName,
                NormalizedName = DatabaseConstants.DefaultRoles.BuyerNormalizedName,
                ConcurrencyStamp = DatabaseConstants.DefaultRoles.BuyerConcurrencyStamp
            });
    }
}
