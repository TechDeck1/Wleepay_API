using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class InsertDataForCashFlowHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "CashFlowHistory");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 5, 15, 9, 22, 677, DateTimeKind.Local).AddTicks(6546),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 5, 14, 57, 57, 858, DateTimeKind.Local).AddTicks(1134));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 5, 15, 9, 22, 677, DateTimeKind.Local).AddTicks(6031),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 5, 14, 57, 57, 858, DateTimeKind.Local).AddTicks(542));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 5, 15, 9, 22, 671, DateTimeKind.Local).AddTicks(1592),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 5, 14, 57, 57, 850, DateTimeKind.Local).AddTicks(3245));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 5, 15, 9, 22, 644, DateTimeKind.Local).AddTicks(3349),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 5, 14, 57, 57, 819, DateTimeKind.Local).AddTicks(6253));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 5, 15, 9, 22, 642, DateTimeKind.Local).AddTicks(6102),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 5, 14, 57, 57, 817, DateTimeKind.Local).AddTicks(5093));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a9b96067-888c-4971-922c-7abf5fdda3ef", "AQAAAAEAACcQAAAAEHZB8Thd+Me1IMzseSPKR64nEWGOOAl1ETYolhbvjyvNCPVp7O49r5dHlesaNRCUhw==", "0e17e767-1670-4e0c-8240-a6da44d09910" });

            migrationBuilder.InsertData(
                table: "CashFlowHistory",
                columns: new[] { "Id", "CreatedDate", "DepositAmount", "TotalAmount", "TransactionStatusId", "WalletId", "WithdrawAmount" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0, 0.0, null, 1L, 0.0 });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 5, 15, 9, 22, 665, DateTimeKind.Local).AddTicks(8853), new DateTime(2024, 2, 5, 15, 9, 22, 665, DateTimeKind.Local).AddTicks(9058) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 5, 15, 9, 22, 678, DateTimeKind.Local).AddTicks(6557), new DateTime(2024, 2, 5, 15, 9, 22, 678, DateTimeKind.Local).AddTicks(6738) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 5, 15, 9, 22, 678, DateTimeKind.Local).AddTicks(7014), new DateTime(2024, 2, 5, 15, 9, 22, 678, DateTimeKind.Local).AddTicks(7017) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 5, 15, 9, 22, 678, DateTimeKind.Local).AddTicks(7023), new DateTime(2024, 2, 5, 15, 9, 22, 678, DateTimeKind.Local).AddTicks(7024) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 5, 15, 9, 22, 678, DateTimeKind.Local).AddTicks(7028), new DateTime(2024, 2, 5, 15, 9, 22, 678, DateTimeKind.Local).AddTicks(7029) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 5, 15, 9, 22, 678, DateTimeKind.Local).AddTicks(7032), new DateTime(2024, 2, 5, 15, 9, 22, 678, DateTimeKind.Local).AddTicks(7033) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 5, 15, 9, 22, 678, DateTimeKind.Local).AddTicks(7103), new DateTime(2024, 2, 5, 15, 9, 22, 678, DateTimeKind.Local).AddTicks(7104) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CashFlowHistory",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 5, 14, 57, 57, 858, DateTimeKind.Local).AddTicks(1134),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 5, 15, 9, 22, 677, DateTimeKind.Local).AddTicks(6546));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 5, 14, 57, 57, 858, DateTimeKind.Local).AddTicks(542),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 5, 15, 9, 22, 677, DateTimeKind.Local).AddTicks(6031));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 5, 14, 57, 57, 850, DateTimeKind.Local).AddTicks(3245),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 5, 15, 9, 22, 671, DateTimeKind.Local).AddTicks(1592));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 5, 14, 57, 57, 819, DateTimeKind.Local).AddTicks(6253),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 5, 15, 9, 22, 644, DateTimeKind.Local).AddTicks(3349));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 5, 14, 57, 57, 817, DateTimeKind.Local).AddTicks(5093),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 5, 15, 9, 22, 642, DateTimeKind.Local).AddTicks(6102));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "CashFlowHistory",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9be7dba0-4a12-4f79-8e45-e8f1728aac73", "AQAAAAEAACcQAAAAECmUABZy0nxx8b/znbo7unXXJETfIkt+m48TOUiIkiGefgIcknA64ztGsL60p1bPZg==", "9a0b04b8-a69f-4842-b582-ca30649b23d4" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 5, 14, 57, 57, 844, DateTimeKind.Local).AddTicks(414), new DateTime(2024, 2, 5, 14, 57, 57, 844, DateTimeKind.Local).AddTicks(608) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 5, 14, 57, 57, 859, DateTimeKind.Local).AddTicks(2291), new DateTime(2024, 2, 5, 14, 57, 57, 859, DateTimeKind.Local).AddTicks(2478) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 5, 14, 57, 57, 859, DateTimeKind.Local).AddTicks(2819), new DateTime(2024, 2, 5, 14, 57, 57, 859, DateTimeKind.Local).AddTicks(2822) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 5, 14, 57, 57, 859, DateTimeKind.Local).AddTicks(2829), new DateTime(2024, 2, 5, 14, 57, 57, 859, DateTimeKind.Local).AddTicks(2830) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 5, 14, 57, 57, 859, DateTimeKind.Local).AddTicks(2834), new DateTime(2024, 2, 5, 14, 57, 57, 859, DateTimeKind.Local).AddTicks(2835) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 5, 14, 57, 57, 859, DateTimeKind.Local).AddTicks(2838), new DateTime(2024, 2, 5, 14, 57, 57, 859, DateTimeKind.Local).AddTicks(2839) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 5, 14, 57, 57, 859, DateTimeKind.Local).AddTicks(2847), new DateTime(2024, 2, 5, 14, 57, 57, 859, DateTimeKind.Local).AddTicks(2847) });
        }
    }
}
