using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class UpdateStaffRolesTableForNane : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 16, 40, 34, 287, DateTimeKind.Local).AddTicks(3982),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 16, 36, 24, 506, DateTimeKind.Local).AddTicks(2336));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 16, 40, 34, 287, DateTimeKind.Local).AddTicks(3584),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 16, 36, 24, 506, DateTimeKind.Local).AddTicks(1832));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 16, 40, 34, 281, DateTimeKind.Local).AddTicks(8066),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 16, 36, 24, 500, DateTimeKind.Local).AddTicks(2194));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 16, 40, 34, 260, DateTimeKind.Local).AddTicks(8129),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 16, 36, 24, 480, DateTimeKind.Local).AddTicks(2951));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 16, 40, 34, 259, DateTimeKind.Local).AddTicks(675),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 16, 36, 24, 478, DateTimeKind.Local).AddTicks(3183));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c9fe633d-3200-49ac-8435-612342d29086", "AQAAAAEAACcQAAAAELCoi74fBuJiQ8Xxpjb6iHk3Y0QhD7QacpmC0Q31F8dHZP0sSHTHdbx8kwbkJrcQiQ==", "6fd52dd3-daf9-48dc-8f8b-aafd308ad0d7" });

            migrationBuilder.UpdateData(
                table: "StaffRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Customer Service");

            migrationBuilder.UpdateData(
                table: "StaffRoles",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "System Wallet");

            migrationBuilder.UpdateData(
                table: "StaffRoles",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "System Admin");

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 16, 40, 34, 276, DateTimeKind.Local).AddTicks(8348), new DateTime(2024, 1, 29, 16, 40, 34, 276, DateTimeKind.Local).AddTicks(8533) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 16, 40, 34, 288, DateTimeKind.Local).AddTicks(4378), new DateTime(2024, 1, 29, 16, 40, 34, 288, DateTimeKind.Local).AddTicks(4838) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 16, 40, 34, 288, DateTimeKind.Local).AddTicks(5287), new DateTime(2024, 1, 29, 16, 40, 34, 288, DateTimeKind.Local).AddTicks(5292) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 16, 40, 34, 288, DateTimeKind.Local).AddTicks(5307), new DateTime(2024, 1, 29, 16, 40, 34, 288, DateTimeKind.Local).AddTicks(5310) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 16, 40, 34, 288, DateTimeKind.Local).AddTicks(5318), new DateTime(2024, 1, 29, 16, 40, 34, 288, DateTimeKind.Local).AddTicks(5320) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 16, 40, 34, 288, DateTimeKind.Local).AddTicks(5324), new DateTime(2024, 1, 29, 16, 40, 34, 288, DateTimeKind.Local).AddTicks(5325) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 16, 40, 34, 288, DateTimeKind.Local).AddTicks(5438), new DateTime(2024, 1, 29, 16, 40, 34, 288, DateTimeKind.Local).AddTicks(5442) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 16, 36, 24, 506, DateTimeKind.Local).AddTicks(2336),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 16, 40, 34, 287, DateTimeKind.Local).AddTicks(3982));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 16, 36, 24, 506, DateTimeKind.Local).AddTicks(1832),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 16, 40, 34, 287, DateTimeKind.Local).AddTicks(3584));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 16, 36, 24, 500, DateTimeKind.Local).AddTicks(2194),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 16, 40, 34, 281, DateTimeKind.Local).AddTicks(8066));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 16, 36, 24, 480, DateTimeKind.Local).AddTicks(2951),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 16, 40, 34, 260, DateTimeKind.Local).AddTicks(8129));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 16, 36, 24, 478, DateTimeKind.Local).AddTicks(3183),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 16, 40, 34, 259, DateTimeKind.Local).AddTicks(675));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "63713b7b-41d8-4f65-90c4-6d49cb4ca604", "AQAAAAEAACcQAAAAEOA8fKYXglOEH8kNYPpM7Vud4xHkk8+uqlGxsON6gESGKcATXsTE42byw4vLTHu0tQ==", "a1bb3337-f822-4c75-b927-4a618c9a3460" });

            migrationBuilder.UpdateData(
                table: "StaffRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "CustomerService");

            migrationBuilder.UpdateData(
                table: "StaffRoles",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "SystemWallet");

            migrationBuilder.UpdateData(
                table: "StaffRoles",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "SystemAdmin");

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 16, 36, 24, 495, DateTimeKind.Local).AddTicks(5039), new DateTime(2024, 1, 29, 16, 36, 24, 495, DateTimeKind.Local).AddTicks(5267) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 16, 36, 24, 507, DateTimeKind.Local).AddTicks(5255), new DateTime(2024, 1, 29, 16, 36, 24, 507, DateTimeKind.Local).AddTicks(5475) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 16, 36, 24, 507, DateTimeKind.Local).AddTicks(5733), new DateTime(2024, 1, 29, 16, 36, 24, 507, DateTimeKind.Local).AddTicks(5737) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 16, 36, 24, 507, DateTimeKind.Local).AddTicks(5742), new DateTime(2024, 1, 29, 16, 36, 24, 507, DateTimeKind.Local).AddTicks(5743) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 16, 36, 24, 507, DateTimeKind.Local).AddTicks(5746), new DateTime(2024, 1, 29, 16, 36, 24, 507, DateTimeKind.Local).AddTicks(5747) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 16, 36, 24, 507, DateTimeKind.Local).AddTicks(5751), new DateTime(2024, 1, 29, 16, 36, 24, 507, DateTimeKind.Local).AddTicks(5752) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 16, 36, 24, 507, DateTimeKind.Local).AddTicks(5756), new DateTime(2024, 1, 29, 16, 36, 24, 507, DateTimeKind.Local).AddTicks(5757) });
        }
    }
}
