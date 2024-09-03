using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class UpdateSochitelTransactionTableForDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 466, DateTimeKind.Local).AddTicks(4683),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 26, 13, 7, 54, 264, DateTimeKind.Local).AddTicks(1243));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 466, DateTimeKind.Local).AddTicks(4328),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 26, 13, 7, 54, 264, DateTimeKind.Local).AddTicks(892));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 459, DateTimeKind.Local).AddTicks(1281),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 26, 13, 7, 54, 256, DateTimeKind.Local).AddTicks(3349));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "SochitelTransactions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 433, DateTimeKind.Local).AddTicks(1209),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 26, 13, 7, 54, 213, DateTimeKind.Local).AddTicks(3115));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 430, DateTimeKind.Local).AddTicks(5004),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 26, 13, 7, 54, 210, DateTimeKind.Local).AddTicks(9849));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "67cce871-3920-406b-b738-ffd14a7d44b7", "AQAAAAEAACcQAAAAEOboHuRFNoBX1aML25Ur2F9zEIqZ/ejPTAS4fRidmYTp2+ppibIWq6W1Dg/LuGlr6A==", "b36510a2-936d-4114-a9dd-aa5be9443799" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "SochitelTransactions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 26, 13, 7, 54, 264, DateTimeKind.Local).AddTicks(1243),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 466, DateTimeKind.Local).AddTicks(4683));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 26, 13, 7, 54, 264, DateTimeKind.Local).AddTicks(892),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 466, DateTimeKind.Local).AddTicks(4328));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 26, 13, 7, 54, 256, DateTimeKind.Local).AddTicks(3349),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 459, DateTimeKind.Local).AddTicks(1281));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 26, 13, 7, 54, 213, DateTimeKind.Local).AddTicks(3115),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 433, DateTimeKind.Local).AddTicks(1209));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 26, 13, 7, 54, 210, DateTimeKind.Local).AddTicks(9849),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 3, 11, 24, 44, 430, DateTimeKind.Local).AddTicks(5004));

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
    }
}
