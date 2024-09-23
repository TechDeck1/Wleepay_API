using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class UpdateTransferTypeTableForNamees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 23, 11, 34, 26, 681, DateTimeKind.Local).AddTicks(9326),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 466, DateTimeKind.Local).AddTicks(4683));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 23, 11, 34, 26, 681, DateTimeKind.Local).AddTicks(8936),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 466, DateTimeKind.Local).AddTicks(4328));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 23, 11, 34, 26, 673, DateTimeKind.Local).AddTicks(9034),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 459, DateTimeKind.Local).AddTicks(1281));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 23, 11, 34, 26, 644, DateTimeKind.Local).AddTicks(3457),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 433, DateTimeKind.Local).AddTicks(1209));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 23, 11, 34, 26, 641, DateTimeKind.Local).AddTicks(1501),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 430, DateTimeKind.Local).AddTicks(5004));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ba060db4-2211-4628-afec-93063e9d4391", "AQAAAAEAACcQAAAAEFjuUmCMRuypCvDvEXjCiaawSUlMi4byCIzLQU3EOatLHe2XxQPz6DqXCr+1qY5yWg==", "6c3ed0f9-cce9-4ee7-8811-d266b56fd007" });

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Mobile Airtime Purchased");

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 11,
                column: "Name",
                value: "LEC Credit Purchased");

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 23, 11, 34, 26, 667, DateTimeKind.Local).AddTicks(8967), new DateTime(2024, 9, 23, 11, 34, 26, 667, DateTimeKind.Local).AddTicks(9314) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 23, 11, 34, 26, 683, DateTimeKind.Local).AddTicks(1363), new DateTime(2024, 9, 23, 11, 34, 26, 683, DateTimeKind.Local).AddTicks(1645) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 23, 11, 34, 26, 683, DateTimeKind.Local).AddTicks(2041), new DateTime(2024, 9, 23, 11, 34, 26, 683, DateTimeKind.Local).AddTicks(2046) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 23, 11, 34, 26, 683, DateTimeKind.Local).AddTicks(2053), new DateTime(2024, 9, 23, 11, 34, 26, 683, DateTimeKind.Local).AddTicks(2054) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 23, 11, 34, 26, 683, DateTimeKind.Local).AddTicks(2059), new DateTime(2024, 9, 23, 11, 34, 26, 683, DateTimeKind.Local).AddTicks(2060) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 23, 11, 34, 26, 683, DateTimeKind.Local).AddTicks(2063), new DateTime(2024, 9, 23, 11, 34, 26, 683, DateTimeKind.Local).AddTicks(2065) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 23, 11, 34, 26, 683, DateTimeKind.Local).AddTicks(2071), new DateTime(2024, 9, 23, 11, 34, 26, 683, DateTimeKind.Local).AddTicks(2072) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 466, DateTimeKind.Local).AddTicks(4683),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 23, 11, 34, 26, 681, DateTimeKind.Local).AddTicks(9326));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 466, DateTimeKind.Local).AddTicks(4328),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 23, 11, 34, 26, 681, DateTimeKind.Local).AddTicks(8936));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 459, DateTimeKind.Local).AddTicks(1281),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 23, 11, 34, 26, 673, DateTimeKind.Local).AddTicks(9034));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 433, DateTimeKind.Local).AddTicks(1209),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 23, 11, 34, 26, 644, DateTimeKind.Local).AddTicks(3457));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 430, DateTimeKind.Local).AddTicks(5004),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 23, 11, 34, 26, 641, DateTimeKind.Local).AddTicks(1501));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "67cce871-3920-406b-b738-ffd14a7d44b7", "AQAAAAEAACcQAAAAEOboHuRFNoBX1aML25Ur2F9zEIqZ/ejPTAS4fRidmYTp2+ppibIWq6W1Dg/LuGlr6A==", "b36510a2-936d-4114-a9dd-aa5be9443799" });

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Mobile Recharge Transfer");

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 11,
                column: "Name",
                value: "Bill Payment");

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
