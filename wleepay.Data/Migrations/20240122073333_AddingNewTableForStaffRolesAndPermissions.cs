using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class AddingNewTableForStaffRolesAndPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 22, 13, 3, 32, 222, DateTimeKind.Local).AddTicks(2512),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 19, 11, 57, 44, 617, DateTimeKind.Local).AddTicks(4443));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 22, 13, 3, 32, 222, DateTimeKind.Local).AddTicks(1859),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 19, 11, 57, 44, 617, DateTimeKind.Local).AddTicks(3956));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 22, 13, 3, 32, 215, DateTimeKind.Local).AddTicks(390),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 19, 11, 57, 44, 608, DateTimeKind.Local).AddTicks(7822));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 22, 13, 3, 32, 188, DateTimeKind.Local).AddTicks(7824),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 19, 11, 57, 44, 561, DateTimeKind.Local).AddTicks(9074));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 22, 13, 3, 32, 186, DateTimeKind.Local).AddTicks(6898),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 19, 11, 57, 44, 559, DateTimeKind.Local).AddTicks(7788));

            migrationBuilder.CreateTable(
                name: "StaffPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffPermissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaffRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffRoles", x => x.Id);
                });

         
            migrationBuilder.InsertData(
                table: "StaffPermissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Dashboard" },
                    { 2, "SystemWallet" },
                    { 3, "AgentManagement" },
                    { 4, "SubscriberManagement" },
                    { 5, "CustomerService" },
                    { 6, "Reports" },
                    { 7, "Compliance&RiskManagement" },
                    { 8, "SystemAdmin" }
                });

            migrationBuilder.InsertData(
                table: "StaffRoles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 7, "SystemAdmin" },
                    { 6, "Finance" },
                    { 5, "SystemWallet" },
                    { 4, "IT" },
                    { 3, "CustomerService" },
                    { 2, "HR" },
                    { 1, "Marketing" }
                });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 22, 13, 3, 32, 209, DateTimeKind.Local).AddTicks(2113), new DateTime(2024, 1, 22, 13, 3, 32, 209, DateTimeKind.Local).AddTicks(2349) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 22, 13, 3, 32, 223, DateTimeKind.Local).AddTicks(4250), new DateTime(2024, 1, 22, 13, 3, 32, 223, DateTimeKind.Local).AddTicks(4461) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 22, 13, 3, 32, 223, DateTimeKind.Local).AddTicks(4819), new DateTime(2024, 1, 22, 13, 3, 32, 223, DateTimeKind.Local).AddTicks(4824) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 22, 13, 3, 32, 223, DateTimeKind.Local).AddTicks(4831), new DateTime(2024, 1, 22, 13, 3, 32, 223, DateTimeKind.Local).AddTicks(4832) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 22, 13, 3, 32, 223, DateTimeKind.Local).AddTicks(4836), new DateTime(2024, 1, 22, 13, 3, 32, 223, DateTimeKind.Local).AddTicks(4837) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 22, 13, 3, 32, 223, DateTimeKind.Local).AddTicks(4841), new DateTime(2024, 1, 22, 13, 3, 32, 223, DateTimeKind.Local).AddTicks(4843) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 22, 13, 3, 32, 223, DateTimeKind.Local).AddTicks(4891), new DateTime(2024, 1, 22, 13, 3, 32, 223, DateTimeKind.Local).AddTicks(4892) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StaffPermissions");

            migrationBuilder.DropTable(
                name: "StaffRoles");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 19, 11, 57, 44, 617, DateTimeKind.Local).AddTicks(4443),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 22, 13, 3, 32, 222, DateTimeKind.Local).AddTicks(2512));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 19, 11, 57, 44, 617, DateTimeKind.Local).AddTicks(3956),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 22, 13, 3, 32, 222, DateTimeKind.Local).AddTicks(1859));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 19, 11, 57, 44, 608, DateTimeKind.Local).AddTicks(7822),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 22, 13, 3, 32, 215, DateTimeKind.Local).AddTicks(390));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 19, 11, 57, 44, 561, DateTimeKind.Local).AddTicks(9074),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 22, 13, 3, 32, 188, DateTimeKind.Local).AddTicks(7824));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 19, 11, 57, 44, 559, DateTimeKind.Local).AddTicks(7788),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 22, 13, 3, 32, 186, DateTimeKind.Local).AddTicks(6898));

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 19, 11, 57, 44, 602, DateTimeKind.Local).AddTicks(3012), new DateTime(2024, 1, 19, 11, 57, 44, 602, DateTimeKind.Local).AddTicks(3253) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 19, 11, 57, 44, 619, DateTimeKind.Local).AddTicks(1617), new DateTime(2024, 1, 19, 11, 57, 44, 619, DateTimeKind.Local).AddTicks(1816) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 19, 11, 57, 44, 619, DateTimeKind.Local).AddTicks(2113), new DateTime(2024, 1, 19, 11, 57, 44, 619, DateTimeKind.Local).AddTicks(2117) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 19, 11, 57, 44, 619, DateTimeKind.Local).AddTicks(2174), new DateTime(2024, 1, 19, 11, 57, 44, 619, DateTimeKind.Local).AddTicks(2175) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 19, 11, 57, 44, 619, DateTimeKind.Local).AddTicks(2180), new DateTime(2024, 1, 19, 11, 57, 44, 619, DateTimeKind.Local).AddTicks(2181) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 19, 11, 57, 44, 619, DateTimeKind.Local).AddTicks(2185), new DateTime(2024, 1, 19, 11, 57, 44, 619, DateTimeKind.Local).AddTicks(2186) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 19, 11, 57, 44, 619, DateTimeKind.Local).AddTicks(2192), new DateTime(2024, 1, 19, 11, 57, 44, 619, DateTimeKind.Local).AddTicks(2193) });
        }
    }
}
