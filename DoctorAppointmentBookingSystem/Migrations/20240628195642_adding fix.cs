using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorAppointmentBookingSystem.Migrations
{
    /// <inheritdoc />
    public partial class addingfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9e0f8395-c150-4dbf-9202-591c8cd772c7", "3cba3669-fd4b-4b22-888e-d7b50778f22b" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9e0f8395-c150-4dbf-9202-591c8cd772c7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3cba3669-fd4b-4b22-888e-d7b50778f22b");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Patients",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOBirth",
                table: "Doctors",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "352be39c-753c-4a4b-883f-f1c8a2499e48", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Adress", "ConcurrencyStamp", "Created", "CreatedBy", "DateOfBirth", "Email", "EmailConfirmed", "FullName", "Gender", "IsDeleted", "LockoutEnabled", "LockoutEnd", "Modified", "ModifiedBy", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserRole" },
                values: new object[] { "bddab4bc-8f7a-4602-b537-f21e049e1e9f", 0, "No 4 Unity Str Aboru", "2dfb6a01-944f-43e6-b278-a74b9805a70b", new DateTime(2024, 6, 28, 19, 56, 38, 898, DateTimeKind.Utc).AddTicks(929), null, new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", true, "Admin User", 1, false, false, null, null, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEPoQxAvaMZU0cnr9T2omiFIFNc5beXpsWOtOAbh3eH1lW/jCyjys5SIJg4unKulAcw==", null, false, "", false, "Admin", 1 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "352be39c-753c-4a4b-883f-f1c8a2499e48", "bddab4bc-8f7a-4602-b537-f21e049e1e9f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "352be39c-753c-4a4b-883f-f1c8a2499e48", "bddab4bc-8f7a-4602-b537-f21e049e1e9f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "352be39c-753c-4a4b-883f-f1c8a2499e48");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bddab4bc-8f7a-4602-b537-f21e049e1e9f");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateOfBirth",
                table: "Patients",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateOBirth",
                table: "Doctors",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9e0f8395-c150-4dbf-9202-591c8cd772c7", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Adress", "ConcurrencyStamp", "Created", "CreatedBy", "DateOfBirth", "Email", "EmailConfirmed", "FullName", "Gender", "IsDeleted", "LockoutEnabled", "LockoutEnd", "Modified", "ModifiedBy", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserRole" },
                values: new object[] { "3cba3669-fd4b-4b22-888e-d7b50778f22b", 0, "No 4 Unity Str Aboru", "a70345d7-f632-4055-a12b-9a3efe4398f3", new DateTime(2024, 6, 28, 8, 8, 4, 103, DateTimeKind.Utc).AddTicks(7982), null, new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", true, "Admin User", 1, false, false, null, null, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEGdyiWbaISzKewwxM70v8ZZZWzEPrrtMKMMrcnpvdMvfwBOu1aYdj/cPhwkoBcyUIQ==", null, false, "", false, "Admin", 1 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "9e0f8395-c150-4dbf-9202-591c8cd772c7", "3cba3669-fd4b-4b22-888e-d7b50778f22b" });
        }
    }
}
