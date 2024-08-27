using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class updateTheSochitelTransactionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 26, 13, 7, 54, 264, DateTimeKind.Local).AddTicks(1243),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 20, 11, 48, 56, 896, DateTimeKind.Local).AddTicks(3713));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 26, 13, 7, 54, 264, DateTimeKind.Local).AddTicks(892),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 20, 11, 48, 56, 896, DateTimeKind.Local).AddTicks(3247));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 26, 13, 7, 54, 256, DateTimeKind.Local).AddTicks(3349),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 20, 11, 48, 56, 887, DateTimeKind.Local).AddTicks(2176));

            migrationBuilder.AlterColumn<string>(
                name: "TransactionAmount",
                table: "SochitelTransactions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "CommissionRate",
                table: "SochitelTransactions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 26, 13, 7, 54, 213, DateTimeKind.Local).AddTicks(3115),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 20, 11, 48, 56, 854, DateTimeKind.Local).AddTicks(9083));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 26, 13, 7, 54, 210, DateTimeKind.Local).AddTicks(9849),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 20, 11, 48, 56, 852, DateTimeKind.Local).AddTicks(818));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "09993213-cc45-4a85-99fe-e522417b981c", "AQAAAAEAACcQAAAAECRa0DRtQ/UIh/RiNukD5v0IvdqkibhBJyyFHWagwL0Bv/Ibb6itq1SrOI4eAKEbsA==", "cac8d83b-d135-45d1-9691-a0555a7e7c62" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 26, 13, 7, 54, 249, DateTimeKind.Local).AddTicks(9007), new DateTime(2024, 8, 26, 13, 7, 54, 249, DateTimeKind.Local).AddTicks(9380) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 26, 13, 7, 54, 265, DateTimeKind.Local).AddTicks(2324), new DateTime(2024, 8, 26, 13, 7, 54, 265, DateTimeKind.Local).AddTicks(2603) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 26, 13, 7, 54, 265, DateTimeKind.Local).AddTicks(2972), new DateTime(2024, 8, 26, 13, 7, 54, 265, DateTimeKind.Local).AddTicks(2976) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 26, 13, 7, 54, 265, DateTimeKind.Local).AddTicks(2982), new DateTime(2024, 8, 26, 13, 7, 54, 265, DateTimeKind.Local).AddTicks(2983) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 26, 13, 7, 54, 265, DateTimeKind.Local).AddTicks(2988), new DateTime(2024, 8, 26, 13, 7, 54, 265, DateTimeKind.Local).AddTicks(2990) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 26, 13, 7, 54, 265, DateTimeKind.Local).AddTicks(2994), new DateTime(2024, 8, 26, 13, 7, 54, 265, DateTimeKind.Local).AddTicks(2995) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 26, 13, 7, 54, 265, DateTimeKind.Local).AddTicks(3001), new DateTime(2024, 8, 26, 13, 7, 54, 265, DateTimeKind.Local).AddTicks(3002) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 20, 11, 48, 56, 896, DateTimeKind.Local).AddTicks(3713),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 26, 13, 7, 54, 264, DateTimeKind.Local).AddTicks(1243));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 20, 11, 48, 56, 896, DateTimeKind.Local).AddTicks(3247),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 26, 13, 7, 54, 264, DateTimeKind.Local).AddTicks(892));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 20, 11, 48, 56, 887, DateTimeKind.Local).AddTicks(2176),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 26, 13, 7, 54, 256, DateTimeKind.Local).AddTicks(3349));

            migrationBuilder.AlterColumn<double>(
                name: "TransactionAmount",
                table: "SochitelTransactions",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "CommissionRate",
                table: "SochitelTransactions",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 20, 11, 48, 56, 854, DateTimeKind.Local).AddTicks(9083),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 26, 13, 7, 54, 213, DateTimeKind.Local).AddTicks(3115));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 20, 11, 48, 56, 852, DateTimeKind.Local).AddTicks(818),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 26, 13, 7, 54, 210, DateTimeKind.Local).AddTicks(9849));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1ed9cb64-dd01-461c-adc2-462319ec31d0", "AQAAAAEAACcQAAAAEH1MdTj2uHN76o12VRnhP+2qri0G9l9V3zhdBlu8kiZoOXNSxeb2kVtqHvw/yRvQlQ==", "6b374b7d-20ad-4f0a-981b-f52d11091efd" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 20, 11, 48, 56, 880, DateTimeKind.Local).AddTicks(4890), new DateTime(2024, 8, 20, 11, 48, 56, 880, DateTimeKind.Local).AddTicks(5367) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 20, 11, 48, 56, 897, DateTimeKind.Local).AddTicks(7746), new DateTime(2024, 8, 20, 11, 48, 56, 897, DateTimeKind.Local).AddTicks(8187) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 20, 11, 48, 56, 897, DateTimeKind.Local).AddTicks(8651), new DateTime(2024, 8, 20, 11, 48, 56, 897, DateTimeKind.Local).AddTicks(8656) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 20, 11, 48, 56, 897, DateTimeKind.Local).AddTicks(8663), new DateTime(2024, 8, 20, 11, 48, 56, 897, DateTimeKind.Local).AddTicks(8664) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 20, 11, 48, 56, 897, DateTimeKind.Local).AddTicks(8668), new DateTime(2024, 8, 20, 11, 48, 56, 897, DateTimeKind.Local).AddTicks(8669) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 20, 11, 48, 56, 897, DateTimeKind.Local).AddTicks(8673), new DateTime(2024, 8, 20, 11, 48, 56, 897, DateTimeKind.Local).AddTicks(8674) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 20, 11, 48, 56, 897, DateTimeKind.Local).AddTicks(8681), new DateTime(2024, 8, 20, 11, 48, 56, 897, DateTimeKind.Local).AddTicks(8682) });
        }
    }
}
