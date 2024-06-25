using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class _002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHeader_AspNetUsers_OnlineShopUserId",
                schema: "Sale",
                table: "OrderHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHeader_AspNetUsers_SellerId",
                schema: "Sale",
                table: "OrderHeader");

            migrationBuilder.DropIndex(
                name: "IX_OrderHeader_OnlineShopUserId",
                schema: "Sale",
                table: "OrderHeader");

            migrationBuilder.DropIndex(
                name: "IX_OrderHeader_SellerId",
                schema: "Sale",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "OnlineShopUserId",
                schema: "Sale",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "SellerId",
                schema: "Sale",
                table: "OrderHeader");

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                schema: "Sale",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "UserManagement",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "CreatedDateGregorian", "CreatedDatePersian", "PasswordHash", "SecurityStamp" },
                values: new object[] { "56bfb3a6-5d92-4de1-8f90-489e73898c5b", new DateTime(2024, 6, 14, 1, 24, 43, 253, DateTimeKind.Local).AddTicks(1197), "1403/3/25 01:24:43.2531214", "AQAAAAIAAYagAAAAEDXTnz3FyzbuTJYOC2FpomWGEsywA3Gc7TZB5nKeLNfOpJmsSIGFDPg3zYhK1RPTYg==", "92abd87c-e5d8-46ff-aeb7-2eb26a5560d4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                schema: "Sale",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "OnlineShopUserId",
                schema: "Sale",
                table: "OrderHeader",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerId",
                schema: "Sale",
                table: "OrderHeader",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "UserManagement",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "CreatedDateGregorian", "CreatedDatePersian", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6de8ae29-db0a-4d11-b36e-9b6300568762", new DateTime(2024, 5, 24, 18, 55, 4, 591, DateTimeKind.Local).AddTicks(2305), "1403/3/4 18:55:04.5912340", "AQAAAAIAAYagAAAAEBsPaFbYCFgUpX4oZWXe7pe1Z36+F3So+gLwDppB5p23w3+ALQkozQJJVM17Ot2Dtw==", "971da074-48a9-4ee6-8f8d-223eededb906" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeader_OnlineShopUserId",
                schema: "Sale",
                table: "OrderHeader",
                column: "OnlineShopUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeader_SellerId",
                schema: "Sale",
                table: "OrderHeader",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHeader_AspNetUsers_OnlineShopUserId",
                schema: "Sale",
                table: "OrderHeader",
                column: "OnlineShopUserId",
                principalSchema: "UserManagement",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHeader_AspNetUsers_SellerId",
                schema: "Sale",
                table: "OrderHeader",
                column: "SellerId",
                principalSchema: "UserManagement",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
