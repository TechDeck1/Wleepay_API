using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class UpdateTransaferTableForInHand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 17, 15, 31, 18, 415, DateTimeKind.Local).AddTicks(4303),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 17, 13, 42, 35, 492, DateTimeKind.Local).AddTicks(9226));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 17, 15, 31, 18, 415, DateTimeKind.Local).AddTicks(3906),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 17, 13, 42, 35, 492, DateTimeKind.Local).AddTicks(8805));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 17, 15, 31, 18, 406, DateTimeKind.Local).AddTicks(6783),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 17, 13, 42, 35, 483, DateTimeKind.Local).AddTicks(8303));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 17, 15, 31, 18, 376, DateTimeKind.Local).AddTicks(3448),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 17, 13, 42, 35, 458, DateTimeKind.Local).AddTicks(5000));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 17, 15, 31, 18, 373, DateTimeKind.Local).AddTicks(5343),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 17, 13, 42, 35, 456, DateTimeKind.Local).AddTicks(7875));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7470beb0-5387-431b-9b37-6dc760b1318a", "AQAAAAEAACcQAAAAEIeVU+KxJPPrAXIFgxOpQd1p4J32DTozwmWjBxc71sVbvBluMPvu2xhznZgvEXDzsg==", "7ed1f4e7-fe1c-4e0d-8bd5-345aef97c82d" });

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "FromWalletType", "Name", "ToWalletType" },
                values: new object[] { null, "In hand Credit", 1 });

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "FromWalletType", "Name", "ToWalletType" },
                values: new object[] { "Transfer from agent a/c to sytem wallet a/c", 6, "Agent Cash Out", 9 });

            migrationBuilder.InsertData(
                table: "TransferType",
                columns: new[] { "Id", "Description", "FromWalletType", "Name", "ToWalletType", "TransferFee" },
                values: new object[] { 12, "Transfer from agent a/c to general cash account float a/c", 1, "In hand Withdrawl", null, null });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 17, 15, 31, 18, 400, DateTimeKind.Local).AddTicks(7868), new DateTime(2024, 9, 17, 15, 31, 18, 400, DateTimeKind.Local).AddTicks(8195) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 17, 15, 31, 18, 416, DateTimeKind.Local).AddTicks(6679), new DateTime(2024, 9, 17, 15, 31, 18, 416, DateTimeKind.Local).AddTicks(7127) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 17, 15, 31, 18, 416, DateTimeKind.Local).AddTicks(7544), new DateTime(2024, 9, 17, 15, 31, 18, 416, DateTimeKind.Local).AddTicks(7548) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 17, 15, 31, 18, 416, DateTimeKind.Local).AddTicks(7555), new DateTime(2024, 9, 17, 15, 31, 18, 416, DateTimeKind.Local).AddTicks(7556) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 17, 15, 31, 18, 416, DateTimeKind.Local).AddTicks(7561), new DateTime(2024, 9, 17, 15, 31, 18, 416, DateTimeKind.Local).AddTicks(7561) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 17, 15, 31, 18, 416, DateTimeKind.Local).AddTicks(7567), new DateTime(2024, 9, 17, 15, 31, 18, 416, DateTimeKind.Local).AddTicks(7568) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 17, 15, 31, 18, 416, DateTimeKind.Local).AddTicks(7575), new DateTime(2024, 9, 17, 15, 31, 18, 416, DateTimeKind.Local).AddTicks(7576) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 17, 13, 42, 35, 492, DateTimeKind.Local).AddTicks(9226),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 17, 15, 31, 18, 415, DateTimeKind.Local).AddTicks(4303));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 17, 13, 42, 35, 492, DateTimeKind.Local).AddTicks(8805),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 17, 15, 31, 18, 415, DateTimeKind.Local).AddTicks(3906));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 17, 13, 42, 35, 483, DateTimeKind.Local).AddTicks(8303),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 17, 15, 31, 18, 406, DateTimeKind.Local).AddTicks(6783));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 17, 13, 42, 35, 458, DateTimeKind.Local).AddTicks(5000),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 17, 15, 31, 18, 376, DateTimeKind.Local).AddTicks(3448));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 17, 13, 42, 35, 456, DateTimeKind.Local).AddTicks(7875),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 17, 15, 31, 18, 373, DateTimeKind.Local).AddTicks(5343));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cb981778-09fa-434a-b83b-8c6f868c6a70", "AQAAAAEAACcQAAAAEAgb1RbGr01sDcYR6YGqlyCUOXpYt++JU+0hkQe5Nm3uojjbjnViQpRF45VwmsKCdQ==", "22f13536-ee37-4ed1-9593-13068c534f65" });

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "FromWalletType", "Name", "ToWalletType" },
                values: new object[] { 1, "In hand Withdrawl", null });

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "FromWalletType", "Name", "ToWalletType" },
                values: new object[] { "Transfer from agent a/c to general cash account float a/c", null, "In hand Credit", 1 });

            migrationBuilder.InsertData(
                table: "TransferType",
                columns: new[] { "Id", "Description", "FromWalletType", "Name", "ToWalletType", "TransferFee" },
                values: new object[] { 15, "Transfer from agent a/c to sytem wallet a/c", 6, "Agent Cash Out", 9, null });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 17, 13, 42, 35, 478, DateTimeKind.Local).AddTicks(28), new DateTime(2024, 9, 17, 13, 42, 35, 478, DateTimeKind.Local).AddTicks(343) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 17, 13, 42, 35, 494, DateTimeKind.Local).AddTicks(813), new DateTime(2024, 9, 17, 13, 42, 35, 494, DateTimeKind.Local).AddTicks(1093) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 17, 13, 42, 35, 494, DateTimeKind.Local).AddTicks(1469), new DateTime(2024, 9, 17, 13, 42, 35, 494, DateTimeKind.Local).AddTicks(1472) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 17, 13, 42, 35, 494, DateTimeKind.Local).AddTicks(1480), new DateTime(2024, 9, 17, 13, 42, 35, 494, DateTimeKind.Local).AddTicks(1481) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 17, 13, 42, 35, 494, DateTimeKind.Local).AddTicks(1485), new DateTime(2024, 9, 17, 13, 42, 35, 494, DateTimeKind.Local).AddTicks(1486) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 17, 13, 42, 35, 494, DateTimeKind.Local).AddTicks(1491), new DateTime(2024, 9, 17, 13, 42, 35, 494, DateTimeKind.Local).AddTicks(1492) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 17, 13, 42, 35, 494, DateTimeKind.Local).AddTicks(1550), new DateTime(2024, 9, 17, 13, 42, 35, 494, DateTimeKind.Local).AddTicks(1551) });
        }
    }
}
