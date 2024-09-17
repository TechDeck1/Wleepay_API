using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class DeleteDataFromTransferType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 17, 15, 34, 23, 33, DateTimeKind.Local).AddTicks(5459),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 17, 15, 31, 18, 415, DateTimeKind.Local).AddTicks(4303));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 17, 15, 34, 23, 33, DateTimeKind.Local).AddTicks(4908),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 17, 15, 31, 18, 415, DateTimeKind.Local).AddTicks(3906));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 17, 15, 34, 23, 25, DateTimeKind.Local).AddTicks(460),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 17, 15, 31, 18, 406, DateTimeKind.Local).AddTicks(6783));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 17, 15, 34, 22, 999, DateTimeKind.Local).AddTicks(3717),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 17, 15, 31, 18, 376, DateTimeKind.Local).AddTicks(3448));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 17, 15, 34, 22, 997, DateTimeKind.Local).AddTicks(4997),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 17, 15, 31, 18, 373, DateTimeKind.Local).AddTicks(5343));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c167df71-f2ce-4852-99e5-8ca5a7b9265a", "AQAAAAEAACcQAAAAENuDNvxESi+Ot443t1A8iKW2oQANZvSnEupkx4xCVPwSx4nBXM34DmB+dyMSJLuPMA==", "65888e0a-fddc-4261-b515-493aa247fb53" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 17, 15, 34, 23, 18, DateTimeKind.Local).AddTicks(8499), new DateTime(2024, 9, 17, 15, 34, 23, 18, DateTimeKind.Local).AddTicks(9052) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 17, 15, 34, 23, 34, DateTimeKind.Local).AddTicks(8861), new DateTime(2024, 9, 17, 15, 34, 23, 34, DateTimeKind.Local).AddTicks(9215) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 17, 15, 34, 23, 34, DateTimeKind.Local).AddTicks(9672), new DateTime(2024, 9, 17, 15, 34, 23, 34, DateTimeKind.Local).AddTicks(9677) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 17, 15, 34, 23, 34, DateTimeKind.Local).AddTicks(9683), new DateTime(2024, 9, 17, 15, 34, 23, 34, DateTimeKind.Local).AddTicks(9685) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 17, 15, 34, 23, 34, DateTimeKind.Local).AddTicks(9689), new DateTime(2024, 9, 17, 15, 34, 23, 34, DateTimeKind.Local).AddTicks(9690) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 17, 15, 34, 23, 34, DateTimeKind.Local).AddTicks(9694), new DateTime(2024, 9, 17, 15, 34, 23, 34, DateTimeKind.Local).AddTicks(9695) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 17, 15, 34, 23, 34, DateTimeKind.Local).AddTicks(9702), new DateTime(2024, 9, 17, 15, 34, 23, 34, DateTimeKind.Local).AddTicks(9703) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 17, 15, 31, 18, 415, DateTimeKind.Local).AddTicks(4303),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 17, 15, 34, 23, 33, DateTimeKind.Local).AddTicks(5459));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 17, 15, 31, 18, 415, DateTimeKind.Local).AddTicks(3906),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 17, 15, 34, 23, 33, DateTimeKind.Local).AddTicks(4908));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 17, 15, 31, 18, 406, DateTimeKind.Local).AddTicks(6783),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 17, 15, 34, 23, 25, DateTimeKind.Local).AddTicks(460));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 17, 15, 31, 18, 376, DateTimeKind.Local).AddTicks(3448),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 17, 15, 34, 22, 999, DateTimeKind.Local).AddTicks(3717));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 17, 15, 31, 18, 373, DateTimeKind.Local).AddTicks(5343),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 17, 15, 34, 22, 997, DateTimeKind.Local).AddTicks(4997));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7470beb0-5387-431b-9b37-6dc760b1318a", "AQAAAAEAACcQAAAAEIeVU+KxJPPrAXIFgxOpQd1p4J32DTozwmWjBxc71sVbvBluMPvu2xhznZgvEXDzsg==", "7ed1f4e7-fe1c-4e0d-8bd5-345aef97c82d" });

            migrationBuilder.InsertData(
                table: "TransferType",
                columns: new[] { "Id", "Description", "FromWalletType", "Name", "ToWalletType", "TransferFee" },
                values: new object[,]
                {
                    { 1, "Transfer to agent a/c from member a/c ", 7, "Subscriber Cash Out", 6, 1 },
                    { 14, "Transfer from agent a/c to sytem wallet a/c", 6, "Agent Cash Out", 9, null },
                    { 13, "Transfer from agent a/c to general cash account float a/c", null, "In hand Credit", 1, null },
                    { 12, "Transfer from agent a/c to general cash account float a/c", 1, "In hand Withdrawl", null, null },
                    { 10, "Transfer from member a/c to system float a/c", 7, "Mobile Recharge Transfer", 9, null },
                    { 9, "Transfer from cash on hand a/c to sytem wallet", 1, "Top Up System Wallet", 9, null },
                    { 11, "Transfer from member a/c to system float a/c", 7, "Bill Payment Transfer", 9, null },
                    { 7, "Fill Agent a/c from system float n a/c", 9, "Fill Agent", 6, null },
                    { 6, "TopUp to member wallet from agent a/c", 6, "Subscriber Top Up", 7, null },
                    { 5, "Transfer from wleepay member acount to other member account", 7, "Wlee Transfer", 7, null },
                    { 4, "Transfer from member to organization account p", 7, "Cash Out Standard Fee Split", 2, null },
                    { 3, "Transfer from member to wlee tax account", 7, "Wlee Tax Transfer", 4, null },
                    { 2, "Transfer to agent commision a/c from memeber account", 7, "Generated Agent Commission", 5, null },
                    { 8, "Transfer to system float n a/c from Agent Commission a/c", 5, "Cashout Agent Commission", 9, null }
                });

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
    }
}
