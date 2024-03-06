using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineShop.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class _001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "UserManagement");

            migrationBuilder.EnsureSchema(
                name: "Sale");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "UserManagement",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "UserManagement",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Picture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsNationalIdConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CellPhone = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsCellPhoneConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedDateGregorian = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDatePersian = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsModified = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ModifyDateGregorian = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDatePersian = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSoftDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SoftDeleteDateGregorian = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoftDeleteDatePersian = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                schema: "Sale",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCategory_ProductCategory_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Sale",
                        principalTable: "ProductCategory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "UserManagement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "UserManagement",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "UserManagement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "UserManagement",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "UserManagement",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "UserManagement",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "UserManagement",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "UserManagement",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "UserManagement",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "UserManagement",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "UserManagement",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderHeader",
                schema: "Sale",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BuyerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDateGregorian = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 2, 28, 0, 36, 40, 574, DateTimeKind.Local).AddTicks(2789)),
                    CreatedDatePersian = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsModified = table.Column<bool>(type: "bit", nullable: false),
                    ModifyDateGregorian = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDatePersian = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSoftDeleted = table.Column<bool>(type: "bit", nullable: false),
                    SoftDeleteDateGregorian = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoftDeleteDatePersian = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderHeader_AspNetUsers_BuyerId",
                        column: x => x.BuyerId,
                        principalSchema: "UserManagement",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderHeader_AspNetUsers_SellerId",
                        column: x => x.SellerId,
                        principalSchema: "UserManagement",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "Sale",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDateGregorian = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDatePersian = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsModified = table.Column<bool>(type: "bit", nullable: false),
                    ModifyDateGregorian = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDatePersian = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSoftDeleted = table.Column<bool>(type: "bit", nullable: false),
                    SoftDeleteDateGregorian = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoftDeleteDatePersian = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_ProductCategory_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalSchema: "Sale",
                        principalTable: "ProductCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                schema: "Sale",
                columns: table => new
                {
                    OrderHeaderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "money", nullable: false),
                    Quantity = table.Column<decimal>(type: "money", nullable: false),
                    OrderHeaderId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => new { x.OrderHeaderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderDetails_OrderHeader_OrderHeaderId1",
                        column: x => x.OrderHeaderId1,
                        principalSchema: "Sale",
                        principalTable: "OrderHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Product_ProductId1",
                        column: x => x.ProductId1,
                        principalSchema: "Sale",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "UserManagement",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "1", "GodAdmin", "GODADMIN" },
                    { "2", "2", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                schema: "UserManagement",
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CellPhone", "ConcurrencyStamp", "CreatedDateGregorian", "CreatedDatePersian", "Email", "EmailConfirmed", "FirstName", "IsCellPhoneConfirmed", "IsNationalIdConfirmed", "LastName", "Location", "LockoutEnabled", "LockoutEnd", "ModifyDateGregorian", "ModifyDatePersian", "NationalId", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Picture", "SecurityStamp", "SoftDeleteDateGregorian", "SoftDeleteDatePersian", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1", 0, "09120000000", "57730a5c-8236-430a-8ec6-4d5f0edddfc3", new DateTime(2024, 2, 28, 0, 36, 40, 577, DateTimeKind.Local).AddTicks(2570), "1402/12/9 00:36:40.5772585", null, false, "Amir", true, true, "Shahbazi", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "0440000000", null, null, null, null, false, null, "7800fb50-1bb6-47e5-bf1f-ca0982879f99", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null });

            migrationBuilder.InsertData(
                schema: "UserManagement",
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "1" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "UserManagement",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "UserManagement",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "UserManagement",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "UserManagement",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "UserManagement",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "UserManagement",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CellPhone",
                schema: "UserManagement",
                table: "AspNetUsers",
                column: "CellPhone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_NationalId",
                schema: "UserManagement",
                table: "AspNetUsers",
                column: "NationalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "UserManagement",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderHeaderId1",
                schema: "Sale",
                table: "OrderDetails",
                column: "OrderHeaderId1");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId1",
                schema: "Sale",
                table: "OrderDetails",
                column: "ProductId1");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeader_BuyerId",
                schema: "Sale",
                table: "OrderHeader",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeader_SellerId",
                schema: "Sale",
                table: "OrderHeader",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductCategoryId",
                schema: "Sale",
                table: "Product",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_ParentId",
                schema: "Sale",
                table: "ProductCategory",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "UserManagement");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "UserManagement");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "UserManagement");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "UserManagement");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "UserManagement");

            migrationBuilder.DropTable(
                name: "OrderDetails",
                schema: "Sale");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "UserManagement");

            migrationBuilder.DropTable(
                name: "OrderHeader",
                schema: "Sale");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "Sale");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "UserManagement");

            migrationBuilder.DropTable(
                name: "ProductCategory",
                schema: "Sale");
        }
    }
}
