using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class _004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CellPhone_IsSoftDeleted",
                schema: "UserManagement",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Email_IsSoftDeleted",
                schema: "UserManagement",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_NationalId_IsSoftDeleted",
                schema: "UserManagement",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_NormalizedEmail_IsSoftDeleted",
                schema: "UserManagement",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "NationalId",
                schema: "UserManagement",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "CellPhone",
                schema: "UserManagement",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                schema: "UserManagement",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "CreatedDateGregorian", "CreatedDatePersian", "PasswordHash", "SecurityStamp" },
                values: new object[] { "14863898-67e8-4ea8-935e-137fdd7837a0", new DateTime(2024, 6, 25, 17, 36, 58, 263, DateTimeKind.Local).AddTicks(2202), "1403/4/5 17:36:58.2632226", "AQAAAAIAAYagAAAAEKuz9LL2KlxKDU4a0pZr9TE4kz6tvW2JXKOS8vPBxWAt6ybqN5tYqT/yADBaqxhmbQ==", "ee8dc3b7-2edf-4fe2-85a0-27bd33485a03" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NationalId",
                schema: "UserManagement",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CellPhone",
                schema: "UserManagement",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                schema: "UserManagement",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "CreatedDateGregorian", "CreatedDatePersian", "PasswordHash", "SecurityStamp" },
                values: new object[] { "245f40e3-7bc0-426e-b1cd-1f820374e375", new DateTime(2024, 6, 17, 19, 57, 1, 23, DateTimeKind.Local).AddTicks(7312), "1403/3/28 19:57:01.0237333", "AQAAAAIAAYagAAAAEBeAA7ZO+wXZ7ra+sS4NH6KLp6OrzYvqKcGIwwEo8IsOwiTr7CBKeqrdAOAtDC7/Qw==", "38571247-c663-4b4c-ad35-36fe4799060c" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CellPhone_IsSoftDeleted",
                schema: "UserManagement",
                table: "AspNetUsers",
                columns: new[] { "CellPhone", "IsSoftDeleted" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email_IsSoftDeleted",
                schema: "UserManagement",
                table: "AspNetUsers",
                columns: new[] { "Email", "IsSoftDeleted" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_NationalId_IsSoftDeleted",
                schema: "UserManagement",
                table: "AspNetUsers",
                columns: new[] { "NationalId", "IsSoftDeleted" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_NormalizedEmail_IsSoftDeleted",
                schema: "UserManagement",
                table: "AspNetUsers",
                columns: new[] { "NormalizedEmail", "IsSoftDeleted" },
                unique: true);
        }
    }
}
