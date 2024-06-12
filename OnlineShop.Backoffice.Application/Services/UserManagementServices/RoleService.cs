using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Backoffice.Application.Contracts.UserManagement;
using OnlineShop.Backoffice.Application.Dtos.UserManagementDtos.RoleDtos;
using OnlineShop.Backoffice.Application.Dtos.UserManagementDtos.UserDtos;
using OnlineShop.Domain.Aggregates.UserManagementAggregates;
using PublicTools.Resources;
using ResponseFramewrok;

namespace OnlineShop.Backoffice.Application.Services.UserManagementServices;
public class RoleService(RoleManager<OnlineShopRole> roleManager, UserManager<OnlineShopUser> userManager) : IRoleService
{
    private readonly RoleManager<OnlineShopRole> _roleManager = roleManager;
    private readonly UserManager<OnlineShopUser> _userManager = userManager;

    // Get
    public async Task<IResponse<GetOnlineShopRoleResultAppDto>> Get(GetOnlineShopRoleAppDto model)
    {
        var selectedRole = await _roleManager.FindByIdAsync(model.Id);
        if (selectedRole is null) return new Response<GetOnlineShopRoleResultAppDto>(MessageResource.Error_RoleNotFound);

        var getRoleResultDto = new GetOnlineShopRoleResultAppDto
        {
            Id = selectedRole.Id,
            Name = selectedRole.Name,
            NormalizedName = selectedRole.NormalizedName,
            ConcurrencyStamp = selectedRole.ConcurrencyStamp
        };
        return new Response<GetOnlineShopRoleResultAppDto>(getRoleResultDto);
    }

    public async Task<IResponse<GetAllOnlineShopRolesResultAppDto>> GetAll()
    {
        var result = new GetAllOnlineShopRolesResultAppDto();
        await _roleManager.Roles.ForEachAsync(role =>
        {
            var getRoleResultDto = new GetOnlineShopRoleResultAppDto
            {
                Id = role.Id,
                Name = role.Name,
                NormalizedName = role.NormalizedName,
                ConcurrencyStamp = role.ConcurrencyStamp
            };
            result.GetResultDtos.Add(getRoleResultDto);
        });

        return new Response<GetAllOnlineShopRolesResultAppDto>(result);
    }

    // Post
    public async Task<IResponse<object>> Register(RegisterOnlineShopRoleAppDto model)
    {
        if (model is null) return new Response<object>(MessageResource.Error_NullInputModel);
        if (model.Name is null) return new Response<object>(MessageResource.Error_RequiredField);
        if (_roleManager.Roles.SingleOrDefaultAsync(r => r.NormalizedName == model.Name.ToUpper()).Result is not null) return new Response<object>(MessageResource.Error_RoleNameAlreadyExist);

        var newRole = new OnlineShopRole
        {
            Name = model.Name,
            NormalizedName = model.Name.ToUpper(),
        };

        var createRoleResult = await _roleManager.CreateAsync(newRole);
        return createRoleResult.Succeeded ? new Response<object>(model) : new Response<object>(string.Join(" ", createRoleResult.Errors.Select(e => e.Description)));
    }

    // Put
    public async Task<IResponse<object>> Edit(EditOnlineShopRoleAppDto model)
    {
        if (model is null) return new Response<object>(MessageResource.Error_NullInputModel);
        if (model.Name is null) return new Response<object>(MessageResource.Error_RequiredField);

        var roleToEdit = await _roleManager.FindByIdAsync(model.Id);
        if (roleToEdit is null) return new Response<object>(MessageResource.Error_RoleNotFound);

        var existingRoleName = await _roleManager.Roles.SingleOrDefaultAsync(r => r.NormalizedName == model.Name.ToUpper());
        if (existingRoleName is not null && existingRoleName.Id != model.Id) return new Response<object>(MessageResource.Error_RoleNameAlreadyExist);

        roleToEdit.Name = model.Name;
        roleToEdit.NormalizedName = model.Name.ToUpper();

        var updateRoleResult = await _roleManager.UpdateAsync(roleToEdit);
        return updateRoleResult.Succeeded ? new Response<object>(model) : new Response<object>(string.Join(" ", updateRoleResult.Errors.Select(e => e.Description)));
    }

    // Delete
    public async Task<IResponse<object>> Delete(DeleteOnlineShopUserAppDto model)
    {
        if (model is null) return new Response<object>(MessageResource.Error_NullInputModel);

        var roleToDelete = await _roleManager.FindByIdAsync(model.Id);
        if (roleToDelete is null) return new Response<object>(MessageResource.Error_RoleNotFound);

        if (_userManager.GetUsersInRoleAsync(roleToDelete.Name!).Result.Any()) return new Response<object>(MessageResource.Error_RoleIsGranted);

        var deleteRoleResult = await _roleManager.DeleteAsync(roleToDelete);
        return deleteRoleResult.Succeeded ? new Response<object>(model) : new Response<object>(string.Join(" ", deleteRoleResult.Errors.Select(e => e.Description)));
    }
}
