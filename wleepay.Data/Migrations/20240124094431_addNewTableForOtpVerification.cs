using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class addNewTableForOtpVerification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 24, 15, 14, 30, 800, DateTimeKind.Local).AddTicks(6408),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 17, 7, 22, 387, DateTimeKind.Local).AddTicks(2484));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 24, 15, 14, 30, 800, DateTimeKind.Local).AddTicks(6063),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 17, 7, 22, 387, DateTimeKind.Local).AddTicks(2065));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 24, 15, 14, 30, 794, DateTimeKind.Local).AddTicks(6323),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 17, 7, 22, 375, DateTimeKind.Local).AddTicks(1955));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 24, 15, 14, 30, 758, DateTimeKind.Local).AddTicks(266),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 17, 7, 22, 342, DateTimeKind.Local).AddTicks(1576));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 24, 15, 14, 30, 755, DateTimeKind.Local).AddTicks(6794),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 17, 7, 22, 339, DateTimeKind.Local).AddTicks(9746));

            migrationBuilder.CreateTable(
                name: "Verification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VerificationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VerificationExpiry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verification", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "32e05c3a-d088-4a75-bd77-2d441c3fe3ed", "AQAAAAEAACcQAAAAEH2hYQJXThdvvgp35RDWwKz5NlbH3yoWs7HGD85+fgicxPnnQNa2Zbbr2IgIWmLEdw==", "2d7408d3-9ba2-4ed2-92b7-15b859206d6e" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 24, 15, 14, 30, 789, DateTimeKind.Local).AddTicks(5291), new DateTime(2024, 1, 24, 15, 14, 30, 789, DateTimeKind.Local).AddTicks(5476) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 24, 15, 14, 30, 801, DateTimeKind.Local).AddTicks(5136), new DateTime(2024, 1, 24, 15, 14, 30, 801, DateTimeKind.Local).AddTicks(5300) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 24, 15, 14, 30, 801, DateTimeKind.Local).AddTicks(5534), new DateTime(2024, 1, 24, 15, 14, 30, 801, DateTimeKind.Local).AddTicks(5537) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 24, 15, 14, 30, 801, DateTimeKind.Local).AddTicks(5543), new DateTime(2024, 1, 24, 15, 14, 30, 801, DateTimeKind.Local).AddTicks(5543) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 24, 15, 14, 30, 801, DateTimeKind.Local).AddTicks(5547), new DateTime(2024, 1, 24, 15, 14, 30, 801, DateTimeKind.Local).AddTicks(5548) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 24, 15, 14, 30, 801, DateTimeKind.Local).AddTicks(5551), new DateTime(2024, 1, 24, 15, 14, 30, 801, DateTimeKind.Local).AddTicks(5551) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 24, 15, 14, 30, 801, DateTimeKind.Local).AddTicks(5556), new DateTime(2024, 1, 24, 15, 14, 30, 801, DateTimeKind.Local).AddTicks(5557) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Verification");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 17, 7, 22, 387, DateTimeKind.Local).AddTicks(2484),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 24, 15, 14, 30, 800, DateTimeKind.Local).AddTicks(6408));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 17, 7, 22, 387, DateTimeKind.Local).AddTicks(2065),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 24, 15, 14, 30, 800, DateTimeKind.Local).AddTicks(6063));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 17, 7, 22, 375, DateTimeKind.Local).AddTicks(1955),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 24, 15, 14, 30, 794, DateTimeKind.Local).AddTicks(6323));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 17, 7, 22, 342, DateTimeKind.Local).AddTicks(1576),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 24, 15, 14, 30, 758, DateTimeKind.Local).AddTicks(266));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 17, 7, 22, 339, DateTimeKind.Local).AddTicks(9746),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 24, 15, 14, 30, 755, DateTimeKind.Local).AddTicks(6794));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6df86b33-47c9-4927-bc58-2b734d0ee1e9", "AQAAAAEAACcQAAAAEAuDIsVQmWIYtUioytYPjRVcVA4hb+QvJ6EW8j9REhbZ7qk2mTAXkIi/npmWB3eQTg==", "cbc86539-e915-48d5-aa24-ed82b339fdaf" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 17, 7, 22, 368, DateTimeKind.Local).AddTicks(1404), new DateTime(2024, 1, 23, 17, 7, 22, 368, DateTimeKind.Local).AddTicks(1599) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 17, 7, 22, 388, DateTimeKind.Local).AddTicks(2150), new DateTime(2024, 1, 23, 17, 7, 22, 388, DateTimeKind.Local).AddTicks(2331) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 17, 7, 22, 388, DateTimeKind.Local).AddTicks(2871), new DateTime(2024, 1, 23, 17, 7, 22, 388, DateTimeKind.Local).AddTicks(2874) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 17, 7, 22, 388, DateTimeKind.Local).AddTicks(2881), new DateTime(2024, 1, 23, 17, 7, 22, 388, DateTimeKind.Local).AddTicks(2883) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 17, 7, 22, 388, DateTimeKind.Local).AddTicks(2889), new DateTime(2024, 1, 23, 17, 7, 22, 388, DateTimeKind.Local).AddTicks(2891) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 17, 7, 22, 388, DateTimeKind.Local).AddTicks(2905), new DateTime(2024, 1, 23, 17, 7, 22, 388, DateTimeKind.Local).AddTicks(2906) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 17, 7, 22, 388, DateTimeKind.Local).AddTicks(2917), new DateTime(2024, 1, 23, 17, 7, 22, 388, DateTimeKind.Local).AddTicks(2918) });
        }
    }
}
