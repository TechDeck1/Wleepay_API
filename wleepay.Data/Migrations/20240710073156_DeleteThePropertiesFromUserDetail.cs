using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class DeleteThePropertiesFromUserDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                defaultValue: new DateTime(2024, 7, 10, 13, 1, 55, 906, DateTimeKind.Local).AddTicks(1812),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 9, 16, 8, 13, 744, DateTimeKind.Local).AddTicks(7298));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 10, 13, 1, 55, 906, DateTimeKind.Local).AddTicks(1263),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 9, 16, 8, 13, 744, DateTimeKind.Local).AddTicks(6870));

            migrationBuilder.AlterColumn<int>(
                name: "FailedLoginAttempt",
                table: "UserDetail",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 10, 13, 1, 55, 878, DateTimeKind.Local).AddTicks(623),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 9, 16, 8, 13, 736, DateTimeKind.Local).AddTicks(4674));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 10, 13, 1, 55, 837, DateTimeKind.Local).AddTicks(3082),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 9, 16, 8, 13, 710, DateTimeKind.Local).AddTicks(7274));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 10, 13, 1, 55, 834, DateTimeKind.Local).AddTicks(5806),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 9, 16, 8, 13, 709, DateTimeKind.Local).AddTicks(4837));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ffd545d1-43ff-4671-a221-be282044d530", "AQAAAAEAACcQAAAAENK/hp/W3eMbcyjBIJfaTJccJPdHdtlxhOiJfghscCQbH3bHE8+JapojhAtvUTyswQ==", "584063a7-514c-4aa6-ba3d-221f8169918c" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 10, 13, 1, 55, 866, DateTimeKind.Local).AddTicks(1594), new DateTime(2024, 7, 10, 13, 1, 55, 866, DateTimeKind.Local).AddTicks(2147) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 10, 13, 1, 55, 907, DateTimeKind.Local).AddTicks(5721), new DateTime(2024, 7, 10, 13, 1, 55, 907, DateTimeKind.Local).AddTicks(6040) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 10, 13, 1, 55, 907, DateTimeKind.Local).AddTicks(6496), new DateTime(2024, 7, 10, 13, 1, 55, 907, DateTimeKind.Local).AddTicks(6501) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 10, 13, 1, 55, 907, DateTimeKind.Local).AddTicks(6511), new DateTime(2024, 7, 10, 13, 1, 55, 907, DateTimeKind.Local).AddTicks(6512) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 10, 13, 1, 55, 907, DateTimeKind.Local).AddTicks(6517), new DateTime(2024, 7, 10, 13, 1, 55, 907, DateTimeKind.Local).AddTicks(6519) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 10, 13, 1, 55, 907, DateTimeKind.Local).AddTicks(6584), new DateTime(2024, 7, 10, 13, 1, 55, 907, DateTimeKind.Local).AddTicks(6585) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 10, 13, 1, 55, 907, DateTimeKind.Local).AddTicks(6592), new DateTime(2024, 7, 10, 13, 1, 55, 907, DateTimeKind.Local).AddTicks(6593) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 9, 16, 8, 13, 744, DateTimeKind.Local).AddTicks(7298),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 10, 13, 1, 55, 906, DateTimeKind.Local).AddTicks(1812));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 9, 16, 8, 13, 744, DateTimeKind.Local).AddTicks(6870),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 10, 13, 1, 55, 906, DateTimeKind.Local).AddTicks(1263));

            migrationBuilder.AlterColumn<int>(
                name: "FailedLoginAttempt",
                table: "UserDetail",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DeviceId",
                table: "UserDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FcmToken",
                table: "UserDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 9, 16, 8, 13, 736, DateTimeKind.Local).AddTicks(4674),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 10, 13, 1, 55, 878, DateTimeKind.Local).AddTicks(623));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 9, 16, 8, 13, 710, DateTimeKind.Local).AddTicks(7274),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 10, 13, 1, 55, 837, DateTimeKind.Local).AddTicks(3082));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 9, 16, 8, 13, 709, DateTimeKind.Local).AddTicks(4837),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 10, 13, 1, 55, 834, DateTimeKind.Local).AddTicks(5806));

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
    }
}
