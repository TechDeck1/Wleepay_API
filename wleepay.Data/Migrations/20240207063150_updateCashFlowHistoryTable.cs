using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class updateCashFlowHistoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 12, 1, 50, 74, DateTimeKind.Local).AddTicks(7053),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 5, 15, 9, 22, 677, DateTimeKind.Local).AddTicks(6546));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 12, 1, 50, 74, DateTimeKind.Local).AddTicks(6592),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 5, 15, 9, 22, 677, DateTimeKind.Local).AddTicks(6031));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 12, 1, 50, 68, DateTimeKind.Local).AddTicks(5071),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 5, 15, 9, 22, 671, DateTimeKind.Local).AddTicks(1592));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 12, 1, 50, 47, DateTimeKind.Local).AddTicks(8810),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 5, 15, 9, 22, 644, DateTimeKind.Local).AddTicks(3349));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 12, 1, 50, 46, DateTimeKind.Local).AddTicks(4293),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 5, 15, 9, 22, 642, DateTimeKind.Local).AddTicks(6102));

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "CashFlowHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b3406e61-82f4-45be-81a4-01c435af7f28", "AQAAAAEAACcQAAAAEFbZFcaFaaRML+A3DFz5VROmvpPirimJsZ8cDuwqm18QrYnqyzwA3uvKt+Om+3llyQ==", "d8450e84-7736-4b3b-b8a0-af4c91333f6c" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 12, 1, 50, 64, DateTimeKind.Local).AddTicks(1247), new DateTime(2024, 2, 7, 12, 1, 50, 64, DateTimeKind.Local).AddTicks(1443) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 12, 1, 50, 75, DateTimeKind.Local).AddTicks(9260), new DateTime(2024, 2, 7, 12, 1, 50, 75, DateTimeKind.Local).AddTicks(9438) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 12, 1, 50, 75, DateTimeKind.Local).AddTicks(9718), new DateTime(2024, 2, 7, 12, 1, 50, 75, DateTimeKind.Local).AddTicks(9721) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 12, 1, 50, 75, DateTimeKind.Local).AddTicks(9728), new DateTime(2024, 2, 7, 12, 1, 50, 75, DateTimeKind.Local).AddTicks(9729) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 12, 1, 50, 75, DateTimeKind.Local).AddTicks(9733), new DateTime(2024, 2, 7, 12, 1, 50, 75, DateTimeKind.Local).AddTicks(9734) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 12, 1, 50, 75, DateTimeKind.Local).AddTicks(9782), new DateTime(2024, 2, 7, 12, 1, 50, 75, DateTimeKind.Local).AddTicks(9783) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 12, 1, 50, 75, DateTimeKind.Local).AddTicks(9790), new DateTime(2024, 2, 7, 12, 1, 50, 75, DateTimeKind.Local).AddTicks(9791) });

            migrationBuilder.CreateIndex(
                name: "IX_CashFlowHistory_CurrencyId",
                table: "CashFlowHistory",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_CashFlowHistory_Currencies_CurrencyId",
                table: "CashFlowHistory",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashFlowHistory_Currencies_CurrencyId",
                table: "CashFlowHistory");

            migrationBuilder.DropIndex(
                name: "IX_CashFlowHistory_CurrencyId",
                table: "CashFlowHistory");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "CashFlowHistory");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 5, 15, 9, 22, 677, DateTimeKind.Local).AddTicks(6546),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 12, 1, 50, 74, DateTimeKind.Local).AddTicks(7053));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 5, 15, 9, 22, 677, DateTimeKind.Local).AddTicks(6031),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 12, 1, 50, 74, DateTimeKind.Local).AddTicks(6592));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 5, 15, 9, 22, 671, DateTimeKind.Local).AddTicks(1592),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 12, 1, 50, 68, DateTimeKind.Local).AddTicks(5071));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 5, 15, 9, 22, 644, DateTimeKind.Local).AddTicks(3349),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 12, 1, 50, 47, DateTimeKind.Local).AddTicks(8810));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 5, 15, 9, 22, 642, DateTimeKind.Local).AddTicks(6102),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 12, 1, 50, 46, DateTimeKind.Local).AddTicks(4293));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a9b96067-888c-4971-922c-7abf5fdda3ef", "AQAAAAEAACcQAAAAEHZB8Thd+Me1IMzseSPKR64nEWGOOAl1ETYolhbvjyvNCPVp7O49r5dHlesaNRCUhw==", "0e17e767-1670-4e0c-8240-a6da44d09910" });

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
    }
}
