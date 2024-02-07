using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class updateCashFlowHistoryTableForPreviousAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 12, 29, 35, 429, DateTimeKind.Local).AddTicks(3800),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 12, 1, 50, 74, DateTimeKind.Local).AddTicks(7053));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 12, 29, 35, 429, DateTimeKind.Local).AddTicks(3385),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 12, 1, 50, 74, DateTimeKind.Local).AddTicks(6592));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 12, 29, 35, 423, DateTimeKind.Local).AddTicks(4365),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 12, 1, 50, 68, DateTimeKind.Local).AddTicks(5071));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 12, 29, 35, 403, DateTimeKind.Local).AddTicks(3753),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 12, 1, 50, 47, DateTimeKind.Local).AddTicks(8810));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 12, 29, 35, 402, DateTimeKind.Local).AddTicks(39),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 12, 1, 50, 46, DateTimeKind.Local).AddTicks(4293));

            migrationBuilder.AddColumn<double>(
                name: "PreviousAmount",
                table: "CashFlowHistory",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e0fc7c0d-8115-496e-952b-fe9600d91b8f", "AQAAAAEAACcQAAAAEGaSlcGdev0Gxo3D/Js2l7EXJFQm28WO08XgoEONhLpHgbQumAX+/lGm99JloESW6Q==", "e3b5e657-cc65-4e17-ae5d-fc35fc7aef66" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 12, 29, 35, 419, DateTimeKind.Local).AddTicks(261), new DateTime(2024, 2, 7, 12, 29, 35, 419, DateTimeKind.Local).AddTicks(458) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 12, 29, 35, 430, DateTimeKind.Local).AddTicks(3351), new DateTime(2024, 2, 7, 12, 29, 35, 430, DateTimeKind.Local).AddTicks(3575) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 12, 29, 35, 430, DateTimeKind.Local).AddTicks(3859), new DateTime(2024, 2, 7, 12, 29, 35, 430, DateTimeKind.Local).AddTicks(3863) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 12, 29, 35, 430, DateTimeKind.Local).AddTicks(3869), new DateTime(2024, 2, 7, 12, 29, 35, 430, DateTimeKind.Local).AddTicks(3870) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 12, 29, 35, 430, DateTimeKind.Local).AddTicks(3874), new DateTime(2024, 2, 7, 12, 29, 35, 430, DateTimeKind.Local).AddTicks(3875) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 12, 29, 35, 430, DateTimeKind.Local).AddTicks(3926), new DateTime(2024, 2, 7, 12, 29, 35, 430, DateTimeKind.Local).AddTicks(3927) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 12, 29, 35, 430, DateTimeKind.Local).AddTicks(3933), new DateTime(2024, 2, 7, 12, 29, 35, 430, DateTimeKind.Local).AddTicks(3935) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreviousAmount",
                table: "CashFlowHistory");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 12, 1, 50, 74, DateTimeKind.Local).AddTicks(7053),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 12, 29, 35, 429, DateTimeKind.Local).AddTicks(3800));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 12, 1, 50, 74, DateTimeKind.Local).AddTicks(6592),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 12, 29, 35, 429, DateTimeKind.Local).AddTicks(3385));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 12, 1, 50, 68, DateTimeKind.Local).AddTicks(5071),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 12, 29, 35, 423, DateTimeKind.Local).AddTicks(4365));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 12, 1, 50, 47, DateTimeKind.Local).AddTicks(8810),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 12, 29, 35, 403, DateTimeKind.Local).AddTicks(3753));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 12, 1, 50, 46, DateTimeKind.Local).AddTicks(4293),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 12, 29, 35, 402, DateTimeKind.Local).AddTicks(39));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b3406e61-82f4-45be-81a4-01c435af7f28", "AQAAAAEAACcQAAAAEFbZFcaFaaRML+A3DFz5VROmvpPirimJsZ8cDuwqm18QrYnqyzwA3uvKt+Om+3llyQ==", "d8450e84-7736-4b3b-b8a0-af4c91333f6c" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 12, 1, 50, 64, DateTimeKind.Local).AddTicks(1247), new DateTime(2024, 2, 7, 12, 1, 50, 64, DateTimeKind.Local).AddTicks(1443) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 12, 1, 50, 75, DateTimeKind.Local).AddTicks(9260), new DateTime(2024, 2, 7, 12, 1, 50, 75, DateTimeKind.Local).AddTicks(9438) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 12, 1, 50, 75, DateTimeKind.Local).AddTicks(9718), new DateTime(2024, 2, 7, 12, 1, 50, 75, DateTimeKind.Local).AddTicks(9721) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 12, 1, 50, 75, DateTimeKind.Local).AddTicks(9728), new DateTime(2024, 2, 7, 12, 1, 50, 75, DateTimeKind.Local).AddTicks(9729) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 12, 1, 50, 75, DateTimeKind.Local).AddTicks(9733), new DateTime(2024, 2, 7, 12, 1, 50, 75, DateTimeKind.Local).AddTicks(9734) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 12, 1, 50, 75, DateTimeKind.Local).AddTicks(9782), new DateTime(2024, 2, 7, 12, 1, 50, 75, DateTimeKind.Local).AddTicks(9783) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 12, 1, 50, 75, DateTimeKind.Local).AddTicks(9790), new DateTime(2024, 2, 7, 12, 1, 50, 75, DateTimeKind.Local).AddTicks(9791) });
        }
    }
}
