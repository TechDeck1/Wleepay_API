using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class updateTableForOtpVerification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 24, 16, 17, 55, 772, DateTimeKind.Local).AddTicks(658),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 24, 15, 14, 30, 800, DateTimeKind.Local).AddTicks(6408));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 24, 16, 17, 55, 772, DateTimeKind.Local).AddTicks(241),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 24, 15, 14, 30, 800, DateTimeKind.Local).AddTicks(6063));

            migrationBuilder.AlterColumn<DateTime>(
                name: "VerificationExpiry",
                table: "Verification",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 24, 16, 17, 55, 763, DateTimeKind.Local).AddTicks(592),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 24, 15, 14, 30, 794, DateTimeKind.Local).AddTicks(6323));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 24, 16, 17, 55, 727, DateTimeKind.Local).AddTicks(3566),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 24, 15, 14, 30, 758, DateTimeKind.Local).AddTicks(266));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 24, 16, 17, 55, 725, DateTimeKind.Local).AddTicks(3814),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 24, 15, 14, 30, 755, DateTimeKind.Local).AddTicks(6794));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f0c56e5c-c7c0-439e-8394-d9b40ec49dcd", "AQAAAAEAACcQAAAAECC5ffF8n9lPDiGdpS26u5iHvkYGXUwKQaB1ztx3v7EGqzR3ZipVXSo1Tc9n56qpsg==", "284d456a-1d61-40ab-9419-11411438e71e" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 24, 16, 17, 55, 756, DateTimeKind.Local).AddTicks(7370), new DateTime(2024, 1, 24, 16, 17, 55, 756, DateTimeKind.Local).AddTicks(7591) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 24, 16, 17, 55, 772, DateTimeKind.Local).AddTicks(9526), new DateTime(2024, 1, 24, 16, 17, 55, 772, DateTimeKind.Local).AddTicks(9708) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 24, 16, 17, 55, 772, DateTimeKind.Local).AddTicks(9969), new DateTime(2024, 1, 24, 16, 17, 55, 772, DateTimeKind.Local).AddTicks(9973) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 24, 16, 17, 55, 772, DateTimeKind.Local).AddTicks(9978), new DateTime(2024, 1, 24, 16, 17, 55, 772, DateTimeKind.Local).AddTicks(9979) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 24, 16, 17, 55, 772, DateTimeKind.Local).AddTicks(9983), new DateTime(2024, 1, 24, 16, 17, 55, 772, DateTimeKind.Local).AddTicks(9984) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 24, 16, 17, 55, 772, DateTimeKind.Local).AddTicks(9988), new DateTime(2024, 1, 24, 16, 17, 55, 772, DateTimeKind.Local).AddTicks(9989) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 24, 16, 17, 55, 773, DateTimeKind.Local).AddTicks(35), new DateTime(2024, 1, 24, 16, 17, 55, 773, DateTimeKind.Local).AddTicks(36) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 24, 15, 14, 30, 800, DateTimeKind.Local).AddTicks(6408),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 24, 16, 17, 55, 772, DateTimeKind.Local).AddTicks(658));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 24, 15, 14, 30, 800, DateTimeKind.Local).AddTicks(6063),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 24, 16, 17, 55, 772, DateTimeKind.Local).AddTicks(241));

            migrationBuilder.AlterColumn<string>(
                name: "VerificationExpiry",
                table: "Verification",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 24, 15, 14, 30, 794, DateTimeKind.Local).AddTicks(6323),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 24, 16, 17, 55, 763, DateTimeKind.Local).AddTicks(592));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 24, 15, 14, 30, 758, DateTimeKind.Local).AddTicks(266),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 24, 16, 17, 55, 727, DateTimeKind.Local).AddTicks(3566));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 24, 15, 14, 30, 755, DateTimeKind.Local).AddTicks(6794),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 24, 16, 17, 55, 725, DateTimeKind.Local).AddTicks(3814));

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
    }
}
