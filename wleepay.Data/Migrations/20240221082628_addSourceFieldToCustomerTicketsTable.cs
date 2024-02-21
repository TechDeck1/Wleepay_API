using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class addSourceFieldToCustomerTicketsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 21, 13, 56, 27, 841, DateTimeKind.Local).AddTicks(1102),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 20, 18, 2, 17, 822, DateTimeKind.Local).AddTicks(6299));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 21, 13, 56, 27, 841, DateTimeKind.Local).AddTicks(515),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 20, 18, 2, 17, 822, DateTimeKind.Local).AddTicks(5895));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 21, 13, 56, 27, 834, DateTimeKind.Local).AddTicks(1590),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 20, 18, 2, 17, 816, DateTimeKind.Local).AddTicks(2189));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 21, 13, 56, 27, 806, DateTimeKind.Local).AddTicks(6401),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 20, 18, 2, 17, 790, DateTimeKind.Local).AddTicks(9818));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 21, 13, 56, 27, 804, DateTimeKind.Local).AddTicks(1359),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 20, 18, 2, 17, 789, DateTimeKind.Local).AddTicks(665));

            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "CustomerTickets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1f54253-db9e-4e2a-b78b-278b565e9deb", "AQAAAAEAACcQAAAAEGk1IkbiKE3XxxqVTvdzCx82kZbA7GUpShCiadi4b7HXv/kryNmZZCTQbrnrCB2tuA==", "ea32bb5f-0d17-4a03-9956-73d34e7169e4" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 21, 13, 56, 27, 827, DateTimeKind.Local).AddTicks(5567), new DateTime(2024, 2, 21, 13, 56, 27, 827, DateTimeKind.Local).AddTicks(5763) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 21, 13, 56, 27, 842, DateTimeKind.Local).AddTicks(1727), new DateTime(2024, 2, 21, 13, 56, 27, 842, DateTimeKind.Local).AddTicks(1906) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 21, 13, 56, 27, 842, DateTimeKind.Local).AddTicks(2157), new DateTime(2024, 2, 21, 13, 56, 27, 842, DateTimeKind.Local).AddTicks(2161) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 21, 13, 56, 27, 842, DateTimeKind.Local).AddTicks(2166), new DateTime(2024, 2, 21, 13, 56, 27, 842, DateTimeKind.Local).AddTicks(2167) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 21, 13, 56, 27, 842, DateTimeKind.Local).AddTicks(2216), new DateTime(2024, 2, 21, 13, 56, 27, 842, DateTimeKind.Local).AddTicks(2217) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 21, 13, 56, 27, 842, DateTimeKind.Local).AddTicks(2221), new DateTime(2024, 2, 21, 13, 56, 27, 842, DateTimeKind.Local).AddTicks(2222) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 21, 13, 56, 27, 842, DateTimeKind.Local).AddTicks(2228), new DateTime(2024, 2, 21, 13, 56, 27, 842, DateTimeKind.Local).AddTicks(2229) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Source",
                table: "CustomerTickets");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 20, 18, 2, 17, 822, DateTimeKind.Local).AddTicks(6299),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 21, 13, 56, 27, 841, DateTimeKind.Local).AddTicks(1102));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 20, 18, 2, 17, 822, DateTimeKind.Local).AddTicks(5895),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 21, 13, 56, 27, 841, DateTimeKind.Local).AddTicks(515));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 20, 18, 2, 17, 816, DateTimeKind.Local).AddTicks(2189),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 21, 13, 56, 27, 834, DateTimeKind.Local).AddTicks(1590));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 20, 18, 2, 17, 790, DateTimeKind.Local).AddTicks(9818),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 21, 13, 56, 27, 806, DateTimeKind.Local).AddTicks(6401));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 20, 18, 2, 17, 789, DateTimeKind.Local).AddTicks(665),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 21, 13, 56, 27, 804, DateTimeKind.Local).AddTicks(1359));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "06c8b27e-4af0-4c16-9154-e68b12836576", "AQAAAAEAACcQAAAAEC6hU6Sd47FyAb+D030afwZEY4ioKsi0a8G7HZtKTn3xverFnB0pcvObedt0uPa8ow==", "c4ce1c3a-e177-41e0-b693-a0c124070207" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 20, 18, 2, 17, 811, DateTimeKind.Local).AddTicks(3184), new DateTime(2024, 2, 20, 18, 2, 17, 811, DateTimeKind.Local).AddTicks(3415) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 20, 18, 2, 17, 823, DateTimeKind.Local).AddTicks(6745), new DateTime(2024, 2, 20, 18, 2, 17, 823, DateTimeKind.Local).AddTicks(6941) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 20, 18, 2, 17, 823, DateTimeKind.Local).AddTicks(7229), new DateTime(2024, 2, 20, 18, 2, 17, 823, DateTimeKind.Local).AddTicks(7234) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 20, 18, 2, 17, 823, DateTimeKind.Local).AddTicks(7240), new DateTime(2024, 2, 20, 18, 2, 17, 823, DateTimeKind.Local).AddTicks(7241) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 20, 18, 2, 17, 823, DateTimeKind.Local).AddTicks(7246), new DateTime(2024, 2, 20, 18, 2, 17, 823, DateTimeKind.Local).AddTicks(7247) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 20, 18, 2, 17, 823, DateTimeKind.Local).AddTicks(7251), new DateTime(2024, 2, 20, 18, 2, 17, 823, DateTimeKind.Local).AddTicks(7252) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 20, 18, 2, 17, 823, DateTimeKind.Local).AddTicks(7259), new DateTime(2024, 2, 20, 18, 2, 17, 823, DateTimeKind.Local).AddTicks(7260) });
        }
    }
}
