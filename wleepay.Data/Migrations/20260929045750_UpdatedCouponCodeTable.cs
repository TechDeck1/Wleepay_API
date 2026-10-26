using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class UpdatedCouponCodeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 9, 29, 10, 27, 49, 341, DateTimeKind.Local).AddTicks(3396),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 9, 24, 15, 40, 55, 146, DateTimeKind.Local).AddTicks(7914));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 9, 29, 10, 27, 49, 341, DateTimeKind.Local).AddTicks(2907),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 9, 24, 15, 40, 55, 146, DateTimeKind.Local).AddTicks(7440));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 9, 29, 10, 27, 49, 329, DateTimeKind.Local).AddTicks(4966),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 9, 24, 15, 40, 55, 136, DateTimeKind.Local).AddTicks(870));

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 9, 29, 10, 27, 49, 299, DateTimeKind.Local).AddTicks(1272),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 9, 24, 15, 40, 55, 105, DateTimeKind.Local).AddTicks(2751));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 9, 29, 10, 27, 49, 297, DateTimeKind.Local).AddTicks(7273),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 9, 24, 15, 40, 55, 103, DateTimeKind.Local).AddTicks(8596));

            migrationBuilder.AddColumn<bool>(
                name: "IsExposed",
                table: "CouponCode",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsExposed",
                table: "CouponCode");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 9, 24, 15, 40, 55, 146, DateTimeKind.Local).AddTicks(7914),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 9, 29, 10, 27, 49, 341, DateTimeKind.Local).AddTicks(3396));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 9, 24, 15, 40, 55, 146, DateTimeKind.Local).AddTicks(7440),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 9, 29, 10, 27, 49, 341, DateTimeKind.Local).AddTicks(2907));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 9, 24, 15, 40, 55, 136, DateTimeKind.Local).AddTicks(870),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 9, 29, 10, 27, 49, 329, DateTimeKind.Local).AddTicks(4966));

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 9, 24, 15, 40, 55, 105, DateTimeKind.Local).AddTicks(2751),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 9, 29, 10, 27, 49, 299, DateTimeKind.Local).AddTicks(1272));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 9, 24, 15, 40, 55, 103, DateTimeKind.Local).AddTicks(8596),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 9, 29, 10, 27, 49, 297, DateTimeKind.Local).AddTicks(7273));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a132dcc4-1473-47f7-916f-b3b538836c96", "AQAAAAEAACcQAAAAEBrsdKG7/HdmzwNWy0Rb62TY/Dds8EkSCvaZ6TO86x/PJ6p0o09mb60ttkZtIo8PKA==", "e83a9cae-ec1b-42f2-af80-4b1b4e5eedb2" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 9, 24, 15, 40, 55, 128, DateTimeKind.Local).AddTicks(4810), new DateTime(2026, 9, 24, 15, 40, 55, 128, DateTimeKind.Local).AddTicks(5188) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 9, 24, 15, 40, 55, 148, DateTimeKind.Local).AddTicks(3166), new DateTime(2026, 9, 24, 15, 40, 55, 148, DateTimeKind.Local).AddTicks(3519) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 9, 24, 15, 40, 55, 148, DateTimeKind.Local).AddTicks(4071), new DateTime(2026, 9, 24, 15, 40, 55, 148, DateTimeKind.Local).AddTicks(4076) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 9, 24, 15, 40, 55, 148, DateTimeKind.Local).AddTicks(4086), new DateTime(2026, 9, 24, 15, 40, 55, 148, DateTimeKind.Local).AddTicks(4087) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 9, 24, 15, 40, 55, 148, DateTimeKind.Local).AddTicks(4092), new DateTime(2026, 9, 24, 15, 40, 55, 148, DateTimeKind.Local).AddTicks(4094) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 9, 24, 15, 40, 55, 148, DateTimeKind.Local).AddTicks(4099), new DateTime(2026, 9, 24, 15, 40, 55, 148, DateTimeKind.Local).AddTicks(4100) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 9, 24, 15, 40, 55, 148, DateTimeKind.Local).AddTicks(4109), new DateTime(2026, 9, 24, 15, 40, 55, 148, DateTimeKind.Local).AddTicks(4110) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 9, 24, 15, 40, 55, 148, DateTimeKind.Local).AddTicks(4182), new DateTime(2026, 9, 24, 15, 40, 55, 148, DateTimeKind.Local).AddTicks(4183) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 9, 24, 15, 40, 55, 148, DateTimeKind.Local).AddTicks(4190), new DateTime(2026, 9, 24, 15, 40, 55, 148, DateTimeKind.Local).AddTicks(4191) });
        }
    }
}
