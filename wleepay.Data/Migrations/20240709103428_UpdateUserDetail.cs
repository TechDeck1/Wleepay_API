using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class UpdateUserDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 9, 16, 4, 27, 254, DateTimeKind.Local).AddTicks(4592),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 9, 16, 2, 41, 832, DateTimeKind.Local).AddTicks(3579));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 9, 16, 4, 27, 254, DateTimeKind.Local).AddTicks(3910),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 9, 16, 2, 41, 832, DateTimeKind.Local).AddTicks(3000));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 9, 16, 4, 27, 241, DateTimeKind.Local).AddTicks(1200),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 9, 16, 2, 41, 823, DateTimeKind.Local).AddTicks(5504));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 9, 16, 4, 27, 194, DateTimeKind.Local).AddTicks(4927),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 9, 16, 2, 41, 797, DateTimeKind.Local).AddTicks(1213));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 9, 16, 4, 27, 192, DateTimeKind.Local).AddTicks(5730),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 9, 16, 2, 41, 795, DateTimeKind.Local).AddTicks(8418));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3c4b2637-6775-4ecc-96e4-dbc390bd2ebd", "AQAAAAEAACcQAAAAEDRbqjKtvuy8sy9itASqpuxJZ7bMLwNx6j1VNAPaMjJBZaR87pC12w9F9kRfKxMnqQ==", "4903ea7f-faa8-4c06-bfda-1c329cd4b553" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 9, 16, 4, 27, 224, DateTimeKind.Local).AddTicks(3672), new DateTime(2024, 7, 9, 16, 4, 27, 224, DateTimeKind.Local).AddTicks(4196) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 9, 16, 4, 27, 256, DateTimeKind.Local).AddTicks(2711), new DateTime(2024, 7, 9, 16, 4, 27, 256, DateTimeKind.Local).AddTicks(3147) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 9, 16, 4, 27, 256, DateTimeKind.Local).AddTicks(3719), new DateTime(2024, 7, 9, 16, 4, 27, 256, DateTimeKind.Local).AddTicks(3724) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 9, 16, 4, 27, 256, DateTimeKind.Local).AddTicks(3736), new DateTime(2024, 7, 9, 16, 4, 27, 256, DateTimeKind.Local).AddTicks(3737) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 9, 16, 4, 27, 256, DateTimeKind.Local).AddTicks(3755), new DateTime(2024, 7, 9, 16, 4, 27, 256, DateTimeKind.Local).AddTicks(3757) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 9, 16, 4, 27, 256, DateTimeKind.Local).AddTicks(3763), new DateTime(2024, 7, 9, 16, 4, 27, 256, DateTimeKind.Local).AddTicks(3765) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 9, 16, 4, 27, 256, DateTimeKind.Local).AddTicks(3775), new DateTime(2024, 7, 9, 16, 4, 27, 256, DateTimeKind.Local).AddTicks(3777) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 9, 16, 2, 41, 832, DateTimeKind.Local).AddTicks(3579),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 9, 16, 4, 27, 254, DateTimeKind.Local).AddTicks(4592));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 9, 16, 2, 41, 832, DateTimeKind.Local).AddTicks(3000),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 9, 16, 4, 27, 254, DateTimeKind.Local).AddTicks(3910));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 9, 16, 2, 41, 823, DateTimeKind.Local).AddTicks(5504),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 9, 16, 4, 27, 241, DateTimeKind.Local).AddTicks(1200));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 9, 16, 2, 41, 797, DateTimeKind.Local).AddTicks(1213),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 9, 16, 4, 27, 194, DateTimeKind.Local).AddTicks(4927));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 9, 16, 2, 41, 795, DateTimeKind.Local).AddTicks(8418),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 9, 16, 4, 27, 192, DateTimeKind.Local).AddTicks(5730));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "06af0db9-2028-4e93-a017-d3ce1546dee8", "AQAAAAEAACcQAAAAEF6eopBFZ+OTbMl//1Xa74iALpIQ9rhvC6XFDpamW4slVGZmGfTVyD+HgcI4eua+fw==", "24958fd1-2ec4-4f9e-9868-0ae6223772a1" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 9, 16, 2, 41, 817, DateTimeKind.Local).AddTicks(3008), new DateTime(2024, 7, 9, 16, 2, 41, 817, DateTimeKind.Local).AddTicks(3357) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 9, 16, 2, 41, 833, DateTimeKind.Local).AddTicks(7069), new DateTime(2024, 7, 9, 16, 2, 41, 833, DateTimeKind.Local).AddTicks(7855) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 9, 16, 2, 41, 833, DateTimeKind.Local).AddTicks(8807), new DateTime(2024, 7, 9, 16, 2, 41, 833, DateTimeKind.Local).AddTicks(8811) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 9, 16, 2, 41, 833, DateTimeKind.Local).AddTicks(8820), new DateTime(2024, 7, 9, 16, 2, 41, 833, DateTimeKind.Local).AddTicks(8821) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 9, 16, 2, 41, 833, DateTimeKind.Local).AddTicks(8879), new DateTime(2024, 7, 9, 16, 2, 41, 833, DateTimeKind.Local).AddTicks(8880) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 9, 16, 2, 41, 833, DateTimeKind.Local).AddTicks(8885), new DateTime(2024, 7, 9, 16, 2, 41, 833, DateTimeKind.Local).AddTicks(8887) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 9, 16, 2, 41, 833, DateTimeKind.Local).AddTicks(8895), new DateTime(2024, 7, 9, 16, 2, 41, 833, DateTimeKind.Local).AddTicks(8896) });
        }
    }
}
