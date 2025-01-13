using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class AddMessageTypeToUserNotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<DateTime>(
            //    name: "UpdatedDate",
            //    table: "Wallet",
            //    type: "datetime2",
            //    nullable: false,
            //    defaultValue: new DateTime(2025, 1, 6, 11, 54, 0, 917, DateTimeKind.Local).AddTicks(5140),
            //    oldClrType: typeof(DateTime),
            //    oldType: "datetime2",
            //    oldDefaultValue: new DateTime(2024, 12, 24, 10, 48, 13, 413, DateTimeKind.Local).AddTicks(6462));

            //migrationBuilder.AlterColumn<DateTime>(
            //    name: "CreatedDate",
            //    table: "Wallet",
            //    type: "datetime2",
            //    nullable: false,
            //    defaultValue: new DateTime(2025, 1, 6, 11, 54, 0, 917, DateTimeKind.Local).AddTicks(4511),
            //    oldClrType: typeof(DateTime),
            //    oldType: "datetime2",
            //    oldDefaultValue: new DateTime(2024, 12, 24, 10, 48, 13, 413, DateTimeKind.Local).AddTicks(5470));

            migrationBuilder.AddColumn<int>(
                name: "MessageType",
                table: "UserNotes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.AlterColumn<DateTime>(
            //    name: "CreatedDate",
            //    table: "Transaction",
            //    type: "datetime2",
            //    nullable: false,
            //    defaultValue: new DateTime(2025, 1, 6, 11, 54, 0, 902, DateTimeKind.Local).AddTicks(7949),
            //    oldClrType: typeof(DateTime),
            //    oldType: "datetime2",
            //    oldDefaultValue: new DateTime(2024, 12, 24, 10, 48, 13, 394, DateTimeKind.Local).AddTicks(882));

            //migrationBuilder.AlterColumn<DateTime>(
            //    name: "UpdatedDate",
            //    table: "KYCDocuments",
            //    type: "datetime2",
            //    nullable: false,
            //    defaultValue: new DateTime(2025, 1, 6, 11, 54, 0, 859, DateTimeKind.Local).AddTicks(8534),
            //    oldClrType: typeof(DateTime),
            //    oldType: "datetime2",
            //    oldDefaultValue: new DateTime(2024, 12, 24, 10, 48, 13, 345, DateTimeKind.Local).AddTicks(3127));

            //migrationBuilder.AlterColumn<DateTime>(
            //    name: "CreatedDate",
            //    table: "KYCDocuments",
            //    type: "datetime2",
            //    nullable: false,
            //    defaultValue: new DateTime(2025, 1, 6, 11, 54, 0, 856, DateTimeKind.Local).AddTicks(8779),
            //    oldClrType: typeof(DateTime),
            //    oldType: "datetime2",
            //    oldDefaultValue: new DateTime(2024, 12, 24, 10, 48, 13, 342, DateTimeKind.Local).AddTicks(4223));

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "7fa51391-5e7c-418a-a434-5bb02769b464", "AQAAAAEAACcQAAAAEGL+aLiTOdailwLRD8dNkGM4Wpd/VBsySUiXn0UGZ8xFn5JTlXPpyheuSz+6Vllqtg==", "01edbd66-75f0-4824-8f70-a906aa577d16" });

            //migrationBuilder.UpdateData(
            //    table: "UserDetail",
            //    keyColumn: "Id",
            //    keyValue: 1,
            //    columns: new[] { "CreatedDate", "UpdatedDate" },
            //    values: new object[] { new DateTime(2025, 1, 6, 11, 54, 0, 890, DateTimeKind.Local).AddTicks(1642), new DateTime(2025, 1, 6, 11, 54, 0, 890, DateTimeKind.Local).AddTicks(2366) });

            //migrationBuilder.UpdateData(
            //    table: "Wallet",
            //    keyColumn: "Id",
            //    keyValue: 1L,
            //    columns: new[] { "CreatedDate", "UpdatedDate" },
            //    values: new object[] { new DateTime(2025, 1, 6, 11, 54, 0, 919, DateTimeKind.Local).AddTicks(6745), new DateTime(2025, 1, 6, 11, 54, 0, 919, DateTimeKind.Local).AddTicks(7339) });

            //migrationBuilder.UpdateData(
            //    table: "Wallet",
            //    keyColumn: "Id",
            //    keyValue: 2L,
            //    columns: new[] { "CreatedDate", "UpdatedDate" },
            //    values: new object[] { new DateTime(2025, 1, 6, 11, 54, 0, 919, DateTimeKind.Local).AddTicks(8187), new DateTime(2025, 1, 6, 11, 54, 0, 919, DateTimeKind.Local).AddTicks(8195) });

            //migrationBuilder.UpdateData(
            //    table: "Wallet",
            //    keyColumn: "Id",
            //    keyValue: 3L,
            //    columns: new[] { "CreatedDate", "UpdatedDate" },
            //    values: new object[] { new DateTime(2025, 1, 6, 11, 54, 0, 919, DateTimeKind.Local).AddTicks(8206), new DateTime(2025, 1, 6, 11, 54, 0, 919, DateTimeKind.Local).AddTicks(8208) });

            //migrationBuilder.UpdateData(
            //    table: "Wallet",
            //    keyColumn: "Id",
            //    keyValue: 4L,
            //    columns: new[] { "CreatedDate", "UpdatedDate" },
            //    values: new object[] { new DateTime(2025, 1, 6, 11, 54, 0, 919, DateTimeKind.Local).AddTicks(8216), new DateTime(2025, 1, 6, 11, 54, 0, 919, DateTimeKind.Local).AddTicks(8217) });

            //migrationBuilder.UpdateData(
            //    table: "Wallet",
            //    keyColumn: "Id",
            //    keyValue: 5L,
            //    columns: new[] { "CreatedDate", "UpdatedDate" },
            //    values: new object[] { new DateTime(2025, 1, 6, 11, 54, 0, 919, DateTimeKind.Local).AddTicks(8223), new DateTime(2025, 1, 6, 11, 54, 0, 919, DateTimeKind.Local).AddTicks(8224) });

            //migrationBuilder.UpdateData(
            //    table: "Wallet",
            //    keyColumn: "Id",
            //    keyValue: 6L,
            //    columns: new[] { "CreatedDate", "UpdatedDate" },
            //    values: new object[] { new DateTime(2025, 1, 6, 11, 54, 0, 919, DateTimeKind.Local).AddTicks(8239), new DateTime(2025, 1, 6, 11, 54, 0, 919, DateTimeKind.Local).AddTicks(8240) });

            //migrationBuilder.UpdateData(
            //    table: "Wallet",
            //    keyColumn: "Id",
            //    keyValue: 7L,
            //    columns: new[] { "CreatedDate", "UpdatedDate" },
            //    values: new object[] { new DateTime(2025, 1, 6, 11, 54, 0, 919, DateTimeKind.Local).AddTicks(8247), new DateTime(2025, 1, 6, 11, 54, 0, 919, DateTimeKind.Local).AddTicks(8248) });

            //migrationBuilder.UpdateData(
            //    table: "Wallet",
            //    keyColumn: "Id",
            //    keyValue: 8L,
            //    columns: new[] { "CreatedDate", "UpdatedDate" },
            //    values: new object[] { new DateTime(2025, 1, 6, 11, 54, 0, 919, DateTimeKind.Local).AddTicks(8254), new DateTime(2025, 1, 6, 11, 54, 0, 919, DateTimeKind.Local).AddTicks(8255) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessageType",
                table: "UserNotes");

            //migrationBuilder.AlterColumn<DateTime>(
            //    name: "UpdatedDate",
            //    table: "Wallet",
            //    type: "datetime2",
            //    nullable: false,
            //    defaultValue: new DateTime(2024, 12, 24, 10, 48, 13, 413, DateTimeKind.Local).AddTicks(6462),
            //    oldClrType: typeof(DateTime),
            //    oldType: "datetime2",
            //    oldDefaultValue: new DateTime(2025, 1, 6, 11, 54, 0, 917, DateTimeKind.Local).AddTicks(5140));

            //migrationBuilder.AlterColumn<DateTime>(
            //    name: "CreatedDate",
            //    table: "Wallet",
            //    type: "datetime2",
            //    nullable: false,
            //    defaultValue: new DateTime(2024, 12, 24, 10, 48, 13, 413, DateTimeKind.Local).AddTicks(5470),
            //    oldClrType: typeof(DateTime),
            //    oldType: "datetime2",
            //    oldDefaultValue: new DateTime(2025, 1, 6, 11, 54, 0, 917, DateTimeKind.Local).AddTicks(4511));

            //migrationBuilder.AlterColumn<DateTime>(
            //    name: "CreatedDate",
            //    table: "Transaction",
            //    type: "datetime2",
            //    nullable: false,
            //    defaultValue: new DateTime(2024, 12, 24, 10, 48, 13, 394, DateTimeKind.Local).AddTicks(882),
            //    oldClrType: typeof(DateTime),
            //    oldType: "datetime2",
            //    oldDefaultValue: new DateTime(2025, 1, 6, 11, 54, 0, 902, DateTimeKind.Local).AddTicks(7949));

            //migrationBuilder.AlterColumn<DateTime>(
            //    name: "UpdatedDate",
            //    table: "KYCDocuments",
            //    type: "datetime2",
            //    nullable: false,
            //    defaultValue: new DateTime(2024, 12, 24, 10, 48, 13, 345, DateTimeKind.Local).AddTicks(3127),
            //    oldClrType: typeof(DateTime),
            //    oldType: "datetime2",
            //    oldDefaultValue: new DateTime(2025, 1, 6, 11, 54, 0, 859, DateTimeKind.Local).AddTicks(8534));

            //migrationBuilder.AlterColumn<DateTime>(
            //    name: "CreatedDate",
            //    table: "KYCDocuments",
            //    type: "datetime2",
            //    nullable: false,
            //    defaultValue: new DateTime(2024, 12, 24, 10, 48, 13, 342, DateTimeKind.Local).AddTicks(4223),
            //    oldClrType: typeof(DateTime),
            //    oldType: "datetime2",
            //    oldDefaultValue: new DateTime(2025, 1, 6, 11, 54, 0, 856, DateTimeKind.Local).AddTicks(8779));

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "3a857bcf-f196-4295-8687-64628382d568", "AQAAAAEAACcQAAAAEEH/JB3Jy22w46P4rwFUJ0EghasBLDtUwKLnVXzSzcWIswvSWMf4CTPp0AS2XW2M0w==", "bef12019-29f7-4939-bf21-5cc0f3cd0c53" });

            //migrationBuilder.UpdateData(
            //    table: "UserDetail",
            //    keyColumn: "Id",
            //    keyValue: 1,
            //    columns: new[] { "CreatedDate", "UpdatedDate" },
            //    values: new object[] { new DateTime(2024, 12, 24, 10, 48, 13, 379, DateTimeKind.Local).AddTicks(9990), new DateTime(2024, 12, 24, 10, 48, 13, 380, DateTimeKind.Local).AddTicks(724) });

            //migrationBuilder.UpdateData(
            //    table: "Wallet",
            //    keyColumn: "Id",
            //    keyValue: 1L,
            //    columns: new[] { "CreatedDate", "UpdatedDate" },
            //    values: new object[] { new DateTime(2024, 12, 24, 10, 48, 13, 415, DateTimeKind.Local).AddTicks(9806), new DateTime(2024, 12, 24, 10, 48, 13, 416, DateTimeKind.Local).AddTicks(531) });

            //migrationBuilder.UpdateData(
            //    table: "Wallet",
            //    keyColumn: "Id",
            //    keyValue: 2L,
            //    columns: new[] { "CreatedDate", "UpdatedDate" },
            //    values: new object[] { new DateTime(2024, 12, 24, 10, 48, 13, 416, DateTimeKind.Local).AddTicks(1454), new DateTime(2024, 12, 24, 10, 48, 13, 416, DateTimeKind.Local).AddTicks(1463) });

            //migrationBuilder.UpdateData(
            //    table: "Wallet",
            //    keyColumn: "Id",
            //    keyValue: 3L,
            //    columns: new[] { "CreatedDate", "UpdatedDate" },
            //    values: new object[] { new DateTime(2024, 12, 24, 10, 48, 13, 416, DateTimeKind.Local).AddTicks(1475), new DateTime(2024, 12, 24, 10, 48, 13, 416, DateTimeKind.Local).AddTicks(1477) });

            //migrationBuilder.UpdateData(
            //    table: "Wallet",
            //    keyColumn: "Id",
            //    keyValue: 4L,
            //    columns: new[] { "CreatedDate", "UpdatedDate" },
            //    values: new object[] { new DateTime(2024, 12, 24, 10, 48, 13, 416, DateTimeKind.Local).AddTicks(1485), new DateTime(2024, 12, 24, 10, 48, 13, 416, DateTimeKind.Local).AddTicks(1486) });

            //migrationBuilder.UpdateData(
            //    table: "Wallet",
            //    keyColumn: "Id",
            //    keyValue: 5L,
            //    columns: new[] { "CreatedDate", "UpdatedDate" },
            //    values: new object[] { new DateTime(2024, 12, 24, 10, 48, 13, 416, DateTimeKind.Local).AddTicks(1492), new DateTime(2024, 12, 24, 10, 48, 13, 416, DateTimeKind.Local).AddTicks(1494) });

            //migrationBuilder.UpdateData(
            //    table: "Wallet",
            //    keyColumn: "Id",
            //    keyValue: 6L,
            //    columns: new[] { "CreatedDate", "UpdatedDate" },
            //    values: new object[] { new DateTime(2024, 12, 24, 10, 48, 13, 416, DateTimeKind.Local).AddTicks(1530), new DateTime(2024, 12, 24, 10, 48, 13, 416, DateTimeKind.Local).AddTicks(1532) });

            //migrationBuilder.UpdateData(
            //    table: "Wallet",
            //    keyColumn: "Id",
            //    keyValue: 7L,
            //    columns: new[] { "CreatedDate", "UpdatedDate" },
            //    values: new object[] { new DateTime(2024, 12, 24, 10, 48, 13, 416, DateTimeKind.Local).AddTicks(1551), new DateTime(2024, 12, 24, 10, 48, 13, 416, DateTimeKind.Local).AddTicks(1553) });

            //migrationBuilder.UpdateData(
            //    table: "Wallet",
            //    keyColumn: "Id",
            //    keyValue: 8L,
            //    columns: new[] { "CreatedDate", "UpdatedDate" },
            //    values: new object[] { new DateTime(2024, 12, 24, 10, 48, 13, 416, DateTimeKind.Local).AddTicks(1560), new DateTime(2024, 12, 24, 10, 48, 13, 416, DateTimeKind.Local).AddTicks(1561) });
        }
    }
}
