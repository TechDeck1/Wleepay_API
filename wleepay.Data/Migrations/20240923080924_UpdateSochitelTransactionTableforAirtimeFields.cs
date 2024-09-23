using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class UpdateSochitelTransactionTableforAirtimeFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 23, 13, 39, 22, 983, DateTimeKind.Local).AddTicks(5282),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 23, 13, 33, 24, 673, DateTimeKind.Local).AddTicks(8189));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 23, 13, 39, 22, 983, DateTimeKind.Local).AddTicks(4930),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 23, 13, 33, 24, 673, DateTimeKind.Local).AddTicks(7499));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 23, 13, 39, 22, 975, DateTimeKind.Local).AddTicks(7421),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 23, 13, 33, 24, 660, DateTimeKind.Local).AddTicks(6162));

            migrationBuilder.AddColumn<string>(
                name: "MobileVendor",
                table: "SochitelTransactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "SochitelTransactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 23, 13, 39, 22, 944, DateTimeKind.Local).AddTicks(7941),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 23, 13, 33, 24, 604, DateTimeKind.Local).AddTicks(9103));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 23, 13, 39, 22, 942, DateTimeKind.Local).AddTicks(5980),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 23, 13, 33, 24, 602, DateTimeKind.Local).AddTicks(870));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3ceb2733-62dc-42c9-93d0-e1f9d08b20ff", "AQAAAAEAACcQAAAAEERH2PZl2WDQjFiWxiiVvgsPIZjG578ppZtTOQ3k1TfgCHSibs0uj3bsSg/C2OCKYA==", "2ce50d14-68f8-4631-aa14-3ece39d2647c" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 23, 13, 39, 22, 970, DateTimeKind.Local).AddTicks(5968), new DateTime(2024, 9, 23, 13, 39, 22, 970, DateTimeKind.Local).AddTicks(6254) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 23, 13, 39, 22, 984, DateTimeKind.Local).AddTicks(6196), new DateTime(2024, 9, 23, 13, 39, 22, 984, DateTimeKind.Local).AddTicks(6465) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 23, 13, 39, 22, 984, DateTimeKind.Local).AddTicks(6838), new DateTime(2024, 9, 23, 13, 39, 22, 984, DateTimeKind.Local).AddTicks(6842) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 23, 13, 39, 22, 984, DateTimeKind.Local).AddTicks(6850), new DateTime(2024, 9, 23, 13, 39, 22, 984, DateTimeKind.Local).AddTicks(6850) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 23, 13, 39, 22, 984, DateTimeKind.Local).AddTicks(6854), new DateTime(2024, 9, 23, 13, 39, 22, 984, DateTimeKind.Local).AddTicks(6855) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 23, 13, 39, 22, 984, DateTimeKind.Local).AddTicks(6861), new DateTime(2024, 9, 23, 13, 39, 22, 984, DateTimeKind.Local).AddTicks(6862) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 23, 13, 39, 22, 984, DateTimeKind.Local).AddTicks(6868), new DateTime(2024, 9, 23, 13, 39, 22, 984, DateTimeKind.Local).AddTicks(6869) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MobileVendor",
                table: "SochitelTransactions");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "SochitelTransactions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 23, 13, 33, 24, 673, DateTimeKind.Local).AddTicks(8189),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 23, 13, 39, 22, 983, DateTimeKind.Local).AddTicks(5282));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 23, 13, 33, 24, 673, DateTimeKind.Local).AddTicks(7499),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 23, 13, 39, 22, 983, DateTimeKind.Local).AddTicks(4930));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 23, 13, 33, 24, 660, DateTimeKind.Local).AddTicks(6162),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 23, 13, 39, 22, 975, DateTimeKind.Local).AddTicks(7421));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 23, 13, 33, 24, 604, DateTimeKind.Local).AddTicks(9103),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 23, 13, 39, 22, 944, DateTimeKind.Local).AddTicks(7941));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 23, 13, 33, 24, 602, DateTimeKind.Local).AddTicks(870),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 23, 13, 39, 22, 942, DateTimeKind.Local).AddTicks(5980));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "74453411-8ee4-4d7f-8a51-b64557f6f3e4", "AQAAAAEAACcQAAAAEIOm3Dmn4+b//drdsY8Zjso/eOPxtB0VH2FW+zGlssxviCrc8xrabCqEUfe3OckdEg==", "25b38977-32ca-4625-a1a7-e082d26136c4" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 23, 13, 33, 24, 650, DateTimeKind.Local).AddTicks(9565), new DateTime(2024, 9, 23, 13, 33, 24, 651, DateTimeKind.Local).AddTicks(272) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 23, 13, 33, 24, 675, DateTimeKind.Local).AddTicks(5077), new DateTime(2024, 9, 23, 13, 33, 24, 675, DateTimeKind.Local).AddTicks(5527) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 23, 13, 33, 24, 675, DateTimeKind.Local).AddTicks(6142), new DateTime(2024, 9, 23, 13, 33, 24, 675, DateTimeKind.Local).AddTicks(6147) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 23, 13, 33, 24, 675, DateTimeKind.Local).AddTicks(6158), new DateTime(2024, 9, 23, 13, 33, 24, 675, DateTimeKind.Local).AddTicks(6159) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 23, 13, 33, 24, 675, DateTimeKind.Local).AddTicks(6165), new DateTime(2024, 9, 23, 13, 33, 24, 675, DateTimeKind.Local).AddTicks(6166) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 23, 13, 33, 24, 675, DateTimeKind.Local).AddTicks(6172), new DateTime(2024, 9, 23, 13, 33, 24, 675, DateTimeKind.Local).AddTicks(6173) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 23, 13, 33, 24, 675, DateTimeKind.Local).AddTicks(6184), new DateTime(2024, 9, 23, 13, 33, 24, 675, DateTimeKind.Local).AddTicks(6186) });
        }
    }
}
