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
public class UserService(RoleManager<OnlineShopRole> roleManager, UserManager<OnlineShopUser> userManager, IOrderRepository orderRepository) : IUserService
{
    private readonly RoleManager<OnlineShopRole> _roleManager = roleManager;
    private readonly UserManager<OnlineShopUser> _userManager = userManager;
    private readonly IOrderRepository _orderRepository = orderRepository;

    // Get
    public async Task<IResponse<GetOnlineShopUserResultAppDto>> GetWithPrivateData(GetOnlineShopUserAppDto model)
    {
        #region[Guards]
        if (model is null) return new Response<GetOnlineShopUserResultAppDto>(MessageResource.Error_NullInputModel);
        var selectedUser = await _userManager.FindByIdAsync(model.Id);
        if (selectedUser is null) return new Response<GetOnlineShopUserResultAppDto>(MessageResource.Error_UserNotFound);
        #endregion

        var getUserResultDto = new GetOnlineShopUserResultAppDto
        {
            Id = selectedUser.Id,
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

            // Details
            AccessFailedCount = selectedUser.AccessFailedCount,
            IsSoftDeleted = selectedUser.IsSoftDeleted,
            SoftDeleteDateGregorian = selectedUser.SoftDeleteDateGregorian,
            SoftDeleteDatePersian = selectedUser.SoftDeleteDatePersian,
            IsModified = selectedUser.IsModified,
            ModifyDateGregorian = selectedUser.ModifyDateGregorian,
            ModifyDatePersian = selectedUser.ModifyDatePersian,

        };
        return new Response<GetOnlineShopUserResultAppDto>(getUserResultDto);
    } // GodAdmin

    public async Task<IResponse<GetOnlineShopUserResultAppDto>> GetNonAdminWithPrivateData(GetOnlineShopUserAppDto model)
    {
        #region[Guards]
        if (model is null) return new Response<GetOnlineShopUserResultAppDto>(MessageResource.Error_NullInputModel);
        var selectedUser = await _userManager.FindByIdAsync(model.Id);
        if (selectedUser is null) return new Response<GetOnlineShopUserResultAppDto>(MessageResource.Error_UserNotFound);
        var selectedUserRoles = await _userManager.GetRolesAsync(selectedUser);
        if (selectedUserRoles.Contains(DatabaseConstants.DefaultRoles.GodAdminName) || selectedUserRoles.Contains(DatabaseConstants.DefaultRoles.AdminName))
            return new Response<GetOnlineShopUserResultAppDto>(MessageResource.Error_NoPermissionToGetAdmins);
        #endregion

        var getUserResultDto = new GetOnlineShopUserResultAppDto
        {
            Id = selectedUser.Id,
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

            // Details
            AccessFailedCount = selectedUser.AccessFailedCount,
            IsSoftDeleted = selectedUser.IsSoftDeleted,
            SoftDeleteDateGregorian = selectedUser.SoftDeleteDateGregorian,
            SoftDeleteDatePersian = selectedUser.SoftDeleteDatePersian,
            IsModified = selectedUser.IsModified,
            ModifyDateGregorian = selectedUser.ModifyDateGregorian,
            ModifyDatePersian = selectedUser.ModifyDatePersian,

        };
        return new Response<GetOnlineShopUserResultAppDto>(getUserResultDto);
    } // Admin

    public async Task<IResponse<GetOnlineShopUserResultAppDto>> Get(GetOnlineShopUserAppDto model)
    {
        #region[Guards]
        if (model is null) return new Response<GetOnlineShopUserResultAppDto>(MessageResource.Error_NullInputModel);
        var selectedUser = await _userManager.FindByIdAsync(model.Id);
        if (selectedUser is null) return new Response<GetOnlineShopUserResultAppDto>(MessageResource.Error_UserNotFound);
        #endregion

        var getUserResultDto = new GetOnlineShopUserResultAppDto
        {
            Id = selectedUser.Id,
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
    } // Seller

    public async Task<IResponse<GetAllOnlineShopUsersResultAppDto>> GetAllWithPrivateData()
    {
        var result = new GetAllOnlineShopUsersResultAppDto();
        await _userManager.Users.ForEachAsync(user =>
        {
            var getUserResultDto = new GetOnlineShopUserResultAppDto
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
        return new Response<GetAllOnlineShopUsersResultAppDto>(result);
    } // GodAdmin

    public async Task<IResponse<GetAllOnlineShopUsersResultAppDto>> GetNonAdminsWithPrivateData()
    {
        var result = new GetAllOnlineShopUsersResultAppDto();

        List<OnlineShopUser> adminUsers = [];
        adminUsers.AddRange(_userManager.GetUsersInRoleAsync(DatabaseConstants.DefaultRoles.GodAdminName).Result);
        adminUsers.AddRange(_userManager.GetUsersInRoleAsync(DatabaseConstants.DefaultRoles.AdminName).Result);
        var nonAdminUsers = _userManager.Users.Except(adminUsers);

        await nonAdminUsers.ForEachAsync(async user =>
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var getUserResultDto = new GetOnlineShopUserResultAppDto
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
                SoftDeleteDatePersian = user.SoftDeleteDatePersian,
                UserRoles = userRoles
            };
            result.GetResultDtos.Add(getUserResultDto);
        });
        return new Response<GetAllOnlineShopUsersResultAppDto>(result);
    } // Admin

    // (Replace By Filtering On GetNonAdminsWithPrivateData)
    public async Task<IResponse<GetAllOnlineShopUsersResultAppDto>> GetSellers()
    {
        var result = new GetAllOnlineShopUsersResultAppDto();

        IQueryable<OnlineShopUser> sellers = (IQueryable<OnlineShopUser>)await _userManager.GetUsersInRoleAsync(DatabaseConstants.DefaultRoles.SellerName);

        await sellers.ForEachAsync(user =>
        {
            var getUserResultDto = new GetOnlineShopUserResultAppDto
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
        return new Response<GetAllOnlineShopUsersResultAppDto>(result);
    } // Admin

    // Post
    public async Task<IResponse<object>> Register(RegisterOnlineShopUserAppDto model)
    {
        #region[Guards]
        if (model is null) return new Response<object>(MessageResource.Error_NullInputModel);

        if (_userManager.Users.SingleOrDefaultAsync(user => user.NormalizedUserName == model.UserName.ToUpper()).Result is not null)
            return new Response<object>(MessageResource.Error_UserNameAlreadyExist);

        if (_userManager.Users.SingleOrDefaultAsync(user => user.NormalizedEmail == model.Email.ToUpper()).Result is not null)
            return new Response<object>(MessageResource.Error_UserEmailAlreadyExist);

        if (_userManager.Users.SingleOrDefaultAsync(user => user.CellPhone == model.CellPhone).Result is not null)
            return new Response<object>(MessageResource.Error_UserCellPhoneAlreadyExist);

        if (_userManager.Users.SingleOrDefaultAsync(user => user.NationalId == model.NationalId).Result is not null)
            return new Response<object>(MessageResource.Error_UserNationalIdAlreadyExist);
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

        var defaultRole = await _roleManager.FindByIdAsync(DatabaseConstants.DefaultRoles.SellerId);
        var addDefaultRoleToUserResult = await _userManager.AddToRoleAsync(newUser, defaultRole!.Name!);
        return addDefaultRoleToUserResult.Succeeded ? new Response<object>(model) : new Response<object>(string.Join(" ", createUserResult.Errors.Select(e => e.Description)));
    } // Admin

    // Put
    public async Task<IResponse<object>> EditSelf(EditOnlineShopUserPropertiesAppDto model)
    {
        #region[Guards]
        if (model is null) return new Response<object>(MessageResource.Error_NullInputModel);

        var userToEdit = await _userManager.FindByIdAsync(model.Id);
        if (userToEdit is null) return new Response<object>(MessageResource.Error_UserNotFound);

        var existingEmail = await _userManager.Users.SingleOrDefaultAsync(user => user.NormalizedEmail == model.Email.ToUpper());
        if (existingEmail is not null && existingEmail.Id != model.Id) return new Response<object>(MessageResource.Error_UserEmailAlreadyExist);

        var existingCellPhone = await _userManager.Users.SingleOrDefaultAsync(user => user.CellPhone == model.CellPhone.ToUpper());
        if (existingCellPhone is not null && existingCellPhone.Id != model.Id) return new Response<object>(MessageResource.Error_UserCellPhoneAlreadyExist);

        var existingNationalId = await _userManager.Users.SingleOrDefaultAsync(user => user.NationalId == model.NationalId);
        if (existingNationalId is not null && existingNationalId.Id != model.Id) return new Response<object>(MessageResource.Error_UserNationalIdAlreadyExist);
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
    } // GodAdmin/Admin/Seller

    public async Task<IResponse<object>> EditNonGodAdmin(EditOnlineShopUserPropertiesAppDto model)
    {
        #region[Guards]
        if (model is null) return new Response<object>(MessageResource.Error_NullInputModel);

        var userToEdit = await _userManager.FindByIdAsync(model.Id);
        if (userToEdit is null) return new Response<object>(MessageResource.Error_UserNotFound);
        if (_userManager.IsInRoleAsync(userToEdit, DatabaseConstants.DefaultRoles.GodAdminName).Result)
            return new Response<object>(MessageResource.Error_NoPermissionToEditAdmins);


        var existingEmail = await _userManager.Users.SingleOrDefaultAsync(user => user.NormalizedEmail == model.Email.ToUpper());
        if (existingEmail is not null && existingEmail.Id != model.Id) return new Response<object>(MessageResource.Error_UserEmailAlreadyExist);

        var existingCellPhone = await _userManager.Users.SingleOrDefaultAsync(user => user.CellPhone == model.CellPhone.ToUpper());
        if (existingCellPhone is not null && existingCellPhone.Id != model.Id) return new Response<object>(MessageResource.Error_UserCellPhoneAlreadyExist);

        var existingNationalId = await _userManager.Users.SingleOrDefaultAsync(user => user.NationalId == model.NationalId);
        if (existingNationalId is not null && existingNationalId.Id != model.Id) return new Response<object>(MessageResource.Error_UserNationalIdAlreadyExist);
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
    } // GodAdmin

    public async Task<IResponse<object>> EditNonAdmin(EditOnlineShopUserPropertiesAppDto model)
    {
        #region[Guards]
        if (model is null) return new Response<object>(MessageResource.Error_NullInputModel);

        var userToEdit = await _userManager.FindByIdAsync(model.Id);
        if (userToEdit is null) return new Response<object>(MessageResource.Error_UserNotFound);
        if (_userManager.IsInRoleAsync(userToEdit, DatabaseConstants.DefaultRoles.GodAdminName).Result ||
                    _userManager.IsInRoleAsync(userToEdit, DatabaseConstants.DefaultRoles.AdminName).Result)
            return new Response<object>(MessageResource.Error_NoPermissionToEditAdmins);


        var existingEmail = await _userManager.Users.SingleOrDefaultAsync(user => user.NormalizedEmail == model.Email.ToUpper());
        if (existingEmail is not null && existingEmail.Id != model.Id) return new Response<object>(MessageResource.Error_UserEmailAlreadyExist);

        var existingCellPhone = await _userManager.Users.SingleOrDefaultAsync(user => user.CellPhone == model.CellPhone.ToUpper());
        if (existingCellPhone is not null && existingCellPhone.Id != model.Id) return new Response<object>(MessageResource.Error_UserCellPhoneAlreadyExist);

        var existingNationalId = await _userManager.Users.SingleOrDefaultAsync(user => user.NationalId == model.NationalId);
        if (existingNationalId is not null && existingNationalId.Id != model.Id) return new Response<object>(MessageResource.Error_UserNationalIdAlreadyExist);
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
    } // Admin

    public async Task<IResponse<object>> ChangePassword(ChangePasswordAppDto model)
    {
        if (model is null) return new Response<object>(MessageResource.Error_NullInputModel);

        var userToChangePassword = await _userManager.FindByIdAsync(model.UserId);
        if (userToChangePassword is null) return new Response<object>(MessageResource.Error_UserNotFound);

        var changePasswordResult = await _userManager.ChangePasswordAsync(userToChangePassword,model.CurrentPassword,model.NewPassword);
        return changePasswordResult.Succeeded ? new Response<object>(model) : new Response<object>(string.Join(" ", changePasswordResult.Errors.Select(e => e.Description)));
    }

    // Delete
    public async Task<IResponse<object>> DeleteNonGodAdmin(DeleteOnlineShopUserAppDto model)
    {
        #region[Guards]
        if (model is null) return new Response<object>(MessageResource.Error_NullInputModel);

        var userToDelete = await _userManager.FindByIdAsync(model.Id);
        if (userToDelete is null) return new Response<object>(MessageResource.Error_UserNotFound);

        if (_userManager.IsInRoleAsync(userToDelete, DatabaseConstants.DefaultRoles.GodAdminName).Result)
            return new Response<object>(MessageResource.Error_NoPermissionToEditAdmins);

        if (_orderRepository.SelectRangeBySellerAsync(model.Id).Result.ResultModel!.Any()) return new Response<object>(MessageResource.Error_UserIsSellerWithOrder);
        #endregion

        userToDelete.IsSoftDeleted = true;
        userToDelete.SoftDeleteDateGregorian = DateTime.Now;
        userToDelete.SoftDeleteDatePersian = DateTime.Now.ConvertToPersian();

        var deleteUserResult = await _userManager.UpdateAsync(userToDelete);
        return deleteUserResult.Succeeded ? new Response<object>(model) : new Response<object>(string.Join(" ", deleteUserResult.Errors.Select(e => e.Description)));
    } // GodAdmin

    public async Task<IResponse<object>> DeleteNonAdmin(DeleteOnlineShopUserAppDto model)
    {
        #region[Guards]
        if (model is null) return new Response<object>(MessageResource.Error_NullInputModel);

        var userToDelete = await _userManager.FindByIdAsync(model.Id);
        if (userToDelete is null) return new Response<object>(MessageResource.Error_UserNotFound);

        if (_userManager.IsInRoleAsync(userToDelete, DatabaseConstants.DefaultRoles.GodAdminName).Result ||
            _userManager.IsInRoleAsync(userToDelete, DatabaseConstants.DefaultRoles.AdminName).Result)
            return new Response<object>(MessageResource.Error_NoPermissionToEditAdmins);

        if (_orderRepository.SelectRangeBySellerAsync(model.Id).Result.ResultModel!.Any()) return new Response<object>(MessageResource.Error_UserIsSellerWithOrder);
        #endregion

        userToDelete.IsSoftDeleted = true;
        userToDelete.SoftDeleteDateGregorian = DateTime.Now;
        userToDelete.SoftDeleteDatePersian = DateTime.Now.ConvertToPersian();

        var deleteUserResult = await _userManager.UpdateAsync(userToDelete);
        return deleteUserResult.Succeeded ? new Response<object>(model) : new Response<object>(string.Join(" ", deleteUserResult.Errors.Select(e => e.Description)));
    } // Admin/Seller

    public async Task<IResponse<object>> GrantOrRevokeAdminRole(GrantOrRevokeAdminRoleAppDto model)
    {
        #region[Guards]
        if (model is null) return new Response<object>(MessageResource.Error_NullInputModel);

        var userToGrantOrRevoke = await _userManager.FindByIdAsync(model.Id);
        if (userToGrantOrRevoke is null) return new Response<object>(MessageResource.Error_UserNotFound);

        if (_userManager.IsInRoleAsync(userToGrantOrRevoke, DatabaseConstants.DefaultRoles.GodAdminName).Result)
            return new Response<object>(MessageResource.Error_UserIsGodAdmin);
        #endregion

        var isAdmin = await _userManager.IsInRoleAsync(userToGrantOrRevoke, DatabaseConstants.DefaultRoles.AdminName);
        if (isAdmin)
        {
            var removeFromRoleResult = await _userManager.RemoveFromRoleAsync(userToGrantOrRevoke, DatabaseConstants.DefaultRoles.AdminName);
            return removeFromRoleResult.Succeeded ? new Response<object>(model) : new Response<object>(string.Join(" ", removeFromRoleResult.Errors.Select(e => e.Description)));
        }

        var addToRoleResult = await _userManager.AddToRoleAsync(userToGrantOrRevoke, DatabaseConstants.DefaultRoles.AdminName);
        return addToRoleResult.Succeeded ? new Response<object>(model) : new Response<object>(string.Join(" ", addToRoleResult.Errors.Select(e => e.Description)));
    }


    public async Task<IResponse<GetOnlineShopUserResultAppDto>> Authorize(string id)
    {
        var selectedUser = await _userManager.FindByIdAsync(id);
        if (selectedUser is null) return new Response<GetOnlineShopUserResultAppDto>(MessageResource.Error_UserNotFound);

        var getUserResultDto = new GetOnlineShopUserResultAppDto
        {
            Id = selectedUser.Id,
        };
        return new Response<GetOnlineShopUserResultAppDto>(getUserResultDto);
    }
}