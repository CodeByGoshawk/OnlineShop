using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace OnlineShop.EFCore.Frameworks;

internal static class ModelBuilderExtensions
{
    public static void RegisterAllEntities<IDbSetEntity>(this ModelBuilder modelBuilder, params Assembly[] assemblies)
    {
        IEnumerable<Type> types = assemblies.SelectMany(a => a.GetExportedTypes()).Where(c => c.IsClass && !c.IsAbstract && c.IsPublic &&
            typeof(IDbSetEntity).IsAssignableFrom(c));
        foreach (Type type in types)
            modelBuilder.Entity(type);
    }
}
