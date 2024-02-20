using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class addNewCoulmnInCustomerTicketTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 20, 18, 2, 17, 822, DateTimeKind.Local).AddTicks(6299),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 8, 18, 32, 10, 999, DateTimeKind.Local).AddTicks(575));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 20, 18, 2, 17, 822, DateTimeKind.Local).AddTicks(5895),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 8, 18, 32, 10, 999, DateTimeKind.Local).AddTicks(164));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 20, 18, 2, 17, 816, DateTimeKind.Local).AddTicks(2189),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 8, 18, 32, 10, 992, DateTimeKind.Local).AddTicks(5466));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 20, 18, 2, 17, 790, DateTimeKind.Local).AddTicks(9818),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 8, 18, 32, 10, 971, DateTimeKind.Local).AddTicks(8763));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 20, 18, 2, 17, 789, DateTimeKind.Local).AddTicks(665),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 8, 18, 32, 10, 970, DateTimeKind.Local).AddTicks(4794));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CustomerTickets",
                type: "nvarchar(max)",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CustomerTickets");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 8, 18, 32, 10, 999, DateTimeKind.Local).AddTicks(575),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 20, 18, 2, 17, 822, DateTimeKind.Local).AddTicks(6299));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 8, 18, 32, 10, 999, DateTimeKind.Local).AddTicks(164),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 20, 18, 2, 17, 822, DateTimeKind.Local).AddTicks(5895));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 8, 18, 32, 10, 992, DateTimeKind.Local).AddTicks(5466),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 20, 18, 2, 17, 816, DateTimeKind.Local).AddTicks(2189));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 8, 18, 32, 10, 971, DateTimeKind.Local).AddTicks(8763),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 20, 18, 2, 17, 790, DateTimeKind.Local).AddTicks(9818));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 8, 18, 32, 10, 970, DateTimeKind.Local).AddTicks(4794),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 20, 18, 2, 17, 789, DateTimeKind.Local).AddTicks(665));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8ea4d2b5-b2da-41d2-95d6-b63dc4d15606", "AQAAAAEAACcQAAAAENcxDfhc+/CEb+r0zynLELZF0AzEAaOZ0s/28Ilqz+vBPnsdIh0RG9uITBL1FRU4VQ==", "1e442363-5e8b-4c93-b50c-90d308874889" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 8, 18, 32, 10, 988, DateTimeKind.Local).AddTicks(90), new DateTime(2024, 2, 8, 18, 32, 10, 988, DateTimeKind.Local).AddTicks(299) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 8, 18, 32, 10, 999, DateTimeKind.Local).AddTicks(9966), new DateTime(2024, 2, 8, 18, 32, 11, 0, DateTimeKind.Local).AddTicks(153) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 8, 18, 32, 11, 0, DateTimeKind.Local).AddTicks(425), new DateTime(2024, 2, 8, 18, 32, 11, 0, DateTimeKind.Local).AddTicks(428) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 8, 18, 32, 11, 0, DateTimeKind.Local).AddTicks(436), new DateTime(2024, 2, 8, 18, 32, 11, 0, DateTimeKind.Local).AddTicks(437) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 8, 18, 32, 11, 0, DateTimeKind.Local).AddTicks(441), new DateTime(2024, 2, 8, 18, 32, 11, 0, DateTimeKind.Local).AddTicks(442) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 8, 18, 32, 11, 0, DateTimeKind.Local).AddTicks(446), new DateTime(2024, 2, 8, 18, 32, 11, 0, DateTimeKind.Local).AddTicks(447) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 8, 18, 32, 11, 0, DateTimeKind.Local).AddTicks(497), new DateTime(2024, 2, 8, 18, 32, 11, 0, DateTimeKind.Local).AddTicks(498) });
        }
    }
}
