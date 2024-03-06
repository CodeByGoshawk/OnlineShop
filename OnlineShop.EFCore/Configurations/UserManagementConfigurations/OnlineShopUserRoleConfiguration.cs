using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Domain.Aggregates.UserManagementAggregates;
using PublicTools.Constants;

namespace OnlineShop.EFCore.Configurations.UserManagementConfigurations;

internal class OnlineShopUserRoleConfiguration : IEntityTypeConfiguration<OnlineShopUserRole>
{
    public void Configure(EntityTypeBuilder<OnlineShopUserRole> builder)
    {
        builder.ToTable(nameof(OnlineShopUserRole)).HasData(
            new OnlineShopUserRole
            {
                UserId = DatabaseConstants.GodAdminUsers.ShahbaziUserId,
                RoleId = DatabaseConstants.DefaultRoles.GodAdminId
            });
        //builder.HasKey(p => new  // Should inherit from IdentityDbContext.
        //{
        //    p.UserId,
        //    p.RoleId
        //});
    }
}
