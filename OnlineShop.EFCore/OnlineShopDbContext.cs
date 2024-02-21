using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShop.EFCore.Frameworks;
using OnlineShop.Domain.Frameworks.Abstracts;
using System.Reflection;
using PublicTools.Constants;

namespace OnlineShop.EFCore;

public class OnlineShopDbContext(DbContextOptions options) : IdentityDbContext<IdentityUser, IdentityRole, string,
        IdentityUserClaim<string>, IdentityUserRole<string> , IdentityUserLogin<string>,
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
