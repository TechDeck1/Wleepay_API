using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class UpdatePaymentTransactionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 10, 8, 10, 48, 24, 25, DateTimeKind.Local).AddTicks(7239),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 9, 29, 10, 27, 49, 341, DateTimeKind.Local).AddTicks(3396));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 10, 8, 10, 48, 24, 25, DateTimeKind.Local).AddTicks(6375),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 9, 29, 10, 27, 49, 341, DateTimeKind.Local).AddTicks(2907));

            migrationBuilder.AlterColumn<string>(
                name: "ReceiverPhoneNumber",
                table: "UserTransactions",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 10, 8, 10, 48, 24, 13, DateTimeKind.Local).AddTicks(8621),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 9, 29, 10, 27, 49, 329, DateTimeKind.Local).AddTicks(4966));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 10, 8, 10, 48, 23, 984, DateTimeKind.Local).AddTicks(2886),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 9, 29, 10, 27, 49, 299, DateTimeKind.Local).AddTicks(1272));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 10, 8, 10, 48, 23, 982, DateTimeKind.Local).AddTicks(8483),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 9, 29, 10, 27, 49, 297, DateTimeKind.Local).AddTicks(7273));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "69e0f58c-bb15-4f2d-928d-9ccb97539e4b", "AQAAAAEAACcQAAAAEGMR2xqKDgViKzrlNkZYptyGhK65ak6T4ewqE85cg9ZUVhSQJhgT0vYqcBFVFQ9w8A==", "e47dad46-a6a3-4344-91f4-e9a4f7eca067" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 10, 8, 10, 48, 24, 6, DateTimeKind.Local).AddTicks(981), new DateTime(2026, 10, 8, 10, 48, 24, 6, DateTimeKind.Local).AddTicks(1356) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 10, 8, 10, 48, 24, 28, DateTimeKind.Local).AddTicks(675), new DateTime(2026, 10, 8, 10, 48, 24, 28, DateTimeKind.Local).AddTicks(1231) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 10, 8, 10, 48, 24, 28, DateTimeKind.Local).AddTicks(2186), new DateTime(2026, 10, 8, 10, 48, 24, 28, DateTimeKind.Local).AddTicks(2194) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 10, 8, 10, 48, 24, 28, DateTimeKind.Local).AddTicks(2210), new DateTime(2026, 10, 8, 10, 48, 24, 28, DateTimeKind.Local).AddTicks(2211) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 10, 8, 10, 48, 24, 28, DateTimeKind.Local).AddTicks(2218), new DateTime(2026, 10, 8, 10, 48, 24, 28, DateTimeKind.Local).AddTicks(2220) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 10, 8, 10, 48, 24, 28, DateTimeKind.Local).AddTicks(2237), new DateTime(2026, 10, 8, 10, 48, 24, 28, DateTimeKind.Local).AddTicks(2241) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 10, 8, 10, 48, 24, 28, DateTimeKind.Local).AddTicks(2269), new DateTime(2026, 10, 8, 10, 48, 24, 28, DateTimeKind.Local).AddTicks(2273) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 10, 8, 10, 48, 24, 28, DateTimeKind.Local).AddTicks(2291), new DateTime(2026, 10, 8, 10, 48, 24, 28, DateTimeKind.Local).AddTicks(2295) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 10, 8, 10, 48, 24, 28, DateTimeKind.Local).AddTicks(2312), new DateTime(2026, 10, 8, 10, 48, 24, 28, DateTimeKind.Local).AddTicks(2315) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 9, 29, 10, 27, 49, 341, DateTimeKind.Local).AddTicks(3396),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 10, 8, 10, 48, 24, 25, DateTimeKind.Local).AddTicks(7239));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 9, 29, 10, 27, 49, 341, DateTimeKind.Local).AddTicks(2907),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 10, 8, 10, 48, 24, 25, DateTimeKind.Local).AddTicks(6375));

            migrationBuilder.AlterColumn<string>(
                name: "ReceiverPhoneNumber",
                table: "UserTransactions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 9, 29, 10, 27, 49, 329, DateTimeKind.Local).AddTicks(4966),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 10, 8, 10, 48, 24, 13, DateTimeKind.Local).AddTicks(8621));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 9, 29, 10, 27, 49, 299, DateTimeKind.Local).AddTicks(1272),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 10, 8, 10, 48, 23, 984, DateTimeKind.Local).AddTicks(2886));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 9, 29, 10, 27, 49, 297, DateTimeKind.Local).AddTicks(7273),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 10, 8, 10, 48, 23, 982, DateTimeKind.Local).AddTicks(8483));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c8a1ab62-a54d-4265-9cfb-76d7898487ff", "AQAAAAEAACcQAAAAEALGE5dJoJesbEGmvPbFrDLqIHkROdTW153+3lzocUMbV6zDlz6PK96Zl7SCNjJmGQ==", "7f4f422f-d93e-4775-a3c2-e60ae6a04372" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 9, 29, 10, 27, 49, 321, DateTimeKind.Local).AddTicks(9708), new DateTime(2026, 9, 29, 10, 27, 49, 322, DateTimeKind.Local).AddTicks(100) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 9, 29, 10, 27, 49, 342, DateTimeKind.Local).AddTicks(7528), new DateTime(2026, 9, 29, 10, 27, 49, 342, DateTimeKind.Local).AddTicks(7885) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 9, 29, 10, 27, 49, 342, DateTimeKind.Local).AddTicks(8383), new DateTime(2026, 9, 29, 10, 27, 49, 342, DateTimeKind.Local).AddTicks(8388) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 9, 29, 10, 27, 49, 342, DateTimeKind.Local).AddTicks(8395), new DateTime(2026, 9, 29, 10, 27, 49, 342, DateTimeKind.Local).AddTicks(8397) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 9, 29, 10, 27, 49, 342, DateTimeKind.Local).AddTicks(8401), new DateTime(2026, 9, 29, 10, 27, 49, 342, DateTimeKind.Local).AddTicks(8403) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 9, 29, 10, 27, 49, 342, DateTimeKind.Local).AddTicks(8464), new DateTime(2026, 9, 29, 10, 27, 49, 342, DateTimeKind.Local).AddTicks(8465) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 9, 29, 10, 27, 49, 342, DateTimeKind.Local).AddTicks(8474), new DateTime(2026, 9, 29, 10, 27, 49, 342, DateTimeKind.Local).AddTicks(8475) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 9, 29, 10, 27, 49, 342, DateTimeKind.Local).AddTicks(8480), new DateTime(2026, 9, 29, 10, 27, 49, 342, DateTimeKind.Local).AddTicks(8481) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 9, 29, 10, 27, 49, 342, DateTimeKind.Local).AddTicks(8486), new DateTime(2026, 9, 29, 10, 27, 49, 342, DateTimeKind.Local).AddTicks(8487) });
        }
    }
}
