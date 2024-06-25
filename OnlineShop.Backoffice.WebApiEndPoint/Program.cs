using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineShop.Backoffice.Application.Contracts.Sale;
using OnlineShop.Backoffice.Application.Contracts.UserManagement;
using OnlineShop.Backoffice.Application.Services.SaleServices;
using OnlineShop.Backoffice.Application.Services.UserManagementServices;
using OnlineShop.Backoffice.WebApiEndPoint.Authorizations.Handlers;
using OnlineShop.Backoffice.WebApiEndPoint.Authorizations.Requirements;
using OnlineShop.Domain.Aggregates.UserManagementAggregates;
using OnlineShop.EFCore;
using OnlineShop.RepositoryDesignPattern.Contracts;
using OnlineShop.RepositoryDesignPattern.Services.SaleRepositories;
using PublicTools.Constants;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("OnlineShop");
builder.Services.AddDbContext<OnlineShopDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddIdentity<OnlineShopUser, OnlineShopRole>()
    .AddEntityFrameworkStores<OnlineShopDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]!)),

        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidIssuer = builder.Configuration["JWT:Issuer"],
    };
});
builder.Services.Configure<IdentityOptions>(c =>
{
    c.Password.RequireDigit = false;
    c.Password.RequireLowercase = false;
    c.Password.RequireNonAlphanumeric = false;
    c.Password.RequiredLength = 3;
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy(PolicyConstants.OwnerOnlyPolicy, policy =>
        policy.Requirements.Add(new OwnerOnlyRequirement()))
    .AddPolicy(PolicyConstants.AdminsOrOwnerOnly, policy =>
        policy.Requirements.Add(new AdminsOrOwnerOnlyRequirement()))
    .AddPolicy(PolicyConstants.AdminsOnly, policy =>
        policy.RequireRole(DatabaseConstants.DefaultRoles.GodAdminName,DatabaseConstants.DefaultRoles.AdminName));

builder.Services.AddSingleton<IAuthorizationHandler, OwnerOnlyAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, AdminsOrOwnerOnlyAuthorizationHandler>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
