using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class UpdateDatabaseForTransactionAndTransferType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 9, 12, 24, 43, 863, DateTimeKind.Local).AddTicks(7757),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 466, DateTimeKind.Local).AddTicks(4683));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 9, 12, 24, 43, 863, DateTimeKind.Local).AddTicks(7453),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 466, DateTimeKind.Local).AddTicks(4328));

            migrationBuilder.AlterColumn<int>(
                name: "ToWalletType",
                table: "TransferType",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FromWalletType",
                table: "TransferType",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "ToWalletId",
                table: "Transaction",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "FromWalletId",
                table: "Transaction",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 9, 12, 24, 43, 856, DateTimeKind.Local).AddTicks(8456),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 459, DateTimeKind.Local).AddTicks(1281));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 9, 12, 24, 43, 835, DateTimeKind.Local).AddTicks(6300),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 433, DateTimeKind.Local).AddTicks(1209));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 9, 12, 24, 43, 834, DateTimeKind.Local).AddTicks(752),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 430, DateTimeKind.Local).AddTicks(5004));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "557d8b1b-47ae-455a-89dd-b7684b262d6d", "AQAAAAEAACcQAAAAEH41eKo3IptwlFUWMQq3A7mWds996XyU05pErDe51MvSgQFV5kf4xK6STeJZlLkv2w==", "704d6b4d-3bb0-41fd-b9c8-3af1484b1afd" });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "LRD");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "USD");

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Transfer to agent a/c from member a/c ");

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Transfer to agent commision a/c from memeber account");

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 6,
                column: "Description",
                value: "TopUp to member wallet from agent a/c");

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 7,
                column: "FromWalletType",
                value: 9);

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "ToWalletType" },
                values: new object[] { "Transfer to system float n a/c from Agent Commission a/c", 9 });

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 9,
                column: "Description",
                value: "Transfer from cash on hand a/c to sytem wallet");

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 10,
                column: "ToWalletType",
                value: 9);

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Name", "ToWalletType" },
                values: new object[] { "Bill Payment Transfer", 9 });

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "FromWalletType", "Name", "ToWalletType" },
                values: new object[] { 1, "In hand Withdrawl", null });

            migrationBuilder.InsertData(
                table: "TransferType",
                columns: new[] { "Id", "Description", "FromWalletType", "Name", "ToWalletType", "TransferFee" },
                values: new object[,]
                {
                    { 13, "Transfer from agent a/c to general cash account float a/c", null, "In hand Credit", 1, null },
                    { 16, "Transfer from member a/c to sytem wallet a/c", 7, "Member Cash Out By Wleepay", 9, null },
                    { 15, "Transfer from system a/c to member wallet a/c", 9, "Member Top Up By WleePay", 7, null },
                    { 14, "Transfer from agent a/c to sytem wallet a/c", 6, "Agent Cash Out", 9, null }
                });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Status", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 9, 12, 24, 43, 851, DateTimeKind.Local).AddTicks(4777), 1, new DateTime(2024, 10, 9, 12, 24, 43, 851, DateTimeKind.Local).AddTicks(5078) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 9, 12, 24, 43, 864, DateTimeKind.Local).AddTicks(7675), new DateTime(2024, 10, 9, 12, 24, 43, 864, DateTimeKind.Local).AddTicks(7933) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 9, 12, 24, 43, 864, DateTimeKind.Local).AddTicks(8283), new DateTime(2024, 10, 9, 12, 24, 43, 864, DateTimeKind.Local).AddTicks(8286) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 9, 12, 24, 43, 864, DateTimeKind.Local).AddTicks(8293), new DateTime(2024, 10, 9, 12, 24, 43, 864, DateTimeKind.Local).AddTicks(8294) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 9, 12, 24, 43, 864, DateTimeKind.Local).AddTicks(8298), new DateTime(2024, 10, 9, 12, 24, 43, 864, DateTimeKind.Local).AddTicks(8299) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 9, 12, 24, 43, 864, DateTimeKind.Local).AddTicks(8303), new DateTime(2024, 10, 9, 12, 24, 43, 864, DateTimeKind.Local).AddTicks(8304) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 9, 12, 24, 43, 864, DateTimeKind.Local).AddTicks(8310), new DateTime(2024, 10, 9, 12, 24, 43, 864, DateTimeKind.Local).AddTicks(8311) });

            migrationBuilder.InsertData(
                table: "Wallet",
                columns: new[] { "Id", "Balance", "CreatedDate", "CurrencyId", "Type", "UpdatedDate", "UserId" },
                values: new object[,]
                {
                    { 8L, 0.0, new DateTime(2024, 10, 9, 12, 24, 43, 864, DateTimeKind.Local).AddTicks(8319), 2, 9, new DateTime(2024, 10, 9, 12, 24, 43, 864, DateTimeKind.Local).AddTicks(8320), "07ac193d-26e7-4039-9440-fbb53988b878" },
                    { 7L, 0.0, new DateTime(2024, 10, 9, 12, 24, 43, 864, DateTimeKind.Local).AddTicks(8315), 1, 9, new DateTime(2024, 10, 9, 12, 24, 43, 864, DateTimeKind.Local).AddTicks(8316), "07ac193d-26e7-4039-9440-fbb53988b878" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 466, DateTimeKind.Local).AddTicks(4683),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 9, 12, 24, 43, 863, DateTimeKind.Local).AddTicks(7757));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 466, DateTimeKind.Local).AddTicks(4328),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 9, 12, 24, 43, 863, DateTimeKind.Local).AddTicks(7453));

            migrationBuilder.AlterColumn<int>(
                name: "ToWalletType",
                table: "TransferType",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FromWalletType",
                table: "TransferType",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ToWalletId",
                table: "Transaction",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "FromWalletId",
                table: "Transaction",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 459, DateTimeKind.Local).AddTicks(1281),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 9, 12, 24, 43, 856, DateTimeKind.Local).AddTicks(8456));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 433, DateTimeKind.Local).AddTicks(1209),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 9, 12, 24, 43, 835, DateTimeKind.Local).AddTicks(6300));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 430, DateTimeKind.Local).AddTicks(5004),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 9, 12, 24, 43, 834, DateTimeKind.Local).AddTicks(752));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "67cce871-3920-406b-b738-ffd14a7d44b7", "AQAAAAEAACcQAAAAEOboHuRFNoBX1aML25Ur2F9zEIqZ/ejPTAS4fRidmYTp2+ppibIWq6W1Dg/LuGlr6A==", "b36510a2-936d-4114-a9dd-aa5be9443799" });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Liberian Dollar");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "United State Of America Dollar");

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Transfer from member a/c to agent a/c");

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Transfer from memeber to agent commission account");

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 6,
                column: "Description",
                value: "TopUp member wallet from agent a/c");

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 7,
                column: "FromWalletType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "ToWalletType" },
                values: new object[] { "Transfer from Agent Commission a/c  to system float n a/c", 1 });

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 9,
                column: "Description",
                value: "Transfer from system float n a/c to sytem wallet");

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 10,
                column: "ToWalletType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Name", "ToWalletType" },
                values: new object[] { "Bill Payment", 1 });

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "FromWalletType", "Name", "ToWalletType" },
                values: new object[] { 6, "On Hand Agent Cashout", 1 });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Status", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 3, 11, 24, 44, 454, DateTimeKind.Local).AddTicks(1370), 0, new DateTime(2024, 9, 3, 11, 24, 44, 454, DateTimeKind.Local).AddTicks(1575) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 3, 11, 24, 44, 467, DateTimeKind.Local).AddTicks(4355), new DateTime(2024, 9, 3, 11, 24, 44, 467, DateTimeKind.Local).AddTicks(4548) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 3, 11, 24, 44, 467, DateTimeKind.Local).AddTicks(4823), new DateTime(2024, 9, 3, 11, 24, 44, 467, DateTimeKind.Local).AddTicks(4826) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 3, 11, 24, 44, 467, DateTimeKind.Local).AddTicks(4833), new DateTime(2024, 9, 3, 11, 24, 44, 467, DateTimeKind.Local).AddTicks(4834) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 3, 11, 24, 44, 467, DateTimeKind.Local).AddTicks(4838), new DateTime(2024, 9, 3, 11, 24, 44, 467, DateTimeKind.Local).AddTicks(4839) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 3, 11, 24, 44, 467, DateTimeKind.Local).AddTicks(4843), new DateTime(2024, 9, 3, 11, 24, 44, 467, DateTimeKind.Local).AddTicks(4845) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 3, 11, 24, 44, 467, DateTimeKind.Local).AddTicks(4850), new DateTime(2024, 9, 3, 11, 24, 44, 467, DateTimeKind.Local).AddTicks(4851) });
        }
    }
}
