using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class updateTransaferTypeTableforNewTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 3, 13, 44, 13, 743, DateTimeKind.Local).AddTicks(6944),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 30, 15, 45, 12, 462, DateTimeKind.Local).AddTicks(7947));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 3, 13, 44, 13, 743, DateTimeKind.Local).AddTicks(6245),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 30, 15, 45, 12, 462, DateTimeKind.Local).AddTicks(7410));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 3, 13, 44, 13, 730, DateTimeKind.Local).AddTicks(5969),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 30, 15, 45, 12, 450, DateTimeKind.Local).AddTicks(6770));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 3, 13, 44, 13, 688, DateTimeKind.Local).AddTicks(1585),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 30, 15, 45, 12, 416, DateTimeKind.Local).AddTicks(370));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 3, 13, 44, 13, 684, DateTimeKind.Local).AddTicks(6434),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 30, 15, 45, 12, 413, DateTimeKind.Local).AddTicks(5357));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0b558cd4-bb98-44b3-b700-006960f0490f", "AQAAAAEAACcQAAAAEGyZanf1BZ392iE3dU2GfiOZREXgqwTi5JUVyNJADKxitJFHfVJZgnAt1eshP92VMg==", "fa33f3c3-7634-462d-a033-cc0c00325bf1" });

            migrationBuilder.InsertData(
                table: "TransferType",
                columns: new[] { "Id", "Description", "FromWalletType", "Name", "ToWalletType", "TransferFee" },
                values: new object[,]
                {
                    { 15, "Transfer from system a/c to member wallet a/c", 7, "Member Top Up By WleePay", 9, null },
                    { 16, "Transfer from member a/c to sytem wallet a/c", 7, "Member Cash Out By Wleepay", 9, null }
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 30, 15, 45, 12, 462, DateTimeKind.Local).AddTicks(7947),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 3, 13, 44, 13, 743, DateTimeKind.Local).AddTicks(6944));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 30, 15, 45, 12, 462, DateTimeKind.Local).AddTicks(7410),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 3, 13, 44, 13, 743, DateTimeKind.Local).AddTicks(6245));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 30, 15, 45, 12, 450, DateTimeKind.Local).AddTicks(6770),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 3, 13, 44, 13, 730, DateTimeKind.Local).AddTicks(5969));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 30, 15, 45, 12, 416, DateTimeKind.Local).AddTicks(370),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 3, 13, 44, 13, 688, DateTimeKind.Local).AddTicks(1585));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 30, 15, 45, 12, 413, DateTimeKind.Local).AddTicks(5357),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 3, 13, 44, 13, 684, DateTimeKind.Local).AddTicks(6434));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dd799b45-1c15-4323-9fa0-ff346a8f8b8b", "AQAAAAEAACcQAAAAEFBUUBKLQCLu26HxuKSkWYnRQhfATrmLZrwbCvtlJEW3rLDRxXetjqy6wVXwspG7KQ==", "d7bc6374-d183-406c-84de-c8f01ce3d63c" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 30, 15, 45, 12, 442, DateTimeKind.Local).AddTicks(8616), new DateTime(2024, 9, 30, 15, 45, 12, 442, DateTimeKind.Local).AddTicks(9266) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 30, 15, 45, 12, 464, DateTimeKind.Local).AddTicks(3797), new DateTime(2024, 9, 30, 15, 45, 12, 464, DateTimeKind.Local).AddTicks(4224) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 30, 15, 45, 12, 464, DateTimeKind.Local).AddTicks(4750), new DateTime(2024, 9, 30, 15, 45, 12, 464, DateTimeKind.Local).AddTicks(4756) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 30, 15, 45, 12, 464, DateTimeKind.Local).AddTicks(4765), new DateTime(2024, 9, 30, 15, 45, 12, 464, DateTimeKind.Local).AddTicks(4766) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 30, 15, 45, 12, 464, DateTimeKind.Local).AddTicks(4771), new DateTime(2024, 9, 30, 15, 45, 12, 464, DateTimeKind.Local).AddTicks(4773) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 30, 15, 45, 12, 464, DateTimeKind.Local).AddTicks(4778), new DateTime(2024, 9, 30, 15, 45, 12, 464, DateTimeKind.Local).AddTicks(4779) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 30, 15, 45, 12, 464, DateTimeKind.Local).AddTicks(4786), new DateTime(2024, 9, 30, 15, 45, 12, 464, DateTimeKind.Local).AddTicks(4787) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 30, 15, 45, 12, 464, DateTimeKind.Local).AddTicks(4792), new DateTime(2024, 9, 30, 15, 45, 12, 464, DateTimeKind.Local).AddTicks(4793) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 30, 15, 45, 12, 464, DateTimeKind.Local).AddTicks(4798), new DateTime(2024, 9, 30, 15, 45, 12, 464, DateTimeKind.Local).AddTicks(4799) });
        }
    }
}
