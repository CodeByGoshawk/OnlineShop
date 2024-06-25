using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class _003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "EmailIndex",
                schema: "UserManagement",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CellPhone",
                schema: "UserManagement",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Email",
                schema: "UserManagement",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_NationalId",
                schema: "UserManagement",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                schema: "UserManagement",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "CreatedDateGregorian", "CreatedDatePersian", "PasswordHash", "SecurityStamp" },
                values: new object[] { "245f40e3-7bc0-426e-b1cd-1f820374e375", new DateTime(2024, 6, 17, 19, 57, 1, 23, DateTimeKind.Local).AddTicks(7312), "1403/3/28 19:57:01.0237333", "AQAAAAIAAYagAAAAEBeAA7ZO+wXZ7ra+sS4NH6KLp6OrzYvqKcGIwwEo8IsOwiTr7CBKeqrdAOAtDC7/Qw==", "38571247-c663-4b4c-ad35-36fe4799060c" });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "UserManagement",
                table: "AspNetUsers",
                column: "NormalizedEmail");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "EmailIndex",
                schema: "UserManagement",
                table: "AspNetUsers");

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

            migrationBuilder.UpdateData(
                schema: "UserManagement",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "CreatedDateGregorian", "CreatedDatePersian", "PasswordHash", "SecurityStamp" },
                values: new object[] { "56bfb3a6-5d92-4de1-8f90-489e73898c5b", new DateTime(2024, 6, 14, 1, 24, 43, 253, DateTimeKind.Local).AddTicks(1197), "1403/3/25 01:24:43.2531214", "AQAAAAIAAYagAAAAEDXTnz3FyzbuTJYOC2FpomWGEsywA3Gc7TZB5nKeLNfOpJmsSIGFDPg3zYhK1RPTYg==", "92abd87c-e5d8-46ff-aeb7-2eb26a5560d4" });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "UserManagement",
                table: "AspNetUsers",
                column: "NormalizedEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CellPhone",
                schema: "UserManagement",
                table: "AspNetUsers",
                column: "CellPhone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                schema: "UserManagement",
                table: "AspNetUsers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_NationalId",
                schema: "UserManagement",
                table: "AspNetUsers",
                column: "NationalId",
                unique: true);
        }
    }
}
