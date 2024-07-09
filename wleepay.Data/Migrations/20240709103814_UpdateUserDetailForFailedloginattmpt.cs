using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class UpdateUserDetailForFailedloginattmpt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 9, 16, 8, 13, 744, DateTimeKind.Local).AddTicks(7298),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 9, 16, 4, 27, 254, DateTimeKind.Local).AddTicks(4592));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 9, 16, 8, 13, 744, DateTimeKind.Local).AddTicks(6870),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 9, 16, 4, 27, 254, DateTimeKind.Local).AddTicks(3910));

            migrationBuilder.AddColumn<int>(
                name: "FailedLoginAttempt",
                table: "UserDetail",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FcmToken",
                table: "UserDetail",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeviceId",
                table: "UserDetail",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");


            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 9, 16, 8, 13, 736, DateTimeKind.Local).AddTicks(4674),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 9, 16, 4, 27, 241, DateTimeKind.Local).AddTicks(1200));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 9, 16, 8, 13, 710, DateTimeKind.Local).AddTicks(7274),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 9, 16, 4, 27, 194, DateTimeKind.Local).AddTicks(4927));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 9, 16, 8, 13, 709, DateTimeKind.Local).AddTicks(4837),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 9, 16, 4, 27, 192, DateTimeKind.Local).AddTicks(5730));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3471df67-66ca-4037-929f-eba18d658792", "AQAAAAEAACcQAAAAEB6dzg1qnBXiJ+Nui1EJSuovA2UWI+SjuFg96lsjQJFx/Vk1iQbQzd5eKGBIr5M1Eg==", "d06ace9d-4f6b-4db7-bdfb-45922dba6678" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 9, 16, 8, 13, 730, DateTimeKind.Local).AddTicks(4653), new DateTime(2024, 7, 9, 16, 8, 13, 730, DateTimeKind.Local).AddTicks(4995) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 9, 16, 8, 13, 745, DateTimeKind.Local).AddTicks(9303), new DateTime(2024, 7, 9, 16, 8, 13, 745, DateTimeKind.Local).AddTicks(9614) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 9, 16, 8, 13, 746, DateTimeKind.Local).AddTicks(7), new DateTime(2024, 7, 9, 16, 8, 13, 746, DateTimeKind.Local).AddTicks(12) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 9, 16, 8, 13, 746, DateTimeKind.Local).AddTicks(20), new DateTime(2024, 7, 9, 16, 8, 13, 746, DateTimeKind.Local).AddTicks(21) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 9, 16, 8, 13, 746, DateTimeKind.Local).AddTicks(26), new DateTime(2024, 7, 9, 16, 8, 13, 746, DateTimeKind.Local).AddTicks(28) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 9, 16, 8, 13, 746, DateTimeKind.Local).AddTicks(32), new DateTime(2024, 7, 9, 16, 8, 13, 746, DateTimeKind.Local).AddTicks(34) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 9, 16, 8, 13, 746, DateTimeKind.Local).AddTicks(41), new DateTime(2024, 7, 9, 16, 8, 13, 746, DateTimeKind.Local).AddTicks(42) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FailedLoginAttempts",
                table: "UserDetail");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "UserDetail");

            migrationBuilder.DropColumn(
                name: "FcmToken",
                table: "UserDetail");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 9, 16, 4, 27, 254, DateTimeKind.Local).AddTicks(4592),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 9, 16, 8, 13, 744, DateTimeKind.Local).AddTicks(7298));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 9, 16, 4, 27, 254, DateTimeKind.Local).AddTicks(3910),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 9, 16, 8, 13, 744, DateTimeKind.Local).AddTicks(6870));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 9, 16, 4, 27, 241, DateTimeKind.Local).AddTicks(1200),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 9, 16, 8, 13, 736, DateTimeKind.Local).AddTicks(4674));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 9, 16, 4, 27, 194, DateTimeKind.Local).AddTicks(4927),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 9, 16, 8, 13, 710, DateTimeKind.Local).AddTicks(7274));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 9, 16, 4, 27, 192, DateTimeKind.Local).AddTicks(5730),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 9, 16, 8, 13, 709, DateTimeKind.Local).AddTicks(4837));

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
    }
}
