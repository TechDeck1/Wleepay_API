using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class AddNewFieldForPermissionsInUserDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 31, 11, 40, 14, 291, DateTimeKind.Local).AddTicks(6355),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 30, 17, 32, 20, 786, DateTimeKind.Local).AddTicks(5216));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 31, 11, 40, 14, 291, DateTimeKind.Local).AddTicks(5929),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 30, 17, 32, 20, 786, DateTimeKind.Local).AddTicks(4113));

            migrationBuilder.AddColumn<string>(
                name: "Permissions",
                table: "UserDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 31, 11, 40, 14, 285, DateTimeKind.Local).AddTicks(6555),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 30, 17, 32, 20, 776, DateTimeKind.Local).AddTicks(7589));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 31, 11, 40, 14, 262, DateTimeKind.Local).AddTicks(1881),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 30, 17, 32, 20, 744, DateTimeKind.Local).AddTicks(3791));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 31, 11, 40, 14, 259, DateTimeKind.Local).AddTicks(7364),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 30, 17, 32, 20, 742, DateTimeKind.Local).AddTicks(1077));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b02a9203-6105-4aaf-a69f-c74f870465fa", "AQAAAAEAACcQAAAAEAW+pvO6/NpO+YkwO0I2f4x3ktJ3wXLWySQz2V6ZsonMF7QP/jcU3yvuX0iqOKY6Rg==", "cb7ea486-f03a-42bf-81cc-b59993375568" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 31, 11, 40, 14, 280, DateTimeKind.Local).AddTicks(8882), new DateTime(2024, 1, 31, 11, 40, 14, 280, DateTimeKind.Local).AddTicks(9076) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 31, 11, 40, 14, 292, DateTimeKind.Local).AddTicks(5205), new DateTime(2024, 1, 31, 11, 40, 14, 292, DateTimeKind.Local).AddTicks(5377) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 31, 11, 40, 14, 292, DateTimeKind.Local).AddTicks(5672), new DateTime(2024, 1, 31, 11, 40, 14, 292, DateTimeKind.Local).AddTicks(5675) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 31, 11, 40, 14, 292, DateTimeKind.Local).AddTicks(5681), new DateTime(2024, 1, 31, 11, 40, 14, 292, DateTimeKind.Local).AddTicks(5681) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 31, 11, 40, 14, 292, DateTimeKind.Local).AddTicks(5685), new DateTime(2024, 1, 31, 11, 40, 14, 292, DateTimeKind.Local).AddTicks(5686) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 31, 11, 40, 14, 292, DateTimeKind.Local).AddTicks(5689), new DateTime(2024, 1, 31, 11, 40, 14, 292, DateTimeKind.Local).AddTicks(5690) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 31, 11, 40, 14, 292, DateTimeKind.Local).AddTicks(5694), new DateTime(2024, 1, 31, 11, 40, 14, 292, DateTimeKind.Local).AddTicks(5695) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Permissions",
                table: "UserDetail");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 30, 17, 32, 20, 786, DateTimeKind.Local).AddTicks(5216),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 31, 11, 40, 14, 291, DateTimeKind.Local).AddTicks(6355));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 30, 17, 32, 20, 786, DateTimeKind.Local).AddTicks(4113),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 31, 11, 40, 14, 291, DateTimeKind.Local).AddTicks(5929));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 30, 17, 32, 20, 776, DateTimeKind.Local).AddTicks(7589),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 31, 11, 40, 14, 285, DateTimeKind.Local).AddTicks(6555));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 30, 17, 32, 20, 744, DateTimeKind.Local).AddTicks(3791),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 31, 11, 40, 14, 262, DateTimeKind.Local).AddTicks(1881));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 30, 17, 32, 20, 742, DateTimeKind.Local).AddTicks(1077),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 31, 11, 40, 14, 259, DateTimeKind.Local).AddTicks(7364));

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
    }
}
