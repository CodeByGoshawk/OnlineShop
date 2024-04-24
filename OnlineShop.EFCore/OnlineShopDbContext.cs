using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Aggregates.UserManagementAggregates;
using OnlineShop.Domain.Frameworks.Abstracts;
using OnlineShop.EFCore.Frameworks;
using PublicTools.Constants;
using System.Reflection;
namespace OnlineShop.EFCore;

public class OnlineShopDbContext(DbContextOptions options) : IdentityDbContext<OnlineShopUser, OnlineShopRole, string,
        IdentityUserClaim<string>, OnlineShopUserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>(options)
{
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema(DatabaseConstants.Schemas.UserManagement);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.RegisterAllEntities<IDbSetEntity>(typeof(IDbSetEntity).Assembly);

        base.OnModelCreating(builder);
    }
}
