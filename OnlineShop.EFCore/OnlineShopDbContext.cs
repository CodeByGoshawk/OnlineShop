using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace OnlineShop.EFCore;

public class OnlineShopDbContext(DbContextOptions options) : IdentityDbContext<IdentityUser>(options)
{
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        //builder.RegisterAllEntities<IDbSetEntity>(typeof(IDbSetEntity).Assembly); => Extension Method Needed
        base.OnModelCreating(builder);
    }
}
