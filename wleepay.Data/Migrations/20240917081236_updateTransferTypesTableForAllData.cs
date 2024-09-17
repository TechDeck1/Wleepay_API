using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class updateTransferTypesTableForAllData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                oldDefaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 466, DateTimeKind.Local).AddTicks(4683));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 17, 13, 42, 35, 492, DateTimeKind.Local).AddTicks(8805),
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

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 17, 13, 42, 35, 483, DateTimeKind.Local).AddTicks(8303),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 459, DateTimeKind.Local).AddTicks(1281));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 17, 13, 42, 35, 458, DateTimeKind.Local).AddTicks(5000),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 433, DateTimeKind.Local).AddTicks(1209));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 17, 13, 42, 35, 456, DateTimeKind.Local).AddTicks(7875),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 430, DateTimeKind.Local).AddTicks(5004));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cb981778-09fa-434a-b83b-8c6f868c6a70", "AQAAAAEAACcQAAAAEAgb1RbGr01sDcYR6YGqlyCUOXpYt++JU+0hkQe5Nm3uojjbjnViQpRF45VwmsKCdQ==", "22f13536-ee37-4ed1-9593-13068c534f65" });

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Transfer to agent a/c from member a/c ", "Subscriber Cash Out" });

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
                columns: new[] { "Description", "Name" },
                values: new object[] { "TopUp to member wallet from agent a/c", "Subscriber Top Up" });

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

            migrationBuilder.InsertData(
                table: "TransferType",
                columns: new[] { "Id", "Description", "FromWalletType", "Name", "ToWalletType", "TransferFee" },
                values: new object[,]
                {
                    { 13, "Transfer from agent a/c to general cash account float a/c", 1, "In hand Withdrawl", null, null },
                    { 15, "Transfer from agent a/c to sytem wallet a/c", 6, "Agent Cash Out", 9, null },
                    { 14, "Transfer from agent a/c to general cash account float a/c", null, "In hand Credit", 1, null }
                });

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

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 466, DateTimeKind.Local).AddTicks(4683),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 17, 13, 42, 35, 492, DateTimeKind.Local).AddTicks(9226));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 466, DateTimeKind.Local).AddTicks(4328),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 17, 13, 42, 35, 492, DateTimeKind.Local).AddTicks(8805));

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

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 459, DateTimeKind.Local).AddTicks(1281),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 17, 13, 42, 35, 483, DateTimeKind.Local).AddTicks(8303));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 433, DateTimeKind.Local).AddTicks(1209),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 17, 13, 42, 35, 458, DateTimeKind.Local).AddTicks(5000));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 430, DateTimeKind.Local).AddTicks(5004),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 17, 13, 42, 35, 456, DateTimeKind.Local).AddTicks(7875));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "67cce871-3920-406b-b738-ffd14a7d44b7", "AQAAAAEAACcQAAAAEOboHuRFNoBX1aML25Ur2F9zEIqZ/ejPTAS4fRidmYTp2+ppibIWq6W1Dg/LuGlr6A==", "b36510a2-936d-4114-a9dd-aa5be9443799" });

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Transfer from member a/c to agent a/c", "Cash Out" });

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
                columns: new[] { "Description", "Name" },
                values: new object[] { "TopUp member wallet from agent a/c", "Top Up" });

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

            migrationBuilder.InsertData(
                table: "TransferType",
                columns: new[] { "Id", "Description", "FromWalletType", "Name", "ToWalletType", "TransferFee" },
                values: new object[] { 12, "Transfer from agent a/c to general cash account float a/c", 6, "On Hand Agent Cashout", 1, null });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 3, 11, 24, 44, 454, DateTimeKind.Local).AddTicks(1370), new DateTime(2024, 9, 3, 11, 24, 44, 454, DateTimeKind.Local).AddTicks(1575) });

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
