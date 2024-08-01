using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class createTableForSochitelTransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 1, 11, 48, 42, 854, DateTimeKind.Local).AddTicks(5467),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 22, 13, 44, 29, 333, DateTimeKind.Local).AddTicks(8164));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 1, 11, 48, 42, 854, DateTimeKind.Local).AddTicks(5030),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 22, 13, 44, 29, 333, DateTimeKind.Local).AddTicks(7809));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 1, 11, 48, 42, 846, DateTimeKind.Local).AddTicks(4013),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 22, 13, 44, 29, 326, DateTimeKind.Local).AddTicks(6582));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 1, 11, 48, 42, 822, DateTimeKind.Local).AddTicks(1004),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 22, 13, 44, 29, 279, DateTimeKind.Local).AddTicks(6684));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 1, 11, 48, 42, 819, DateTimeKind.Local).AddTicks(9769),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 22, 13, 44, 29, 276, DateTimeKind.Local).AddTicks(8089));

            migrationBuilder.CreateTable(
                name: "SochitelTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionAmount = table.Column<double>(type: "float", nullable: false),
                    CommissionRate = table.Column<double>(type: "float", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperatorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserReference = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SochitelTransactions", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2b20454c-a625-4d34-af63-34fa6c9b8448", "AQAAAAEAACcQAAAAEJ+NtSNh0MrYFIiljwtWsj98iX2Yj/RjhQEq5Vtd9UBK2xss23fcUYUtgFhXuGsc7A==", "81e64281-8543-4a86-93de-e666fc697a6f" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 1, 11, 48, 42, 840, DateTimeKind.Local).AddTicks(6009), new DateTime(2024, 8, 1, 11, 48, 42, 840, DateTimeKind.Local).AddTicks(6353) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 1, 11, 48, 42, 855, DateTimeKind.Local).AddTicks(7762), new DateTime(2024, 8, 1, 11, 48, 42, 855, DateTimeKind.Local).AddTicks(8066) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 1, 11, 48, 42, 855, DateTimeKind.Local).AddTicks(8465), new DateTime(2024, 8, 1, 11, 48, 42, 855, DateTimeKind.Local).AddTicks(8470) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 1, 11, 48, 42, 855, DateTimeKind.Local).AddTicks(8477), new DateTime(2024, 8, 1, 11, 48, 42, 855, DateTimeKind.Local).AddTicks(8478) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 1, 11, 48, 42, 855, DateTimeKind.Local).AddTicks(8484), new DateTime(2024, 8, 1, 11, 48, 42, 855, DateTimeKind.Local).AddTicks(8485) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 1, 11, 48, 42, 855, DateTimeKind.Local).AddTicks(8543), new DateTime(2024, 8, 1, 11, 48, 42, 855, DateTimeKind.Local).AddTicks(8544) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 1, 11, 48, 42, 855, DateTimeKind.Local).AddTicks(8553), new DateTime(2024, 8, 1, 11, 48, 42, 855, DateTimeKind.Local).AddTicks(8554) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SochitelTransactions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 22, 13, 44, 29, 333, DateTimeKind.Local).AddTicks(8164),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 1, 11, 48, 42, 854, DateTimeKind.Local).AddTicks(5467));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 22, 13, 44, 29, 333, DateTimeKind.Local).AddTicks(7809),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 1, 11, 48, 42, 854, DateTimeKind.Local).AddTicks(5030));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 22, 13, 44, 29, 326, DateTimeKind.Local).AddTicks(6582),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 1, 11, 48, 42, 846, DateTimeKind.Local).AddTicks(4013));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 22, 13, 44, 29, 279, DateTimeKind.Local).AddTicks(6684),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 1, 11, 48, 42, 822, DateTimeKind.Local).AddTicks(1004));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 22, 13, 44, 29, 276, DateTimeKind.Local).AddTicks(8089),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 1, 11, 48, 42, 819, DateTimeKind.Local).AddTicks(9769));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "96f02597-febc-4ea0-8c5f-66c90e773116", "AQAAAAEAACcQAAAAECjefyrVyhjEGxg0clIC/rFkSaNO9GefhASEut1+DHea8Lkt+Vi0dT2Wq+k6vmGlvQ==", "e5f6bc18-3b12-4d51-894e-e0cf268eabe5" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 22, 13, 44, 29, 321, DateTimeKind.Local).AddTicks(2962), new DateTime(2024, 7, 22, 13, 44, 29, 321, DateTimeKind.Local).AddTicks(3274) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 22, 13, 44, 29, 334, DateTimeKind.Local).AddTicks(9250), new DateTime(2024, 7, 22, 13, 44, 29, 334, DateTimeKind.Local).AddTicks(9536) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 22, 13, 44, 29, 335, DateTimeKind.Local).AddTicks(41), new DateTime(2024, 7, 22, 13, 44, 29, 335, DateTimeKind.Local).AddTicks(45) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 22, 13, 44, 29, 335, DateTimeKind.Local).AddTicks(52), new DateTime(2024, 7, 22, 13, 44, 29, 335, DateTimeKind.Local).AddTicks(53) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 22, 13, 44, 29, 335, DateTimeKind.Local).AddTicks(56), new DateTime(2024, 7, 22, 13, 44, 29, 335, DateTimeKind.Local).AddTicks(57) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 22, 13, 44, 29, 335, DateTimeKind.Local).AddTicks(61), new DateTime(2024, 7, 22, 13, 44, 29, 335, DateTimeKind.Local).AddTicks(62) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 22, 13, 44, 29, 335, DateTimeKind.Local).AddTicks(68), new DateTime(2024, 7, 22, 13, 44, 29, 335, DateTimeKind.Local).AddTicks(69) });
        }
    }
}
