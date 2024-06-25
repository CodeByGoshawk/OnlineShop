using Microsoft.AspNetCore.Identity;
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
        builder.Property(p => p.UserName).IsRequired();
        builder.HasIndex(p => p.UserName).IsUnique();
        builder.Property(p => p.NormalizedUserName).IsRequired();
        builder.HasIndex(p => p.NormalizedUserName).IsUnique();
        builder.Property(p => p.PasswordHash).IsRequired();

        builder.Property(p => p.FirstName).IsRequired();
        builder.Property(p => p.LastName).IsRequired();

        builder.Property(p => p.NationalId).IsRequired();
        builder.Property(p => p.IsNationalIdConfirmed).IsRequired().HasDefaultValue(false);

        builder.Property(p => p.CellPhone).IsRequired();
        builder.Property(p => p.IsCellPhoneConfirmed).IsRequired().HasDefaultValue(false);

        builder.Property(p => p.Email).IsRequired();
        builder.Property(p => p.NormalizedEmail).IsRequired();
        builder.Property(p => p.EmailConfirmed).IsRequired().HasDefaultValue(false);

        builder.Property(p => p.PhoneNumberConfirmed).IsRequired().HasDefaultValue(false);

        builder.Property(p => p.CreatedDateGregorian).IsRequired();
        builder.Property(p => p.CreatedDatePersian).IsRequired();
        builder.Property(p => p.IsModified).IsRequired().HasDefaultValue(false);
        builder.Property(p => p.IsSoftDeleted).IsRequired().HasDefaultValue(false);

        builder.Property(p => p.IsActive).IsRequired().HasDefaultValue(true);
        builder.Property(p => p.TwoFactorEnabled).IsRequired().HasDefaultValue(false);
        builder.Property(p => p.AccessFailedCount).IsRequired().HasDefaultValue(0);

        var shahbazi = new OnlineShopUser
        {
            Id = DatabaseConstants.GodAdminUsers.ShahbaziUserId,
            FirstName = DatabaseConstants.GodAdminUsers.ShahbaziFirstName,
            LastName = DatabaseConstants.GodAdminUsers.ShahbaziLastName,
            NationalId = DatabaseConstants.GodAdminUsers.ShahbaziNationalId,
            IsNationalIdConfirmed = true,
            CellPhone = DatabaseConstants.GodAdminUsers.ShahbaziCellPhone,
            IsCellPhoneConfirmed = true,
            Email = DatabaseConstants.GodAdminUsers.ShahbaziEmail,
            NormalizedEmail = DatabaseConstants.GodAdminUsers.ShahbaziEmail.ToUpper(),
            EmailConfirmed = true,
            CreatedDateGregorian = DateTime.Now,
            CreatedDatePersian = DateTime.Now.ConvertToPersian(),

            UserName = DatabaseConstants.GodAdminUsers.ShahbaziUserName,
            NormalizedUserName = DatabaseConstants.GodAdminUsers.ShahbaziUserName.ToUpper(),
        };

        var passwordHasher = new PasswordHasher<OnlineShopUser>();
        var hashedPasshword = passwordHasher.HashPassword(shahbazi, DatabaseConstants.GodAdminUsers.ShahbaziPassword);
        shahbazi.PasswordHash = hashedPasshword;

        builder.ToTable(nameof(OnlineShopUser)).HasData(shahbazi);
    }
}
