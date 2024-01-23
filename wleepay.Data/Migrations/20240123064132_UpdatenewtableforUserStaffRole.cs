using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class UpdatenewtableforUserStaffRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "UsersStaffRole");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 12, 11, 31, 475, DateTimeKind.Local).AddTicks(3128),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 11, 26, 30, 264, DateTimeKind.Local).AddTicks(3374));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 12, 11, 31, 475, DateTimeKind.Local).AddTicks(2739),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 11, 26, 30, 264, DateTimeKind.Local).AddTicks(2838));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UsersStaffRole",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "UsersStaffRole",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "UsersStaffRole",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 12, 11, 31, 469, DateTimeKind.Local).AddTicks(5397),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 11, 26, 30, 251, DateTimeKind.Local).AddTicks(696));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 12, 11, 31, 424, DateTimeKind.Local).AddTicks(1546),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 11, 26, 29, 761, DateTimeKind.Local).AddTicks(5060));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 12, 11, 31, 421, DateTimeKind.Local).AddTicks(6650),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 11, 26, 29, 759, DateTimeKind.Local).AddTicks(1331));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "86a2b1c4-c806-4d30-bfef-d9bf2a64e5d0", "AQAAAAEAACcQAAAAECn2hdiJHo/u5wKEE8aV7NFYDVI2CiHgpHyJqpPJUnUjPUt6feE3ksFdkQ+lqE47QA==", "396c558a-1616-4695-895a-8f8a3403252a" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 12, 11, 31, 463, DateTimeKind.Local).AddTicks(1819), new DateTime(2024, 1, 23, 12, 11, 31, 463, DateTimeKind.Local).AddTicks(2031) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 12, 11, 31, 476, DateTimeKind.Local).AddTicks(1762), new DateTime(2024, 1, 23, 12, 11, 31, 476, DateTimeKind.Local).AddTicks(1938) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 12, 11, 31, 476, DateTimeKind.Local).AddTicks(2182), new DateTime(2024, 1, 23, 12, 11, 31, 476, DateTimeKind.Local).AddTicks(2185) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 12, 11, 31, 476, DateTimeKind.Local).AddTicks(2190), new DateTime(2024, 1, 23, 12, 11, 31, 476, DateTimeKind.Local).AddTicks(2191) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 12, 11, 31, 476, DateTimeKind.Local).AddTicks(2194), new DateTime(2024, 1, 23, 12, 11, 31, 476, DateTimeKind.Local).AddTicks(2195) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 12, 11, 31, 476, DateTimeKind.Local).AddTicks(2199), new DateTime(2024, 1, 23, 12, 11, 31, 476, DateTimeKind.Local).AddTicks(2200) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 12, 11, 31, 476, DateTimeKind.Local).AddTicks(2205), new DateTime(2024, 1, 23, 12, 11, 31, 476, DateTimeKind.Local).AddTicks(2206) });

            migrationBuilder.CreateIndex(
                name: "IX_UsersStaffRole_UserId",
                table: "UsersStaffRole",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersStaffRole_AspNetUsers_UserId",
                table: "UsersStaffRole",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersStaffRole_AspNetUsers_UserId",
                table: "UsersStaffRole");

            migrationBuilder.DropIndex(
                name: "IX_UsersStaffRole_UserId",
                table: "UsersStaffRole");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "UsersStaffRole");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "UsersStaffRole");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 11, 26, 30, 264, DateTimeKind.Local).AddTicks(3374),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 12, 11, 31, 475, DateTimeKind.Local).AddTicks(3128));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 11, 26, 30, 264, DateTimeKind.Local).AddTicks(2838),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 12, 11, 31, 475, DateTimeKind.Local).AddTicks(2739));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UsersStaffRole",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "UsersStaffRole",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 11, 26, 30, 251, DateTimeKind.Local).AddTicks(696),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 12, 11, 31, 469, DateTimeKind.Local).AddTicks(5397));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 11, 26, 29, 761, DateTimeKind.Local).AddTicks(5060),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 12, 11, 31, 424, DateTimeKind.Local).AddTicks(1546));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 11, 26, 29, 759, DateTimeKind.Local).AddTicks(1331),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 12, 11, 31, 421, DateTimeKind.Local).AddTicks(6650));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c440ebdc-6e8d-44ed-a40a-4caf43b0798f", "AQAAAAEAACcQAAAAEFiYpVpiyizYg0KTJdklgj1F92CspBeZ0MlbviHK7rT8HywY9l4pf/A+4zhVw/xP3A==", "fb2a6e1d-03dd-4a5f-ad05-3f5ef03e0908" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 11, 26, 30, 242, DateTimeKind.Local).AddTicks(2844), new DateTime(2024, 1, 23, 11, 26, 30, 242, DateTimeKind.Local).AddTicks(3184) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 11, 26, 30, 266, DateTimeKind.Local).AddTicks(1133), new DateTime(2024, 1, 23, 11, 26, 30, 266, DateTimeKind.Local).AddTicks(1481) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 11, 26, 30, 266, DateTimeKind.Local).AddTicks(2016), new DateTime(2024, 1, 23, 11, 26, 30, 266, DateTimeKind.Local).AddTicks(2021) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 11, 26, 30, 266, DateTimeKind.Local).AddTicks(2031), new DateTime(2024, 1, 23, 11, 26, 30, 266, DateTimeKind.Local).AddTicks(2033) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 11, 26, 30, 266, DateTimeKind.Local).AddTicks(2039), new DateTime(2024, 1, 23, 11, 26, 30, 266, DateTimeKind.Local).AddTicks(2041) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 11, 26, 30, 266, DateTimeKind.Local).AddTicks(2047), new DateTime(2024, 1, 23, 11, 26, 30, 266, DateTimeKind.Local).AddTicks(2048) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 11, 26, 30, 266, DateTimeKind.Local).AddTicks(2064), new DateTime(2024, 1, 23, 11, 26, 30, 266, DateTimeKind.Local).AddTicks(2066) });
        }
    }
}
