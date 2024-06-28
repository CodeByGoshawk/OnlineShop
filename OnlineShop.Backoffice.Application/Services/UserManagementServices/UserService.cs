using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Backoffice.Application.Contracts.UserManagement;
using OnlineShop.Backoffice.Application.Dtos.UserManagementDtos.UserDtos;
using OnlineShop.Domain.Aggregates.UserManagementAggregates;
using OnlineShop.RepositoryDesignPattern.Contracts;
using PublicTools.Constants;
using PublicTools.Resources;
using PublicTools.Tools;
using ResponseFramewrok;

namespace OnlineShop.Backoffice.Application.Services.UserManagementServices;
public class UserService(UserManager<OnlineShopUser> userManager, IOrderRepository orderRepository) : IUserService
{
    private readonly UserManager<OnlineShopUser> _userManager = userManager;
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<IResponse<GetUserResultAppDto>> Get(GetUserAppDto model)
    {
        #region[Guards]
        if (model is null) return new Response<GetUserResultAppDto>(MessageResource.Error_NullInputModel);

        var user = await _userManager.FindByIdAsync(model.GetterUserId!);

        #endregion

        var getUserResultDto = new GetUserResultAppDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            PhoneNumber = user.PhoneNumber,
            PhoneNumberConfirmed = user.PhoneNumberConfirmed,
            Email = user.Email,
            EmailConfirmed = user.EmailConfirmed,
            CellPhone = user.CellPhone,
            IsCellPhoneConfirmed = user.IsCellPhoneConfirmed,
            NationalId = user.NationalId,
            IsNationalIdConfirmed = user.IsNationalIdConfirmed,
            IsActive = user.IsActive,
            Location = user.Location,
            Picture = user.Picture,
            TwoFactorEnabled = user.TwoFactorEnabled,
            CreatedDateGregorian = user.CreatedDateGregorian,
            CreatedDatePersian = user.CreatedDatePersian,
        };
        return new Response<GetUserResultAppDto>(getUserResultDto);
    } // Seller

    public async Task<IResponse<GetUserWithPrivateDataResultAppDto>> GetWithPrivateData(GetUserAppDto model)
    {
        #region[Guards]
        if (model is null) return new Response<GetUserWithPrivateDataResultAppDto>(MessageResource.Error_NullInputModel);

        var getterUser = await _userManager.FindByIdAsync(model.GetterUserId!);
        var userToGet = await _userManager.FindByIdAsync(model.UserToGetId);

        var checkPermissionResponse = await CheckIfRequesterUserHasPermission(getterUser!, userToGet!);
        if (!checkPermissionResponse.IsSuccessful) return new Response<GetUserWithPrivateDataResultAppDto>(checkPermissionResponse.ErrorMessage!);
        if (userToGet is null) return new Response<GetUserWithPrivateDataResultAppDto>(MessageResource.Error_UserNotFound);
        #endregion

        var getUserResultDto = new GetUserWithPrivateDataResultAppDto
        {
            Id = userToGet.Id,
            FirstName = userToGet.FirstName,
            LastName = userToGet.LastName,
            UserName = userToGet.UserName,
            PhoneNumber = userToGet.PhoneNumber,
            PhoneNumberConfirmed = userToGet.PhoneNumberConfirmed,
            Email = userToGet.Email,
            EmailConfirmed = userToGet.EmailConfirmed,
            CellPhone = userToGet.CellPhone,
            IsCellPhoneConfirmed = userToGet.IsCellPhoneConfirmed,
            NationalId = userToGet.NationalId,
            IsNationalIdConfirmed = userToGet.IsNationalIdConfirmed,
            IsActive = userToGet.IsActive,
            Location = userToGet.Location,
            Picture = userToGet.Picture,
            TwoFactorEnabled = userToGet.TwoFactorEnabled,
            CreatedDateGregorian = userToGet.CreatedDateGregorian,
            CreatedDatePersian = userToGet.CreatedDatePersian,

            // Details
            AccessFailedCount = userToGet.AccessFailedCount,
            IsSoftDeleted = userToGet.IsSoftDeleted,
            SoftDeleteDateGregorian = userToGet.SoftDeleteDateGregorian,
            SoftDeleteDatePersian = userToGet.SoftDeleteDatePersian,
            IsModified = userToGet.IsModified,
            ModifyDateGregorian = userToGet.ModifyDateGregorian,
            ModifyDatePersian = userToGet.ModifyDatePersian,

            UserRoles = await _userManager.GetRolesAsync(userToGet)
        };
        return new Response<GetUserWithPrivateDataResultAppDto>(getUserResultDto);
    } // Admins

    public async Task<IResponse<GetAllUsersWithPrivateDataResultAppDto>> GetAllWithPrivateData(GetAllUsersAppDto model)
    {
        var result = new GetAllUsersWithPrivateDataResultAppDto();

        var getterUser = await _userManager.FindByIdAsync(model.GetterUserId);

        List<OnlineShopUser> outOfAccessUsers = [];
        outOfAccessUsers.AddRange(await _userManager.GetUsersInRoleAsync(DatabaseConstants.DefaultRoles.GodAdminName));

        if (!await _userManager.IsInRoleAsync(getterUser!, DatabaseConstants.DefaultRoles.GodAdminName))
        {
            outOfAccessUsers.AddRange(await _userManager.GetUsersInRoleAsync(DatabaseConstants.DefaultRoles.AdminName));
        }

        var usersToGet = (await _userManager.Users.ToListAsync()).Except(outOfAccessUsers).ToList();

        foreach(var user in usersToGet)
        {
            var getUserResultDto = new GetUserWithPrivateDataResultAppDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                EmailConfirmed = user.EmailConfirmed,
                IsCellPhoneConfirmed = user.IsCellPhoneConfirmed,
                IsNationalIdConfirmed = user.IsNationalIdConfirmed,
                IsActive = user.IsActive,
                Picture = user.Picture,
                CreatedDateGregorian = user.CreatedDateGregorian,
                CreatedDatePersian = user.CreatedDatePersian,
                IsModified = user.IsModified,
                IsSoftDeleted = user.IsSoftDeleted,

                UserRoles = await _userManager.GetRolesAsync(user)
            };
            result.GetResultDtos.Add(getUserResultDto);
        }
        return new Response<GetAllUsersWithPrivateDataResultAppDto>(result);
    } // Admins

    public async Task<IResponse> Register(RegisterUserAppDto model)
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
    } // Public

    public async Task<IResponse> Edit(EditUserAppDto model)
    {
        #region[Guards]
        if (model is null) return new Response(MessageResource.Error_NullInputModel);

        var editorUser = await _userManager.FindByIdAsync(model.EditorUserId!);
        var userToEdit = await _userManager.FindByIdAsync(model.UserToEditId);

        var checkPermissionResponse = await CheckIfRequesterUserHasPermission(editorUser!, userToEdit!);
        if (!checkPermissionResponse.IsSuccessful) return new Response(checkPermissionResponse.ErrorMessage!);

        var existingUser = await _userManager.Users.SingleOrDefaultAsync(user => user.NormalizedEmail == _userManager.NormalizeEmail(model.Email) && !user.IsSoftDeleted);
        if (existingUser is not null && existingUser.Id != model.UserToEditId) return new Response(MessageResource.Error_UserEmailAlreadyExist);

        existingUser = await _userManager.Users.SingleOrDefaultAsync(user => user.CellPhone == model.CellPhone.Trim() && !user.IsSoftDeleted);
        if (existingUser is not null && existingUser.Id != model.UserToEditId) return new Response(MessageResource.Error_UserCellPhoneAlreadyExist);

        existingUser = await _userManager.Users.SingleOrDefaultAsync(user => user.NationalId == model.NationalId.Trim() && !user.IsSoftDeleted);
        if (existingUser is not null && existingUser.Id != model.UserToEditId) return new Response(MessageResource.Error_UserNationalIdAlreadyExist);
        #endregion

        userToEdit.FirstName = model.FirstName.Trim();
        userToEdit.LastName = model.LastName.Trim();
        userToEdit.Picture = model.Picture?.Trim();
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
    } // Authorize Only

    public async Task<IResponse> ChangePassword(ChangePasswordAppDto model)
    {
        if (model is null) return new Response(MessageResource.Error_NullInputModel);

        var userToChangePassword = await _userManager.FindByIdAsync(model.UserId);
        if (userToChangePassword is null || userToChangePassword.IsSoftDeleted) return new Response(MessageResource.Error_UserNotFound);

        var changePasswordResult = await _userManager.ChangePasswordAsync(userToChangePassword, model.CurrentPassword, model.NewPassword);
        return changePasswordResult.Succeeded ? new Response(model) : new Response(string.Join(" ", changePasswordResult.Errors.Select(e => e.Description)));
    } // Self Only

    public async Task<IResponse> GrantOrRevokeAdminRole(GrantOrRevokeRoleAppDto model)
    {
        #region[Guards]
        if (model is null) return new Response(MessageResource.Error_NullInputModel);

        var userToGrantOrRevoke = await _userManager.FindByIdAsync(model.Id);
        if (userToGrantOrRevoke is null || userToGrantOrRevoke.IsSoftDeleted) return new Response(MessageResource.Error_UserNotFound);

        if (await _userManager.IsInRoleAsync(userToGrantOrRevoke, DatabaseConstants.DefaultRoles.GodAdminName))
            return new Response(MessageResource.Error_UserIsGodAdmin);
        #endregion

        var isAdmin = await _userManager.IsInRoleAsync(userToGrantOrRevoke, DatabaseConstants.DefaultRoles.AdminName);
        var isSeller = await _userManager.IsInRoleAsync(userToGrantOrRevoke, DatabaseConstants.DefaultRoles.SellerName);

        IdentityResult grantOrRevokeResult = new();
        if (isAdmin)
        {
            grantOrRevokeResult = await _userManager.RemoveFromRoleAsync(userToGrantOrRevoke, DatabaseConstants.DefaultRoles.AdminName);
        }
        else if (isSeller)
        {
            grantOrRevokeResult = await _userManager.RemoveFromRoleAsync(userToGrantOrRevoke, DatabaseConstants.DefaultRoles.SellerName);
            if (!grantOrRevokeResult.Succeeded) return new Response(string.Join(" ", grantOrRevokeResult.Errors.Select(e => e.Description)));

            grantOrRevokeResult = await _userManager.AddToRoleAsync(userToGrantOrRevoke, DatabaseConstants.DefaultRoles.AdminName);
        }
        else
        {
            grantOrRevokeResult = await _userManager.AddToRoleAsync(userToGrantOrRevoke, DatabaseConstants.DefaultRoles.AdminName);
        }

        return grantOrRevokeResult.Succeeded ? new Response(model) : new Response(string.Join(" ", grantOrRevokeResult.Errors.Select(e => e.Description)));
    } // GodAdmin

    public async Task<IResponse> GrantOrRevokeSellerRole(GrantOrRevokeRoleAppDto model)
    {
        #region[Guards]
        if (model is null) return new Response(MessageResource.Error_NullInputModel);

        var userToGrantOrRevoke = await _userManager.FindByIdAsync(model.Id);
        if (userToGrantOrRevoke is null || userToGrantOrRevoke.IsSoftDeleted) return new Response(MessageResource.Error_UserNotFound);

        if ((await IsAnyTypeOfAdminsAsync(userToGrantOrRevoke)).IsSuccessful)
            return new Response(MessageResource.Error_AdminsCanNotBeSellers);
        #endregion

        var isSeller = await _userManager.IsInRoleAsync(userToGrantOrRevoke, DatabaseConstants.DefaultRoles.SellerName);

        IdentityResult grantOrRevokeResult = new();

        if (isSeller)
            grantOrRevokeResult = await _userManager.RemoveFromRoleAsync(userToGrantOrRevoke, DatabaseConstants.DefaultRoles.SellerName);
        else
            grantOrRevokeResult = await _userManager.AddToRoleAsync(userToGrantOrRevoke, DatabaseConstants.DefaultRoles.SellerName);

        return grantOrRevokeResult.Succeeded ? new Response(model) : new Response(string.Join(" ", grantOrRevokeResult.Errors.Select(e => e.Description)));
    } // Admins

    public async Task<IResponse> Delete(DeleteUserAppDto model)
    {
        #region[Guards]
        if (model is null) return new Response(MessageResource.Error_NullInputModel);

        var deleterUser = await _userManager.FindByIdAsync(model.DeleterUserId!);
        var userToDelete = await _userManager.FindByIdAsync(model.UserToDeleteId);

        var checkPermissionResponse = await CheckIfRequesterUserHasPermission(deleterUser!, userToDelete!);
        if (!checkPermissionResponse.IsSuccessful) return new Response(checkPermissionResponse.ErrorMessage!);

        if (await _userManager.IsInRoleAsync(userToDelete!, DatabaseConstants.DefaultRoles.GodAdminName))
            return new Response(MessageResource.Error_UserIsGodAdmin);

        if ((await _orderRepository.SelectNonDeletedsBySellerAsync(model.UserToDeleteId)).ResultModel!.Any())
            return new Response(MessageResource.Error_UserIsSellerWithOrder);
        #endregion

        userToDelete.IsSoftDeleted = true;
        userToDelete.SoftDeleteDateGregorian = DateTime.Now;
        userToDelete.SoftDeleteDatePersian = DateTime.Now.ConvertToPersian();

        var deleteUserResult = await _userManager.UpdateAsync(userToDelete);
        return deleteUserResult.Succeeded ? new Response(model) : new Response(string.Join(" ", deleteUserResult.Errors.Select(e => e.Description)));
    } // GodAdmin



    //--------------------------------------------[Private Methods]--------------------------------------------//


    private async Task<IResponse> CheckIfRequesterUserHasPermission(OnlineShopUser requesterUser, OnlineShopUser requestedUser)
    {
        if (requesterUser != requestedUser)
        {
            if (!(await IsAnyTypeOfAdminsAsync(requesterUser!)).IsSuccessful)
                return new Response(MessageResource.Error_UnauthorizedOwner);

            if (requestedUser is null || requestedUser.IsSoftDeleted)
                return new Response(MessageResource.Error_UserNotFound);

            if (await _userManager.IsInRoleAsync(requestedUser, DatabaseConstants.DefaultRoles.GodAdminName))
                return new Response(MessageResource.Error_NoAccessToAdminUsers);

            if (await _userManager.IsInRoleAsync(requestedUser!, DatabaseConstants.DefaultRoles.AdminName) &&
                !await _userManager.IsInRoleAsync(requesterUser!, DatabaseConstants.DefaultRoles.GodAdminName))
                return new Response(MessageResource.Error_NoAccessToAdminUsers);
        }
        return requestedUser.IsSoftDeleted ? new Response(MessageResource.Error_UserNotFound) : new Response(true);
    }

    private async Task<IResponse> IsAnyTypeOfAdminsAsync(OnlineShopUser user)
    {
        List<string> adminRoles =
        [
            DatabaseConstants.DefaultRoles.GodAdminName,
            DatabaseConstants.DefaultRoles.AdminName
        ];
        var userRoles = await _userManager.GetRolesAsync(user);
        return new Response(userRoles.Any(role => adminRoles.Contains(role)));
    }


    //---------------------------------------------------------------------------------------------------------//


    // (Replace By Filtering On GetNonAdminsWithPrivateData)
    public async Task<IResponse<GetAllUsersWithPrivateDataResultAppDto>> GetSellers()
    {
        var result = new GetAllUsersWithPrivateDataResultAppDto();

        IQueryable<OnlineShopUser> sellers = (IQueryable<OnlineShopUser>)await _userManager.GetUsersInRoleAsync(DatabaseConstants.DefaultRoles.SellerName);

        await sellers.ForEachAsync(user =>
        {
            var getUserResultDto = new GetUserWithPrivateDataResultAppDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                CellPhone = user.CellPhone,
                IsCellPhoneConfirmed = user.IsCellPhoneConfirmed,
                NationalId = user.NationalId,
                IsNationalIdConfirmed = user.IsNationalIdConfirmed,
                IsActive = user.IsActive,
                Location = user.Location,
                Picture = user.Picture,
                TwoFactorEnabled = user.TwoFactorEnabled,
                AccessFailedCount = user.AccessFailedCount,
                CreatedDateGregorian = user.CreatedDateGregorian,
                CreatedDatePersian = user.CreatedDatePersian,
                IsModified = user.IsModified,
                ModifyDateGregorian = user.ModifyDateGregorian,
                ModifyDatePersian = user.ModifyDatePersian,
                IsSoftDeleted = user.IsSoftDeleted,
                SoftDeleteDateGregorian = user.SoftDeleteDateGregorian,
                SoftDeleteDatePersian = user.SoftDeleteDatePersian
            };
            result.GetResultDtos.Add(getUserResultDto);
        });
        return new Response<GetAllUsersWithPrivateDataResultAppDto>(result);
    } // Admin
}