using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class updateCashFlowHistoryTableForRemoveForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashFlowHistory_Currencies_CurrencyId",
                table: "CashFlowHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_CashFlowHistory_TransactionStatus_TransactionStatusId",
                table: "CashFlowHistory");

            migrationBuilder.DropIndex(
                name: "IX_CashFlowHistory_CurrencyId",
                table: "CashFlowHistory");

            migrationBuilder.DropIndex(
                name: "IX_CashFlowHistory_TransactionStatusId",
                table: "CashFlowHistory");

            migrationBuilder.DropColumn(
                name: "TransactionStatusId",
                table: "CashFlowHistory");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 13, 39, 5, 115, DateTimeKind.Local).AddTicks(6333),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 12, 29, 35, 429, DateTimeKind.Local).AddTicks(3800));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 13, 39, 5, 115, DateTimeKind.Local).AddTicks(5897),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 12, 29, 35, 429, DateTimeKind.Local).AddTicks(3385));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 13, 39, 5, 109, DateTimeKind.Local).AddTicks(6835),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 12, 29, 35, 423, DateTimeKind.Local).AddTicks(4365));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 13, 39, 5, 65, DateTimeKind.Local).AddTicks(518),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 12, 29, 35, 403, DateTimeKind.Local).AddTicks(3753));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 13, 39, 5, 62, DateTimeKind.Local).AddTicks(5636),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 12, 29, 35, 402, DateTimeKind.Local).AddTicks(39));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "70d364e0-d3a0-42b1-9ca1-5d2eb871c4a0", "AQAAAAEAACcQAAAAEG/k0eWQE71WD88wLMmKJiY/9VKuggpi/RyD5SOBhZTL4eddr88n0jFCbk4oqyBcew==", "bfee7eee-43c5-4b54-aeb8-3e60e2400499" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 13, 39, 5, 102, DateTimeKind.Local).AddTicks(1191), new DateTime(2024, 2, 7, 13, 39, 5, 102, DateTimeKind.Local).AddTicks(1405) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 13, 39, 5, 116, DateTimeKind.Local).AddTicks(5512), new DateTime(2024, 2, 7, 13, 39, 5, 116, DateTimeKind.Local).AddTicks(5687) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 13, 39, 5, 116, DateTimeKind.Local).AddTicks(5933), new DateTime(2024, 2, 7, 13, 39, 5, 116, DateTimeKind.Local).AddTicks(5938) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 13, 39, 5, 116, DateTimeKind.Local).AddTicks(5944), new DateTime(2024, 2, 7, 13, 39, 5, 116, DateTimeKind.Local).AddTicks(5945) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 13, 39, 5, 116, DateTimeKind.Local).AddTicks(6012), new DateTime(2024, 2, 7, 13, 39, 5, 116, DateTimeKind.Local).AddTicks(6013) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 13, 39, 5, 116, DateTimeKind.Local).AddTicks(6018), new DateTime(2024, 2, 7, 13, 39, 5, 116, DateTimeKind.Local).AddTicks(6019) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 13, 39, 5, 116, DateTimeKind.Local).AddTicks(6024), new DateTime(2024, 2, 7, 13, 39, 5, 116, DateTimeKind.Local).AddTicks(6025) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 12, 29, 35, 429, DateTimeKind.Local).AddTicks(3800),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 13, 39, 5, 115, DateTimeKind.Local).AddTicks(6333));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 12, 29, 35, 429, DateTimeKind.Local).AddTicks(3385),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 13, 39, 5, 115, DateTimeKind.Local).AddTicks(5897));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 12, 29, 35, 423, DateTimeKind.Local).AddTicks(4365),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 13, 39, 5, 109, DateTimeKind.Local).AddTicks(6835));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 12, 29, 35, 403, DateTimeKind.Local).AddTicks(3753),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 13, 39, 5, 65, DateTimeKind.Local).AddTicks(518));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 12, 29, 35, 402, DateTimeKind.Local).AddTicks(39),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 13, 39, 5, 62, DateTimeKind.Local).AddTicks(5636));

            migrationBuilder.AddColumn<int>(
                name: "TransactionStatusId",
                table: "CashFlowHistory",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e0fc7c0d-8115-496e-952b-fe9600d91b8f", "AQAAAAEAACcQAAAAEGaSlcGdev0Gxo3D/Js2l7EXJFQm28WO08XgoEONhLpHgbQumAX+/lGm99JloESW6Q==", "e3b5e657-cc65-4e17-ae5d-fc35fc7aef66" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 12, 29, 35, 419, DateTimeKind.Local).AddTicks(261), new DateTime(2024, 2, 7, 12, 29, 35, 419, DateTimeKind.Local).AddTicks(458) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 12, 29, 35, 430, DateTimeKind.Local).AddTicks(3351), new DateTime(2024, 2, 7, 12, 29, 35, 430, DateTimeKind.Local).AddTicks(3575) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 12, 29, 35, 430, DateTimeKind.Local).AddTicks(3859), new DateTime(2024, 2, 7, 12, 29, 35, 430, DateTimeKind.Local).AddTicks(3863) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 12, 29, 35, 430, DateTimeKind.Local).AddTicks(3869), new DateTime(2024, 2, 7, 12, 29, 35, 430, DateTimeKind.Local).AddTicks(3870) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 12, 29, 35, 430, DateTimeKind.Local).AddTicks(3874), new DateTime(2024, 2, 7, 12, 29, 35, 430, DateTimeKind.Local).AddTicks(3875) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 12, 29, 35, 430, DateTimeKind.Local).AddTicks(3926), new DateTime(2024, 2, 7, 12, 29, 35, 430, DateTimeKind.Local).AddTicks(3927) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 12, 29, 35, 430, DateTimeKind.Local).AddTicks(3933), new DateTime(2024, 2, 7, 12, 29, 35, 430, DateTimeKind.Local).AddTicks(3935) });

            migrationBuilder.CreateIndex(
                name: "IX_CashFlowHistory_CurrencyId",
                table: "CashFlowHistory",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CashFlowHistory_TransactionStatusId",
                table: "CashFlowHistory",
                column: "TransactionStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_CashFlowHistory_Currencies_CurrencyId",
                table: "CashFlowHistory",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CashFlowHistory_TransactionStatus_TransactionStatusId",
                table: "CashFlowHistory",
                column: "TransactionStatusId",
                principalTable: "TransactionStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
