using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class UpdatenewtableforstaffPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StaffPermissions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 13, 10, 53, 569, DateTimeKind.Local).AddTicks(307),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 12, 11, 31, 475, DateTimeKind.Local).AddTicks(3128));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 13, 10, 53, 568, DateTimeKind.Local).AddTicks(9869),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 12, 11, 31, 475, DateTimeKind.Local).AddTicks(2739));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 13, 10, 53, 561, DateTimeKind.Local).AddTicks(5275),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 12, 11, 31, 469, DateTimeKind.Local).AddTicks(5397));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 13, 10, 53, 539, DateTimeKind.Local).AddTicks(1827),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 12, 11, 31, 424, DateTimeKind.Local).AddTicks(1546));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 13, 10, 53, 537, DateTimeKind.Local).AddTicks(7198),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 12, 11, 31, 421, DateTimeKind.Local).AddTicks(6650));

            migrationBuilder.CreateTable(
                name: "StaffPermissionType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffPermissionType", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "06eb5d17-5494-46a4-b7fc-fab1b1be9904", "AQAAAAEAACcQAAAAEKXfBDdskbCu8z6cb4KlxcCUR2uB6GjDpNMR+FyqVHd33x59xkfaIL82/6nQiX0Xcg==", "9c30438a-8655-4101-8dab-0d346bcc12fb" });

            migrationBuilder.InsertData(
                table: "StaffPermissionType",
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

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 13, 10, 53, 556, DateTimeKind.Local).AddTicks(9018), new DateTime(2024, 1, 23, 13, 10, 53, 556, DateTimeKind.Local).AddTicks(9209) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 13, 10, 53, 569, DateTimeKind.Local).AddTicks(9176), new DateTime(2024, 1, 23, 13, 10, 53, 569, DateTimeKind.Local).AddTicks(9354) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 13, 10, 53, 569, DateTimeKind.Local).AddTicks(9616), new DateTime(2024, 1, 23, 13, 10, 53, 569, DateTimeKind.Local).AddTicks(9619) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 13, 10, 53, 569, DateTimeKind.Local).AddTicks(9698), new DateTime(2024, 1, 23, 13, 10, 53, 569, DateTimeKind.Local).AddTicks(9699) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 13, 10, 53, 569, DateTimeKind.Local).AddTicks(9704), new DateTime(2024, 1, 23, 13, 10, 53, 569, DateTimeKind.Local).AddTicks(9704) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 13, 10, 53, 569, DateTimeKind.Local).AddTicks(9708), new DateTime(2024, 1, 23, 13, 10, 53, 569, DateTimeKind.Local).AddTicks(9709) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 13, 10, 53, 569, DateTimeKind.Local).AddTicks(9715), new DateTime(2024, 1, 23, 13, 10, 53, 569, DateTimeKind.Local).AddTicks(9716) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StaffPermissionType");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 12, 11, 31, 475, DateTimeKind.Local).AddTicks(3128),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 13, 10, 53, 569, DateTimeKind.Local).AddTicks(307));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 12, 11, 31, 475, DateTimeKind.Local).AddTicks(2739),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 13, 10, 53, 568, DateTimeKind.Local).AddTicks(9869));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 12, 11, 31, 469, DateTimeKind.Local).AddTicks(5397),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 13, 10, 53, 561, DateTimeKind.Local).AddTicks(5275));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 12, 11, 31, 424, DateTimeKind.Local).AddTicks(1546),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 13, 10, 53, 539, DateTimeKind.Local).AddTicks(1827));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 12, 11, 31, 421, DateTimeKind.Local).AddTicks(6650),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 13, 10, 53, 537, DateTimeKind.Local).AddTicks(7198));

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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "86a2b1c4-c806-4d30-bfef-d9bf2a64e5d0", "AQAAAAEAACcQAAAAECn2hdiJHo/u5wKEE8aV7NFYDVI2CiHgpHyJqpPJUnUjPUt6feE3ksFdkQ+lqE47QA==", "396c558a-1616-4695-895a-8f8a3403252a" });

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
        }
    }
}
