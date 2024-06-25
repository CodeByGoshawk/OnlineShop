using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Aggregates.UserManagementAggregates;
using OnlineShop.Office.Application.Contracts.UserManagement;
using OnlineShop.Office.Application.Dtos.UserManagementDtos.UserDtos;
using PublicTools.Constants;
using PublicTools.Resources;
using PublicTools.Tools;
using ResponseFramewrok;

namespace OnlineShop.Office.Application.Services.UserManagementServices;
public class UserService(UserManager<OnlineShopUser> userManager) : IUserService
{
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

    public async Task<IResponse> Post(PostOnlineShopUserAppDto model)
    {
        #region[Guards]
        if (model is null) return new Response(MessageResource.Error_NullInputModel);

        var existingUser = await _userManager.Users.SingleOrDefaultAsync(user => user.NormalizedUserName == _userManager.NormalizeName(model.UserName));
        if (existingUser is not null) return new Response(MessageResource.Error_UserNameAlreadyExist);

        existingUser = await _userManager.Users.SingleOrDefaultAsync(user => user.NormalizedEmail == _userManager.NormalizeEmail(model.Email) && !user.IsSoftDeleted);
        if (existingUser is not null) return new Response(MessageResource.Error_UserEmailAlreadyExist);

        existingUser = await _userManager.Users.SingleOrDefaultAsync(user => user.CellPhone == model.CellPhone.Trim() && !user.IsSoftDeleted);
        if (existingUser is not null) return new Response(MessageResource.Error_UserCellPhoneAlreadyExist);

        existingUser = await _userManager.Users.SingleOrDefaultAsync(user => user.NationalId == model.NationalId.Trim() && !user.IsSoftDeleted);
        if (existingUser is not null) return new Response(MessageResource.Error_UserNationalIdAlreadyExist);
        #endregion

        var newUser = new OnlineShopUser
        {
            Id = Guid.NewGuid().ToString(),

            // Requireds
            UserName = model.UserName.Trim(),
            NormalizedUserName = _userManager.NormalizeName(model.UserName),
            FirstName = model.FirstName.Trim(),
            LastName = model.LastName.Trim(),
            NationalId = model.NationalId.Trim(),
            CellPhone = model.CellPhone.Trim(),
            Email = model.Email.Trim(),
            NormalizedEmail = _userManager.NormalizeEmail(model.Email),
            CreatedDateGregorian = DateTime.Now,
            CreatedDatePersian = DateTime.Now.ConvertToPersian(),

            // Nullables
            PhoneNumber = model.PhoneNumber?.Trim(),
            Picture = model.Picture?.Trim(),
            Location = model.Location?.Trim(),
        };

        var createUserResult = await _userManager.CreateAsync(newUser, model.Password);
        if (!createUserResult.Succeeded) return new Response(string.Join(" ", createUserResult.Errors.Select(e => e.Description)));

        var addDefaultRoleToUserResult = await _userManager.AddToRoleAsync(newUser, DatabaseConstants.DefaultRoles.BuyerName);
        return addDefaultRoleToUserResult.Succeeded ? new Response(model) : new Response(string.Join(" ", createUserResult.Errors.Select(e => e.Description)));
    }

    public async Task<IResponse> Put(PutOnlineShopUserAppDto model)
    {
        #region[Guards]
        if (model is null) return new Response(MessageResource.Error_NullInputModel);

        var userToEdit = await _userManager.FindByIdAsync(model.Id!);
        if (userToEdit is null || userToEdit.IsSoftDeleted) return new Response(MessageResource.Error_UserNotFound);

        var existingUser = await _userManager.Users.SingleOrDefaultAsync(user => user.NormalizedEmail == _userManager.NormalizeEmail(model.Email) && !user.IsSoftDeleted);
        if (existingUser is not null && existingUser.Id != model.Id) return new Response(MessageResource.Error_UserEmailAlreadyExist);

        existingUser = await _userManager.Users.SingleOrDefaultAsync(user => user.CellPhone == model.CellPhone.Trim() && !user.IsSoftDeleted);
        if (existingUser is not null && existingUser.Id != model.Id) return new Response(MessageResource.Error_UserCellPhoneAlreadyExist);

        existingUser = await _userManager.Users.SingleOrDefaultAsync(user => user.NationalId == model.NationalId.Trim() && !user.IsSoftDeleted);
        if (existingUser is not null && existingUser.Id != model.Id) return new Response(MessageResource.Error_UserNationalIdAlreadyExist);
        #endregion

        userToEdit.FirstName = model.FirstName.Trim();
        userToEdit.LastName = model.LastName.Trim();
        userToEdit.Picture = model.Picture;
        userToEdit.PhoneNumber = model.PhoneNumber?.Trim();
        userToEdit.Location = model.Location;

        if (userToEdit.NationalId != model.NationalId)
        {
            userToEdit.NationalId = model.NationalId.Trim();
            userToEdit.IsNationalIdConfirmed = false;
        }
        if (userToEdit.CellPhone != model.CellPhone)
        {
            userToEdit.CellPhone = model.CellPhone.Trim();
            userToEdit.IsCellPhoneConfirmed = false;
        }
        if (userToEdit.Email != model.Email)
        {
            userToEdit.Email = model.Email.Trim();
            userToEdit.EmailConfirmed = false;
            userToEdit.NormalizedEmail = _userManager.NormalizeEmail(model.Email);
        }

        userToEdit.IsModified = true;
        userToEdit.ModifyDateGregorian = DateTime.Now;
        userToEdit.ModifyDatePersian = DateTime.Now.ConvertToPersian();

        var updateUserResult = await _userManager.UpdateAsync(userToEdit);
        return updateUserResult.Succeeded ? new Response(model) : new Response(string.Join(" ", updateUserResult.Errors.Select(e => e.Description)));
    }

    public async Task<IResponse> ChangePassword(ChangePasswordAppDto model)
    {
        if (model is null) return new Response(MessageResource.Error_NullInputModel);

        var userToChangePassword = await _userManager.FindByIdAsync(model.UserId);
        if (userToChangePassword is null || userToChangePassword.IsSoftDeleted) return new Response(MessageResource.Error_UserNotFound);

        var changePasswordResult = await _userManager.ChangePasswordAsync(userToChangePassword, model.CurrentPassword, model.NewPassword);
        return changePasswordResult.Succeeded ? new Response(model) : new Response(string.Join(" ", changePasswordResult.Errors.Select(e => e.Description)));
    }

    public async Task<IResponse> Delete(DeleteOnlineShopUserAppDto model)
    {
        #region[Guards]
        if (model is null) return new Response(MessageResource.Error_NullInputModel);

        var userToDelete = await _userManager.FindByIdAsync(model.Id);
        if (userToDelete is null || userToDelete.IsSoftDeleted) return new Response(MessageResource.Error_UserNotFound);
        if (await _userManager.IsInRoleAsync(userToDelete, DatabaseConstants.DefaultRoles.GodAdminName)) return new Response(MessageResource.Error_UserIsGodAdmin);
        #endregion

        userToDelete.IsSoftDeleted = true;
        userToDelete.SoftDeleteDateGregorian = DateTime.Now;
        userToDelete.SoftDeleteDatePersian = DateTime.Now.ConvertToPersian();

        var deleteUserResult = await _userManager.UpdateAsync(userToDelete);
        return deleteUserResult.Succeeded ? new Response(model) : new Response(string.Join(" ", deleteUserResult.Errors.Select(e => e.Description)));
    }
}