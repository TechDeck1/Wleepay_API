using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class UpdateDataInWalletTypeAndTransferTypeTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 1, 11, 14, 7, 471, DateTimeKind.Local).AddTicks(1135),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 31, 12, 52, 37, 529, DateTimeKind.Local).AddTicks(9119));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 1, 11, 14, 7, 471, DateTimeKind.Local).AddTicks(737),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 31, 12, 52, 37, 529, DateTimeKind.Local).AddTicks(8720));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 1, 11, 14, 7, 462, DateTimeKind.Local).AddTicks(1185),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 31, 12, 52, 37, 524, DateTimeKind.Local).AddTicks(636));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 1, 11, 14, 7, 429, DateTimeKind.Local).AddTicks(778),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 31, 12, 52, 37, 504, DateTimeKind.Local).AddTicks(895));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 1, 11, 14, 7, 426, DateTimeKind.Local).AddTicks(2566),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 31, 12, 52, 37, 502, DateTimeKind.Local).AddTicks(6110));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b4ed6399-7991-495e-8021-16ccbd75d086", "AQAAAAEAACcQAAAAEPuFhxXjqBNcepCrD8uMr6XLg27hFe4v98GQox+8F+U7RtJWQuywdpNCvIgWPLY5+g==", "c0afcfe3-065b-4938-849d-eafbccff9d91" });

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 8,
                column: "Description",
                value: "Transfer from Agent Commission a/c  to system float n a/c");

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

            migrationBuilder.InsertData(
                table: "WalletType",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 9, "System Account", "System Wallet" });

            migrationBuilder.InsertData(
                table: "TransferType",
                columns: new[] { "Id", "Description", "FromWalletType", "Name", "ToWalletType", "TransferFee" },
                values: new object[] { 9, "Transfer from system float n a/c to sytem admin a/c", 1, "Cash On Hand", 9, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "WalletType",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 31, 12, 52, 37, 529, DateTimeKind.Local).AddTicks(9119),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 1, 11, 14, 7, 471, DateTimeKind.Local).AddTicks(1135));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 31, 12, 52, 37, 529, DateTimeKind.Local).AddTicks(8720),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 1, 11, 14, 7, 471, DateTimeKind.Local).AddTicks(737));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 31, 12, 52, 37, 524, DateTimeKind.Local).AddTicks(636),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 1, 11, 14, 7, 462, DateTimeKind.Local).AddTicks(1185));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 31, 12, 52, 37, 504, DateTimeKind.Local).AddTicks(895),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 1, 11, 14, 7, 429, DateTimeKind.Local).AddTicks(778));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 31, 12, 52, 37, 502, DateTimeKind.Local).AddTicks(6110),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 1, 11, 14, 7, 426, DateTimeKind.Local).AddTicks(2566));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2e132c84-81b9-4295-97c1-c336c1f6a8f7", "AQAAAAEAACcQAAAAEDIeUPvYf2lNNdRpUwYx/tJ1U5wmi9pJP6Nx74Yl4RDG7khJu1s39Ur2EPTONYHfSg==", "e7775734-8dad-4b02-b354-f76b4c2c965f" });

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 8,
                column: "Description",
                value: "Transfer from Agent Commission a/c to system float n a/c");

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 31, 12, 52, 37, 519, DateTimeKind.Local).AddTicks(3475), new DateTime(2024, 1, 31, 12, 52, 37, 519, DateTimeKind.Local).AddTicks(3687) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 31, 12, 52, 37, 530, DateTimeKind.Local).AddTicks(8423), new DateTime(2024, 1, 31, 12, 52, 37, 530, DateTimeKind.Local).AddTicks(8599) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 31, 12, 52, 37, 530, DateTimeKind.Local).AddTicks(8853), new DateTime(2024, 1, 31, 12, 52, 37, 530, DateTimeKind.Local).AddTicks(8856) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 31, 12, 52, 37, 530, DateTimeKind.Local).AddTicks(8905), new DateTime(2024, 1, 31, 12, 52, 37, 530, DateTimeKind.Local).AddTicks(8907) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 31, 12, 52, 37, 530, DateTimeKind.Local).AddTicks(8911), new DateTime(2024, 1, 31, 12, 52, 37, 530, DateTimeKind.Local).AddTicks(8912) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 31, 12, 52, 37, 530, DateTimeKind.Local).AddTicks(8915), new DateTime(2024, 1, 31, 12, 52, 37, 530, DateTimeKind.Local).AddTicks(8916) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 31, 12, 52, 37, 530, DateTimeKind.Local).AddTicks(8922), new DateTime(2024, 1, 31, 12, 52, 37, 530, DateTimeKind.Local).AddTicks(8923) });
        }
    }
}
