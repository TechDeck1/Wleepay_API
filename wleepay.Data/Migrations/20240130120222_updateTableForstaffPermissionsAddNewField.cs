using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class updateTableForstaffPermissionsAddNewField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 30, 17, 32, 20, 786, DateTimeKind.Local).AddTicks(5216),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 18, 33, 27, 290, DateTimeKind.Local).AddTicks(7310));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 30, 17, 32, 20, 786, DateTimeKind.Local).AddTicks(4113),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 18, 33, 27, 290, DateTimeKind.Local).AddTicks(6912));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 30, 17, 32, 20, 776, DateTimeKind.Local).AddTicks(7589),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 18, 33, 27, 284, DateTimeKind.Local).AddTicks(2348));

            migrationBuilder.AddColumn<string>(
                name: "Permissions",
                table: "StaffPermissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 30, 17, 32, 20, 744, DateTimeKind.Local).AddTicks(3791),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 18, 33, 27, 261, DateTimeKind.Local).AddTicks(2161));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 30, 17, 32, 20, 742, DateTimeKind.Local).AddTicks(1077),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 18, 33, 27, 258, DateTimeKind.Local).AddTicks(5001));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1fa7275f-14d1-429c-8b49-7a600256603d", "AQAAAAEAACcQAAAAEL7fJxkCdC0M9ELEhGJq3G+Pjd0gL6edgAfF4Rg6Hsmg2QW4H/WKQjTVBxm9QeVyWQ==", "1039477d-2cb9-42ec-ac5b-ec682b3ef7bb" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 30, 17, 32, 20, 769, DateTimeKind.Local).AddTicks(602), new DateTime(2024, 1, 30, 17, 32, 20, 769, DateTimeKind.Local).AddTicks(846) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 30, 17, 32, 20, 788, DateTimeKind.Local).AddTicks(249), new DateTime(2024, 1, 30, 17, 32, 20, 788, DateTimeKind.Local).AddTicks(591) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 30, 17, 32, 20, 788, DateTimeKind.Local).AddTicks(1123), new DateTime(2024, 1, 30, 17, 32, 20, 788, DateTimeKind.Local).AddTicks(1173) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 30, 17, 32, 20, 788, DateTimeKind.Local).AddTicks(1183), new DateTime(2024, 1, 30, 17, 32, 20, 788, DateTimeKind.Local).AddTicks(1185) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 30, 17, 32, 20, 788, DateTimeKind.Local).AddTicks(1190), new DateTime(2024, 1, 30, 17, 32, 20, 788, DateTimeKind.Local).AddTicks(1191) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 30, 17, 32, 20, 788, DateTimeKind.Local).AddTicks(1196), new DateTime(2024, 1, 30, 17, 32, 20, 788, DateTimeKind.Local).AddTicks(1197) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 30, 17, 32, 20, 788, DateTimeKind.Local).AddTicks(1205), new DateTime(2024, 1, 30, 17, 32, 20, 788, DateTimeKind.Local).AddTicks(1207) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Permissions",
                table: "StaffPermissions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 18, 33, 27, 290, DateTimeKind.Local).AddTicks(7310),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 30, 17, 32, 20, 786, DateTimeKind.Local).AddTicks(5216));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 18, 33, 27, 290, DateTimeKind.Local).AddTicks(6912),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 30, 17, 32, 20, 786, DateTimeKind.Local).AddTicks(4113));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 18, 33, 27, 284, DateTimeKind.Local).AddTicks(2348),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 30, 17, 32, 20, 776, DateTimeKind.Local).AddTicks(7589));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 18, 33, 27, 261, DateTimeKind.Local).AddTicks(2161),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 30, 17, 32, 20, 744, DateTimeKind.Local).AddTicks(3791));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 18, 33, 27, 258, DateTimeKind.Local).AddTicks(5001),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 30, 17, 32, 20, 742, DateTimeKind.Local).AddTicks(1077));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "14f9d7b2-5d15-4758-ad5f-6416fe05f35f", "AQAAAAEAACcQAAAAEFOrEqTbcdAhOntdepnOXd9w8tgK1/49udBQx8Vxxgwm/kvZGklseQr+tSPzUYTPBw==", "3410af65-b5b5-4526-95d0-f058e180d21b" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 18, 33, 27, 279, DateTimeKind.Local).AddTicks(2344), new DateTime(2024, 1, 29, 18, 33, 27, 279, DateTimeKind.Local).AddTicks(2534) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 18, 33, 27, 291, DateTimeKind.Local).AddTicks(7031), new DateTime(2024, 1, 29, 18, 33, 27, 291, DateTimeKind.Local).AddTicks(7208) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 18, 33, 27, 291, DateTimeKind.Local).AddTicks(7468), new DateTime(2024, 1, 29, 18, 33, 27, 291, DateTimeKind.Local).AddTicks(7472) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 18, 33, 27, 291, DateTimeKind.Local).AddTicks(7478), new DateTime(2024, 1, 29, 18, 33, 27, 291, DateTimeKind.Local).AddTicks(7479) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 18, 33, 27, 291, DateTimeKind.Local).AddTicks(7483), new DateTime(2024, 1, 29, 18, 33, 27, 291, DateTimeKind.Local).AddTicks(7484) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 18, 33, 27, 291, DateTimeKind.Local).AddTicks(7488), new DateTime(2024, 1, 29, 18, 33, 27, 291, DateTimeKind.Local).AddTicks(7489) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 18, 33, 27, 291, DateTimeKind.Local).AddTicks(7495), new DateTime(2024, 1, 29, 18, 33, 27, 291, DateTimeKind.Local).AddTicks(7496) });
        }
    }
}
