using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Domain.Aggregates.UserManagementAggregates;
using PublicTools.Constants;
using PublicTools.Tools;

namespace OnlineShop.EFCore.Configurations.UserManagementConfigurations;

internal class OnlineShopUserConfiguration : IEntityTypeConfiguration<OnlineShopUser>
{
    public void Configure(EntityTypeBuilder<OnlineShopUser> builder)
    {
        //builder.Property(p => p.FirstName).IsRequired(); // Commented to check if these are necessary ? 
        //builder.Property(p => p.LastName).IsRequired();

        builder.Property(p => p.NationalId).IsRequired();
        builder.HasIndex(p => p.NationalId).IsUnique();
        builder.Property(p => p.IsNationalIdConfirmed).HasDefaultValue(false);

        builder.Property(p => p.CellPhone).IsRequired();
        builder.HasIndex(p => p.CellPhone).IsUnique();
        builder.Property(p => p.IsCellPhoneConfirmed).HasDefaultValue(false);

        builder.Property(p => p.IsActive).IsRequired().HasDefaultValue(true); // Is IsRequired necessary ? 

        builder.Property(p => p.CreatedDateGregorian).IsRequired();
        builder.Property(p => p.CreatedDatePersian).IsRequired();

        builder.Property(p => p.IsModified).HasDefaultValue(false);
        builder.Property(p => p.IsSoftDeleted).HasDefaultValue(false);

        builder.ToTable(nameof(OnlineShopUser)).HasData(
            new OnlineShopUser
            {
                Id = DatabaseConstants.GodAdminUsers.ShahbaziUserId,
                FirstName = DatabaseConstants.GodAdminUsers.ShahbaziFirstName,
                LastName = DatabaseConstants.GodAdminUsers.ShahbaziLastName,
                NationalId = DatabaseConstants.GodAdminUsers.ShahbaziNationalId,
                IsNationalIdConfirmed = true,
                CellPhone = DatabaseConstants.GodAdminUsers.ShahbaziCellPhone,
                IsCellPhoneConfirmed = true,
                CreatedDateGregorian = DateTime.Now,
                CreatedDatePersian = Helper.GregorianToPersianDateConverter(DateTime.Now)
            }) ;
    }
}
