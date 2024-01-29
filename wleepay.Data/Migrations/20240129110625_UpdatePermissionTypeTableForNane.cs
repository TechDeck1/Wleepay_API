using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class UpdatePermissionTypeTableForNane : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 16, 36, 24, 506, DateTimeKind.Local).AddTicks(2336),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 24, 16, 17, 55, 772, DateTimeKind.Local).AddTicks(658));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 16, 36, 24, 506, DateTimeKind.Local).AddTicks(1832),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 24, 16, 17, 55, 772, DateTimeKind.Local).AddTicks(241));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 16, 36, 24, 500, DateTimeKind.Local).AddTicks(2194),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 24, 16, 17, 55, 763, DateTimeKind.Local).AddTicks(592));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 16, 36, 24, 480, DateTimeKind.Local).AddTicks(2951),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 24, 16, 17, 55, 727, DateTimeKind.Local).AddTicks(3566));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 16, 36, 24, 478, DateTimeKind.Local).AddTicks(3183),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 24, 16, 17, 55, 725, DateTimeKind.Local).AddTicks(3814));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "63713b7b-41d8-4f65-90c4-6d49cb4ca604", "AQAAAAEAACcQAAAAEOA8fKYXglOEH8kNYPpM7Vud4xHkk8+uqlGxsON6gESGKcATXsTE42byw4vLTHu0tQ==", "a1bb3337-f822-4c75-b927-4a618c9a3460" });

            migrationBuilder.UpdateData(
                table: "StaffPermissionType",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "System Wallet");

            migrationBuilder.UpdateData(
                table: "StaffPermissionType",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Agent Management");

            migrationBuilder.UpdateData(
                table: "StaffPermissionType",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Subscriber Management");

            migrationBuilder.UpdateData(
                table: "StaffPermissionType",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Customer Service");

            migrationBuilder.UpdateData(
                table: "StaffPermissionType",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Compliance & Risk Management");

            migrationBuilder.UpdateData(
                table: "StaffPermissionType",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "System Admin");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 24, 16, 17, 55, 772, DateTimeKind.Local).AddTicks(658),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 16, 36, 24, 506, DateTimeKind.Local).AddTicks(2336));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 24, 16, 17, 55, 772, DateTimeKind.Local).AddTicks(241),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 16, 36, 24, 506, DateTimeKind.Local).AddTicks(1832));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 24, 16, 17, 55, 763, DateTimeKind.Local).AddTicks(592),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 16, 36, 24, 500, DateTimeKind.Local).AddTicks(2194));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 24, 16, 17, 55, 727, DateTimeKind.Local).AddTicks(3566),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 16, 36, 24, 480, DateTimeKind.Local).AddTicks(2951));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 24, 16, 17, 55, 725, DateTimeKind.Local).AddTicks(3814),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 16, 36, 24, 478, DateTimeKind.Local).AddTicks(3183));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f0c56e5c-c7c0-439e-8394-d9b40ec49dcd", "AQAAAAEAACcQAAAAECC5ffF8n9lPDiGdpS26u5iHvkYGXUwKQaB1ztx3v7EGqzR3ZipVXSo1Tc9n56qpsg==", "284d456a-1d61-40ab-9419-11411438e71e" });

            migrationBuilder.UpdateData(
                table: "StaffPermissionType",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "SystemWallet");

            migrationBuilder.UpdateData(
                table: "StaffPermissionType",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "AgentManagement");

            migrationBuilder.UpdateData(
                table: "StaffPermissionType",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "SubscriberManagement");

            migrationBuilder.UpdateData(
                table: "StaffPermissionType",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "CustomerService");

            migrationBuilder.UpdateData(
                table: "StaffPermissionType",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Compliance&RiskManagement");

            migrationBuilder.UpdateData(
                table: "StaffPermissionType",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "SystemAdmin");

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 24, 16, 17, 55, 756, DateTimeKind.Local).AddTicks(7370), new DateTime(2024, 1, 24, 16, 17, 55, 756, DateTimeKind.Local).AddTicks(7591) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 24, 16, 17, 55, 772, DateTimeKind.Local).AddTicks(9526), new DateTime(2024, 1, 24, 16, 17, 55, 772, DateTimeKind.Local).AddTicks(9708) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 24, 16, 17, 55, 772, DateTimeKind.Local).AddTicks(9969), new DateTime(2024, 1, 24, 16, 17, 55, 772, DateTimeKind.Local).AddTicks(9973) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 24, 16, 17, 55, 772, DateTimeKind.Local).AddTicks(9978), new DateTime(2024, 1, 24, 16, 17, 55, 772, DateTimeKind.Local).AddTicks(9979) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 24, 16, 17, 55, 772, DateTimeKind.Local).AddTicks(9983), new DateTime(2024, 1, 24, 16, 17, 55, 772, DateTimeKind.Local).AddTicks(9984) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 24, 16, 17, 55, 772, DateTimeKind.Local).AddTicks(9988), new DateTime(2024, 1, 24, 16, 17, 55, 772, DateTimeKind.Local).AddTicks(9989) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 24, 16, 17, 55, 773, DateTimeKind.Local).AddTicks(35), new DateTime(2024, 1, 24, 16, 17, 55, 773, DateTimeKind.Local).AddTicks(36) });
        }
    }
}
