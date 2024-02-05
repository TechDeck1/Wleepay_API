using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class CreatedNewTableForCashFlowHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 5, 14, 57, 57, 858, DateTimeKind.Local).AddTicks(1134),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 1, 11, 14, 7, 471, DateTimeKind.Local).AddTicks(1135));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 5, 14, 57, 57, 858, DateTimeKind.Local).AddTicks(542),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 1, 11, 14, 7, 471, DateTimeKind.Local).AddTicks(737));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TransferFee",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TransferFee",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 5, 14, 57, 57, 850, DateTimeKind.Local).AddTicks(3245),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 1, 11, 14, 7, 462, DateTimeKind.Local).AddTicks(1185));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 5, 14, 57, 57, 819, DateTimeKind.Local).AddTicks(6253),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 1, 11, 14, 7, 429, DateTimeKind.Local).AddTicks(778));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 5, 14, 57, 57, 817, DateTimeKind.Local).AddTicks(5093),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 1, 11, 14, 7, 426, DateTimeKind.Local).AddTicks(2566));

            migrationBuilder.CreateTable(
                name: "CashFlowHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WalletId = table.Column<long>(type: "bigint", nullable: true),
                    TotalAmount = table.Column<double>(type: "float", nullable: false),
                    DepositAmount = table.Column<double>(type: "float", nullable: true),
                    WithdrawAmount = table.Column<double>(type: "float", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true),
                    TransactionStatusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashFlowHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashFlowHistory_TransactionStatus_TransactionStatusId",
                        column: x => x.TransactionStatusId,
                        principalTable: "TransactionStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashFlowHistory_Wallet_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallet",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_CashFlowHistory_TransactionStatusId",
                table: "CashFlowHistory",
                column: "TransactionStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CashFlowHistory_WalletId",
                table: "CashFlowHistory",
                column: "WalletId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CashFlowHistory");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TransferFee");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TransferFee");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 1, 11, 14, 7, 471, DateTimeKind.Local).AddTicks(1135),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 5, 14, 57, 57, 858, DateTimeKind.Local).AddTicks(1134));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 1, 11, 14, 7, 471, DateTimeKind.Local).AddTicks(737),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 5, 14, 57, 57, 858, DateTimeKind.Local).AddTicks(542));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 1, 11, 14, 7, 462, DateTimeKind.Local).AddTicks(1185),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 5, 14, 57, 57, 850, DateTimeKind.Local).AddTicks(3245));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 1, 11, 14, 7, 429, DateTimeKind.Local).AddTicks(778),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 5, 14, 57, 57, 819, DateTimeKind.Local).AddTicks(6253));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 1, 11, 14, 7, 426, DateTimeKind.Local).AddTicks(2566),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 5, 14, 57, 57, 817, DateTimeKind.Local).AddTicks(5093));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b4ed6399-7991-495e-8021-16ccbd75d086", "AQAAAAEAACcQAAAAEPuFhxXjqBNcepCrD8uMr6XLg27hFe4v98GQox+8F+U7RtJWQuywdpNCvIgWPLY5+g==", "c0afcfe3-065b-4938-849d-eafbccff9d91" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 1, 11, 14, 7, 456, DateTimeKind.Local).AddTicks(9887), new DateTime(2024, 2, 1, 11, 14, 7, 457, DateTimeKind.Local).AddTicks(363) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 1, 11, 14, 7, 472, DateTimeKind.Local).AddTicks(4863), new DateTime(2024, 2, 1, 11, 14, 7, 472, DateTimeKind.Local).AddTicks(5060) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 1, 11, 14, 7, 472, DateTimeKind.Local).AddTicks(5337), new DateTime(2024, 2, 1, 11, 14, 7, 472, DateTimeKind.Local).AddTicks(5340) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 1, 11, 14, 7, 472, DateTimeKind.Local).AddTicks(5346), new DateTime(2024, 2, 1, 11, 14, 7, 472, DateTimeKind.Local).AddTicks(5347) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 1, 11, 14, 7, 472, DateTimeKind.Local).AddTicks(5351), new DateTime(2024, 2, 1, 11, 14, 7, 472, DateTimeKind.Local).AddTicks(5352) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 1, 11, 14, 7, 472, DateTimeKind.Local).AddTicks(5355), new DateTime(2024, 2, 1, 11, 14, 7, 472, DateTimeKind.Local).AddTicks(5356) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 1, 11, 14, 7, 472, DateTimeKind.Local).AddTicks(5361), new DateTime(2024, 2, 1, 11, 14, 7, 472, DateTimeKind.Local).AddTicks(5362) });
        }
    }
}
