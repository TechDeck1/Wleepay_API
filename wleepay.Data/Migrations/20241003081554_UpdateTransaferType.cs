using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class UpdateTransaferType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 3, 13, 45, 53, 26, DateTimeKind.Local).AddTicks(1656),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 3, 13, 44, 13, 743, DateTimeKind.Local).AddTicks(6944));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 3, 13, 45, 53, 26, DateTimeKind.Local).AddTicks(1327),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 3, 13, 44, 13, 743, DateTimeKind.Local).AddTicks(6245));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 3, 13, 45, 53, 18, DateTimeKind.Local).AddTicks(4707),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 3, 13, 44, 13, 730, DateTimeKind.Local).AddTicks(5969));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 3, 13, 45, 52, 994, DateTimeKind.Local).AddTicks(8488),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 3, 13, 44, 13, 688, DateTimeKind.Local).AddTicks(1585));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 3, 13, 45, 52, 993, DateTimeKind.Local).AddTicks(2531),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 3, 13, 44, 13, 684, DateTimeKind.Local).AddTicks(6434));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2de81a21-0594-447f-9615-505eabc8bf88", "AQAAAAEAACcQAAAAENuGjA4QLMlxY2lE1EKPSqhE7/xR7TKNshduFkbQcz6SMo3Y3WjEB3c4FUnUGNMQXw==", "b3342669-3fa5-4642-98aa-3fcd4ec5d580" });

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "FromWalletType", "ToWalletType" },
                values: new object[] { 9, 7 });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 3, 13, 45, 53, 12, DateTimeKind.Local).AddTicks(6707), new DateTime(2024, 10, 3, 13, 45, 53, 12, DateTimeKind.Local).AddTicks(7056) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 3, 13, 45, 53, 27, DateTimeKind.Local).AddTicks(2517), new DateTime(2024, 10, 3, 13, 45, 53, 27, DateTimeKind.Local).AddTicks(2798) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 3, 13, 45, 53, 27, DateTimeKind.Local).AddTicks(3182), new DateTime(2024, 10, 3, 13, 45, 53, 27, DateTimeKind.Local).AddTicks(3186) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 3, 13, 45, 53, 27, DateTimeKind.Local).AddTicks(3193), new DateTime(2024, 10, 3, 13, 45, 53, 27, DateTimeKind.Local).AddTicks(3194) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 3, 13, 45, 53, 27, DateTimeKind.Local).AddTicks(3199), new DateTime(2024, 10, 3, 13, 45, 53, 27, DateTimeKind.Local).AddTicks(3200) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 3, 13, 45, 53, 27, DateTimeKind.Local).AddTicks(3204), new DateTime(2024, 10, 3, 13, 45, 53, 27, DateTimeKind.Local).AddTicks(3205) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 3, 13, 45, 53, 27, DateTimeKind.Local).AddTicks(3212), new DateTime(2024, 10, 3, 13, 45, 53, 27, DateTimeKind.Local).AddTicks(3213) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 3, 13, 45, 53, 27, DateTimeKind.Local).AddTicks(3217), new DateTime(2024, 10, 3, 13, 45, 53, 27, DateTimeKind.Local).AddTicks(3217) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 3, 13, 45, 53, 27, DateTimeKind.Local).AddTicks(3221), new DateTime(2024, 10, 3, 13, 45, 53, 27, DateTimeKind.Local).AddTicks(3222) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 3, 13, 44, 13, 743, DateTimeKind.Local).AddTicks(6944),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 3, 13, 45, 53, 26, DateTimeKind.Local).AddTicks(1656));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 3, 13, 44, 13, 743, DateTimeKind.Local).AddTicks(6245),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 3, 13, 45, 53, 26, DateTimeKind.Local).AddTicks(1327));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 3, 13, 44, 13, 730, DateTimeKind.Local).AddTicks(5969),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 3, 13, 45, 53, 18, DateTimeKind.Local).AddTicks(4707));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 3, 13, 44, 13, 688, DateTimeKind.Local).AddTicks(1585),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 3, 13, 45, 52, 994, DateTimeKind.Local).AddTicks(8488));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 3, 13, 44, 13, 684, DateTimeKind.Local).AddTicks(6434),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 3, 13, 45, 52, 993, DateTimeKind.Local).AddTicks(2531));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0b558cd4-bb98-44b3-b700-006960f0490f", "AQAAAAEAACcQAAAAEGyZanf1BZ392iE3dU2GfiOZREXgqwTi5JUVyNJADKxitJFHfVJZgnAt1eshP92VMg==", "fa33f3c3-7634-462d-a033-cc0c00325bf1" });

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "FromWalletType", "ToWalletType" },
                values: new object[] { 7, 9 });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 3, 13, 44, 13, 721, DateTimeKind.Local).AddTicks(5861), new DateTime(2024, 10, 3, 13, 44, 13, 721, DateTimeKind.Local).AddTicks(6616) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 3, 13, 44, 13, 745, DateTimeKind.Local).AddTicks(3313), new DateTime(2024, 10, 3, 13, 44, 13, 745, DateTimeKind.Local).AddTicks(3684) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 3, 13, 44, 13, 745, DateTimeKind.Local).AddTicks(4320), new DateTime(2024, 10, 3, 13, 44, 13, 745, DateTimeKind.Local).AddTicks(4327) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 3, 13, 44, 13, 745, DateTimeKind.Local).AddTicks(4337), new DateTime(2024, 10, 3, 13, 44, 13, 745, DateTimeKind.Local).AddTicks(4339) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 3, 13, 44, 13, 745, DateTimeKind.Local).AddTicks(4343), new DateTime(2024, 10, 3, 13, 44, 13, 745, DateTimeKind.Local).AddTicks(4344) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 3, 13, 44, 13, 745, DateTimeKind.Local).AddTicks(4350), new DateTime(2024, 10, 3, 13, 44, 13, 745, DateTimeKind.Local).AddTicks(4351) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 3, 13, 44, 13, 745, DateTimeKind.Local).AddTicks(4360), new DateTime(2024, 10, 3, 13, 44, 13, 745, DateTimeKind.Local).AddTicks(4362) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 3, 13, 44, 13, 745, DateTimeKind.Local).AddTicks(4366), new DateTime(2024, 10, 3, 13, 44, 13, 745, DateTimeKind.Local).AddTicks(4367) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 3, 13, 44, 13, 745, DateTimeKind.Local).AddTicks(4394), new DateTime(2024, 10, 3, 13, 44, 13, 745, DateTimeKind.Local).AddTicks(4397) });
        }
    }
}
