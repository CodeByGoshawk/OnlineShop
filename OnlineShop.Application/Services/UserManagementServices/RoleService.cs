using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Contracts.UserManagement;
using OnlineShop.Application.Dtos.UserManagementDtos.RoleDtos;
using OnlineShop.Domain.Aggregates.UserManagementAggregates;
using PublicTools.Resources;
using ResponseFramewrok;

namespace OnlineShop.Application.Services.UserManagementServices;
public class RoleService(RoleManager<OnlineShopRole> roleManager) : IRoleService
{
    private readonly RoleManager<OnlineShopRole> _roleManager = roleManager;

    // Get
    public async Task<IResponse<GetOnlineShopRoleResultAppDto>> Get(GetOnlineShopRoleAppDto model)
    {
        var selectedRole = await _roleManager.FindByIdAsync(model.Id);
        if (selectedRole is null) return new Response<GetOnlineShopRoleResultAppDto>(MessageResource.Error_RoleNotFound);

        var getRoleResultDto = new GetOnlineShopRoleResultAppDto
        {
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
            ConcurrencyStamp = model.ConcurrencyStamp
        };

        await _roleManager.CreateAsync(newRole);
        return new Response<object>(model);
    }

    // Delete
    public async Task<IResponse<object>> Edit(EditOnlineShopRoleAppDto model)
    {
        if (model is null) return new Response<object>(MessageResource.Error_NullInputModel);
        if (model.Name is null) return new Response<object>(MessageResource.Error_RequiredField);

        var existingNameRole = await _roleManager.Roles.SingleOrDefaultAsync(r => r.NormalizedName == model.Name.ToUpper());
        if (existingNameRole is not null && existingNameRole.Id != model.Id) return new Response<object>(MessageResource.Error_RoleNameAlreadyExist);

        var editedRole = await _roleManager.FindByIdAsync(model.Id);
        if (editedRole is null) return new Response<object>(MessageResource.Error_RoleNotFound);

        editedRole.Name = model.Name;
        editedRole.NormalizedName = model.Name.ToUpper();
        editedRole.ConcurrencyStamp = model.ConcurrencyStamp;

        await _roleManager.UpdateAsync(editedRole);
        return new Response<object>(model);
    }
}
