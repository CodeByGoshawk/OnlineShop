using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Aggregates.UserManagementAggregates;
using OnlineShop.Office.Application.Contracts.UserManagement;
using OnlineShop.Office.Application.Dtos.UserManagementDtos.UserDtos;
using OnlineShop.RepositoryDesignPattern.Contracts;
using PublicTools.Constants;
using PublicTools.Resources;
using PublicTools.Tools;
using ResponseFramewrok;

namespace OnlineShop.Office.Application.Services.UserManagementServices;
public class UserService(RoleManager<OnlineShopRole> roleManager, UserManager<OnlineShopUser> userManager) : IUserService
{
    private readonly RoleManager<OnlineShopRole> _roleManager = roleManager;
    private readonly UserManager<OnlineShopUser> _userManager = userManager;

    public async Task<IResponse<GetOnlineShopUserResultAppDto>> Get(GetOnlineShopUserAppDto model)
    {
        #region[Guards]
        if (model is null) return new Response<GetOnlineShopUserResultAppDto>(MessageResource.Error_NullInputModel);
        var selectedUser = await _userManager.FindByIdAsync(model.Id);
        if (selectedUser is null || selectedUser.IsSoftDeleted) return new Response<GetOnlineShopUserResultAppDto>(MessageResource.Error_UserNotFound);
        #endregion

        var getUserResultDto = new GetOnlineShopUserResultAppDto
        {
            FirstName = selectedUser.FirstName,
            LastName = selectedUser.LastName,
            UserName = selectedUser.UserName,
            PhoneNumber = selectedUser.PhoneNumber,
            PhoneNumberConfirmed = selectedUser.PhoneNumberConfirmed,
            Email = selectedUser.Email,
            EmailConfirmed = selectedUser.EmailConfirmed,
            CellPhone = selectedUser.CellPhone,
            IsCellPhoneConfirmed = selectedUser.IsCellPhoneConfirmed,
            NationalId = selectedUser.NationalId,
            IsNationalIdConfirmed = selectedUser.IsNationalIdConfirmed,
            IsActive = selectedUser.IsActive,
            Location = selectedUser.Location,
            Picture = selectedUser.Picture,
            TwoFactorEnabled = selectedUser.TwoFactorEnabled,
            CreatedDateGregorian = selectedUser.CreatedDateGregorian,
            CreatedDatePersian = selectedUser.CreatedDatePersian,
        };
        return new Response<GetOnlineShopUserResultAppDto>(getUserResultDto);
    }

    public async Task<IResponse<object>> Post(PostOnlineShopUserAppDto model)
    {
        #region[Guards]
        if (model is null) return new Response<object>(MessageResource.Error_NullInputModel);

        var existingUser = await _userManager.Users.SingleOrDefaultAsync(user => user.NormalizedUserName == model.UserName.ToUpper());
        if (existingUser is not null) return new Response<object>(MessageResource.Error_UserNameAlreadyExist);

        existingUser = await _userManager.Users.SingleOrDefaultAsync(user => user.NormalizedEmail == model.Email.ToUpper());
        if (existingUser is not null && !existingUser.IsSoftDeleted) return new Response<object>(MessageResource.Error_UserEmailAlreadyExist);

        existingUser = await _userManager.Users.SingleOrDefaultAsync(user => user.CellPhone == model.CellPhone);
        if (existingUser is not null && !existingUser.IsSoftDeleted) return new Response<object>(MessageResource.Error_UserCellPhoneAlreadyExist);

        existingUser = await _userManager.Users.SingleOrDefaultAsync(user => user.NationalId == model.NationalId);
        if (existingUser is not null && !existingUser.IsSoftDeleted)  return new Response<object>(MessageResource.Error_UserNationalIdAlreadyExist);
        #endregion

        var newUser = new OnlineShopUser
        {
            Id = Guid.NewGuid().ToString(),

            // Requireds
            UserName = model.UserName,
            NormalizedUserName = model.UserName.ToUpper(),
            FirstName = model.FirstName,
            LastName = model.LastName,
            NationalId = model.NationalId,
            CellPhone = model.CellPhone,
            Email = model.Email,
            NormalizedEmail = model.Email.ToUpper(),
            CreatedDateGregorian = DateTime.Now,
            CreatedDatePersian = DateTime.Now.ConvertToPersian(),

            // Nullables
            PhoneNumber = model.PhoneNumber,
            Picture = model.Picture,
            Location = model.Location,
        };

        var createUserResult = await _userManager.CreateAsync(newUser, model.Password);
        if (!createUserResult.Succeeded) return new Response<object>(string.Join(" ", createUserResult.Errors.Select(e => e.Description)));

        var defaultRole = await _roleManager.FindByIdAsync(DatabaseConstants.DefaultRoles.BuyerId);
        var addDefaultRoleToUserResult = await _userManager.AddToRoleAsync(newUser, defaultRole!.Name!);
        return addDefaultRoleToUserResult.Succeeded ? new Response<object>(model) : new Response<object>(string.Join(" ", createUserResult.Errors.Select(e => e.Description)));
    }

    public async Task<IResponse<object>> Put(PutOnlineShopUserAppDto model)
    {
        #region[Guards]
        if (model is null) return new Response<object>(MessageResource.Error_NullInputModel);

        var userToEdit = await _userManager.FindByIdAsync(model.Id);
        if (userToEdit is null || userToEdit.IsSoftDeleted) return new Response<object>(MessageResource.Error_UserNotFound);

        var existingUser = await _userManager.Users.SingleOrDefaultAsync(user => user.NormalizedEmail == model.Email.ToUpper());
        if (existingUser is not null && !existingUser.IsSoftDeleted && existingUser.Id != model.Id) return new Response<object>(MessageResource.Error_UserEmailAlreadyExist);

        existingUser = await _userManager.Users.SingleOrDefaultAsync(user => user.CellPhone == model.CellPhone.ToUpper());
        if (existingUser is not null && !existingUser.IsSoftDeleted && existingUser.Id != model.Id) return new Response<object>(MessageResource.Error_UserCellPhoneAlreadyExist);

        existingUser = await _userManager.Users.SingleOrDefaultAsync(user => user.NationalId == model.NationalId);
        if (existingUser is not null && !existingUser.IsSoftDeleted && existingUser.Id != model.Id) return new Response<object>(MessageResource.Error_UserNationalIdAlreadyExist);
        #endregion

        userToEdit.FirstName = model.FirstName;
        userToEdit.LastName = model.LastName;
        userToEdit.Picture = model.Picture;
        userToEdit.PhoneNumber = model.PhoneNumber;
        userToEdit.Location = model.Location;
        if (userToEdit.NationalId != model.NationalId) { userToEdit.NationalId = model.NationalId; userToEdit.IsNationalIdConfirmed = false; }
        if (userToEdit.CellPhone != model.CellPhone) { userToEdit.CellPhone = model.CellPhone; userToEdit.IsCellPhoneConfirmed = false; }
        if (userToEdit.Email != model.Email) { userToEdit.Email = model.Email; userToEdit.EmailConfirmed = false; }
        userToEdit.IsModified = true;
        userToEdit.ModifyDateGregorian = DateTime.Now;
        userToEdit.ModifyDatePersian = DateTime.Now.ConvertToPersian();

        var updateUserResult = await _userManager.UpdateAsync(userToEdit);
        return updateUserResult.Succeeded ? new Response<object>(model) : new Response<object>(string.Join(" ", updateUserResult.Errors.Select(e => e.Description)));
    }

    public async Task<IResponse<object>> ChangePassword(ChangePasswordAppDto model)
    {
        if (model is null) return new Response<object>(MessageResource.Error_NullInputModel);

        var userToChangePassword = await _userManager.FindByIdAsync(model.UserId);
        if (userToChangePassword is null || userToChangePassword.IsSoftDeleted) return new Response<object>(MessageResource.Error_UserNotFound);

        var changePasswordResult = await _userManager.ChangePasswordAsync(userToChangePassword, model.CurrentPassword, model.NewPassword);
        return changePasswordResult.Succeeded ? new Response<object>(model) : new Response<object>(string.Join(" ", changePasswordResult.Errors.Select(e => e.Description)));
    }

    public async Task<IResponse<object>> Delete(DeleteOnlineShopUserAppDto model)
    {
        #region[Guards]
        if (model is null) return new Response<object>(MessageResource.Error_NullInputModel);

        var userToDelete = await _userManager.FindByIdAsync(model.Id);
        if (userToDelete is null || userToDelete.IsSoftDeleted) return new Response<object>(MessageResource.Error_UserNotFound);
        #endregion

        userToDelete.IsSoftDeleted = true;
        userToDelete.SoftDeleteDateGregorian = DateTime.Now;
        userToDelete.SoftDeleteDatePersian = DateTime.Now.ConvertToPersian();

        var deleteUserResult = await _userManager.UpdateAsync(userToDelete);
        return deleteUserResult.Succeeded ? new Response<object>(model) : new Response<object>(string.Join(" ", deleteUserResult.Errors.Select(e => e.Description)));
    }
}