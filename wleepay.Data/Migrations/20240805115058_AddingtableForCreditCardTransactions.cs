using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class AddingtableForCreditCardTransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 5, 17, 20, 57, 420, DateTimeKind.Local).AddTicks(873),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 1, 11, 48, 42, 854, DateTimeKind.Local).AddTicks(5467));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 5, 17, 20, 57, 420, DateTimeKind.Local).AddTicks(436),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 1, 11, 48, 42, 854, DateTimeKind.Local).AddTicks(5030));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 5, 17, 20, 57, 410, DateTimeKind.Local).AddTicks(8238),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 1, 11, 48, 42, 846, DateTimeKind.Local).AddTicks(4013));

            migrationBuilder.AddColumn<int>(
                name: "CreditCardTransactionId",
                table: "SochitelTransactions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraInfo",
                table: "SochitelTransactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WalletTransactionId",
                table: "SochitelTransactions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 5, 17, 20, 57, 376, DateTimeKind.Local).AddTicks(8697),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 1, 11, 48, 42, 822, DateTimeKind.Local).AddTicks(1004));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 5, 17, 20, 57, 373, DateTimeKind.Local).AddTicks(8070),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 1, 11, 48, 42, 819, DateTimeKind.Local).AddTicks(9769));

            migrationBuilder.CreateTable(
                name: "CreditCardTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    StripeTransactionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BalanceTransactionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCardTransactions", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f6c534e8-4a7c-482a-a0df-08e2387783e6", "AQAAAAEAACcQAAAAEHKBLeIx8lsFXXdId1Nrye28/aF+Yutue0qmboBNz5sUmppo0mhO8VIAJHzHspc1iQ==", "c2514672-99eb-4240-8ff5-b9c8bfb5edf4" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 5, 17, 20, 57, 403, DateTimeKind.Local).AddTicks(9115), new DateTime(2024, 8, 5, 17, 20, 57, 403, DateTimeKind.Local).AddTicks(9453) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 5, 17, 20, 57, 421, DateTimeKind.Local).AddTicks(4438), new DateTime(2024, 8, 5, 17, 20, 57, 421, DateTimeKind.Local).AddTicks(4789) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 5, 17, 20, 57, 421, DateTimeKind.Local).AddTicks(5212), new DateTime(2024, 8, 5, 17, 20, 57, 421, DateTimeKind.Local).AddTicks(5217) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 5, 17, 20, 57, 421, DateTimeKind.Local).AddTicks(5225), new DateTime(2024, 8, 5, 17, 20, 57, 421, DateTimeKind.Local).AddTicks(5226) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 5, 17, 20, 57, 421, DateTimeKind.Local).AddTicks(5231), new DateTime(2024, 8, 5, 17, 20, 57, 421, DateTimeKind.Local).AddTicks(5232) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 5, 17, 20, 57, 421, DateTimeKind.Local).AddTicks(5237), new DateTime(2024, 8, 5, 17, 20, 57, 421, DateTimeKind.Local).AddTicks(5238) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 5, 17, 20, 57, 421, DateTimeKind.Local).AddTicks(5244), new DateTime(2024, 8, 5, 17, 20, 57, 421, DateTimeKind.Local).AddTicks(5245) });

            migrationBuilder.CreateIndex(
                name: "IX_SochitelTransactions_CreditCardTransactionId",
                table: "SochitelTransactions",
                column: "CreditCardTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_SochitelTransactions_WalletTransactionId",
                table: "SochitelTransactions",
                column: "WalletTransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SochitelTransactions_CreditCardTransactions_CreditCardTransactionId",
                table: "SochitelTransactions",
                column: "CreditCardTransactionId",
                principalTable: "CreditCardTransactions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SochitelTransactions_Transaction_WalletTransactionId",
                table: "SochitelTransactions",
                column: "WalletTransactionId",
                principalTable: "Transaction",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SochitelTransactions_CreditCardTransactions_CreditCardTransactionId",
                table: "SochitelTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_SochitelTransactions_Transaction_WalletTransactionId",
                table: "SochitelTransactions");

            migrationBuilder.DropTable(
                name: "CreditCardTransactions");

            migrationBuilder.DropIndex(
                name: "IX_SochitelTransactions_CreditCardTransactionId",
                table: "SochitelTransactions");

            migrationBuilder.DropIndex(
                name: "IX_SochitelTransactions_WalletTransactionId",
                table: "SochitelTransactions");

            migrationBuilder.DropColumn(
                name: "CreditCardTransactionId",
                table: "SochitelTransactions");

            migrationBuilder.DropColumn(
                name: "ExtraInfo",
                table: "SochitelTransactions");

            migrationBuilder.DropColumn(
                name: "WalletTransactionId",
                table: "SochitelTransactions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 1, 11, 48, 42, 854, DateTimeKind.Local).AddTicks(5467),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 5, 17, 20, 57, 420, DateTimeKind.Local).AddTicks(873));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 1, 11, 48, 42, 854, DateTimeKind.Local).AddTicks(5030),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 5, 17, 20, 57, 420, DateTimeKind.Local).AddTicks(436));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 1, 11, 48, 42, 846, DateTimeKind.Local).AddTicks(4013),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 5, 17, 20, 57, 410, DateTimeKind.Local).AddTicks(8238));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 1, 11, 48, 42, 822, DateTimeKind.Local).AddTicks(1004),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 5, 17, 20, 57, 376, DateTimeKind.Local).AddTicks(8697));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 1, 11, 48, 42, 819, DateTimeKind.Local).AddTicks(9769),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 5, 17, 20, 57, 373, DateTimeKind.Local).AddTicks(8070));

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
    }
}
