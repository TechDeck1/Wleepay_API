using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class UpdateSochitelTransactionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 23, 13, 33, 24, 673, DateTimeKind.Local).AddTicks(8189),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 23, 11, 34, 26, 681, DateTimeKind.Local).AddTicks(9326));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 23, 13, 33, 24, 673, DateTimeKind.Local).AddTicks(7499),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 23, 11, 34, 26, 681, DateTimeKind.Local).AddTicks(8936));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 23, 13, 33, 24, 660, DateTimeKind.Local).AddTicks(6162),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 23, 11, 34, 26, 673, DateTimeKind.Local).AddTicks(9034));

            migrationBuilder.AddColumn<string>(
                name: "MeterNumber",
                table: "SochitelTransactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 23, 13, 33, 24, 604, DateTimeKind.Local).AddTicks(9103),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 23, 11, 34, 26, 644, DateTimeKind.Local).AddTicks(3457));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 23, 13, 33, 24, 602, DateTimeKind.Local).AddTicks(870),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 23, 11, 34, 26, 641, DateTimeKind.Local).AddTicks(1501));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeterNumber",
                table: "SochitelTransactions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 23, 11, 34, 26, 681, DateTimeKind.Local).AddTicks(9326),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 23, 13, 33, 24, 673, DateTimeKind.Local).AddTicks(8189));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 23, 11, 34, 26, 681, DateTimeKind.Local).AddTicks(8936),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 23, 13, 33, 24, 673, DateTimeKind.Local).AddTicks(7499));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 23, 11, 34, 26, 673, DateTimeKind.Local).AddTicks(9034),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 23, 13, 33, 24, 660, DateTimeKind.Local).AddTicks(6162));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 23, 11, 34, 26, 644, DateTimeKind.Local).AddTicks(3457),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 23, 13, 33, 24, 604, DateTimeKind.Local).AddTicks(9103));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 23, 11, 34, 26, 641, DateTimeKind.Local).AddTicks(1501),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 23, 13, 33, 24, 602, DateTimeKind.Local).AddTicks(870));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ba060db4-2211-4628-afec-93063e9d4391", "AQAAAAEAACcQAAAAEFjuUmCMRuypCvDvEXjCiaawSUlMi4byCIzLQU3EOatLHe2XxQPz6DqXCr+1qY5yWg==", "6c3ed0f9-cce9-4ee7-8811-d266b56fd007" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 23, 11, 34, 26, 667, DateTimeKind.Local).AddTicks(8967), new DateTime(2024, 9, 23, 11, 34, 26, 667, DateTimeKind.Local).AddTicks(9314) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 23, 11, 34, 26, 683, DateTimeKind.Local).AddTicks(1363), new DateTime(2024, 9, 23, 11, 34, 26, 683, DateTimeKind.Local).AddTicks(1645) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 23, 11, 34, 26, 683, DateTimeKind.Local).AddTicks(2041), new DateTime(2024, 9, 23, 11, 34, 26, 683, DateTimeKind.Local).AddTicks(2046) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 23, 11, 34, 26, 683, DateTimeKind.Local).AddTicks(2053), new DateTime(2024, 9, 23, 11, 34, 26, 683, DateTimeKind.Local).AddTicks(2054) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 23, 11, 34, 26, 683, DateTimeKind.Local).AddTicks(2059), new DateTime(2024, 9, 23, 11, 34, 26, 683, DateTimeKind.Local).AddTicks(2060) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 23, 11, 34, 26, 683, DateTimeKind.Local).AddTicks(2063), new DateTime(2024, 9, 23, 11, 34, 26, 683, DateTimeKind.Local).AddTicks(2065) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 23, 11, 34, 26, 683, DateTimeKind.Local).AddTicks(2071), new DateTime(2024, 9, 23, 11, 34, 26, 683, DateTimeKind.Local).AddTicks(2072) });
        }
    }
}
