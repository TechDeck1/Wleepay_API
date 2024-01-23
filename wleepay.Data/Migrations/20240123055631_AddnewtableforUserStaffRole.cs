using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class AddnewtableforUserStaffRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 11, 26, 30, 264, DateTimeKind.Local).AddTicks(3374),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 22, 13, 3, 32, 222, DateTimeKind.Local).AddTicks(2512));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 11, 26, 30, 264, DateTimeKind.Local).AddTicks(2838),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 22, 13, 3, 32, 222, DateTimeKind.Local).AddTicks(1859));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 11, 26, 30, 251, DateTimeKind.Local).AddTicks(696),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 22, 13, 3, 32, 215, DateTimeKind.Local).AddTicks(390));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 11, 26, 29, 761, DateTimeKind.Local).AddTicks(5060),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 22, 13, 3, 32, 188, DateTimeKind.Local).AddTicks(7824));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 11, 26, 29, 759, DateTimeKind.Local).AddTicks(1331),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 22, 13, 3, 32, 186, DateTimeKind.Local).AddTicks(6898));

            migrationBuilder.CreateTable(
                name: "UsersStaffRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaffRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersStaffRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersStaffRole_StaffRoles_StaffRoleId",
                        column: x => x.StaffRoleId,
                        principalTable: "StaffRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c440ebdc-6e8d-44ed-a40a-4caf43b0798f", "AQAAAAEAACcQAAAAEFiYpVpiyizYg0KTJdklgj1F92CspBeZ0MlbviHK7rT8HywY9l4pf/A+4zhVw/xP3A==", "fb2a6e1d-03dd-4a5f-ad05-3f5ef03e0908" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 11, 26, 30, 242, DateTimeKind.Local).AddTicks(2844), new DateTime(2024, 1, 23, 11, 26, 30, 242, DateTimeKind.Local).AddTicks(3184) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 11, 26, 30, 266, DateTimeKind.Local).AddTicks(1133), new DateTime(2024, 1, 23, 11, 26, 30, 266, DateTimeKind.Local).AddTicks(1481) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 11, 26, 30, 266, DateTimeKind.Local).AddTicks(2016), new DateTime(2024, 1, 23, 11, 26, 30, 266, DateTimeKind.Local).AddTicks(2021) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 11, 26, 30, 266, DateTimeKind.Local).AddTicks(2031), new DateTime(2024, 1, 23, 11, 26, 30, 266, DateTimeKind.Local).AddTicks(2033) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 11, 26, 30, 266, DateTimeKind.Local).AddTicks(2039), new DateTime(2024, 1, 23, 11, 26, 30, 266, DateTimeKind.Local).AddTicks(2041) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 11, 26, 30, 266, DateTimeKind.Local).AddTicks(2047), new DateTime(2024, 1, 23, 11, 26, 30, 266, DateTimeKind.Local).AddTicks(2048) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 11, 26, 30, 266, DateTimeKind.Local).AddTicks(2064), new DateTime(2024, 1, 23, 11, 26, 30, 266, DateTimeKind.Local).AddTicks(2066) });

            migrationBuilder.CreateIndex(
                name: "IX_UsersStaffRole_StaffRoleId",
                table: "UsersStaffRole",
                column: "StaffRoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersStaffRole");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 22, 13, 3, 32, 222, DateTimeKind.Local).AddTicks(2512),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 11, 26, 30, 264, DateTimeKind.Local).AddTicks(3374));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 22, 13, 3, 32, 222, DateTimeKind.Local).AddTicks(1859),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 11, 26, 30, 264, DateTimeKind.Local).AddTicks(2838));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 22, 13, 3, 32, 215, DateTimeKind.Local).AddTicks(390),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 11, 26, 30, 251, DateTimeKind.Local).AddTicks(696));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 22, 13, 3, 32, 188, DateTimeKind.Local).AddTicks(7824),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 11, 26, 29, 761, DateTimeKind.Local).AddTicks(5060));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 22, 13, 3, 32, 186, DateTimeKind.Local).AddTicks(6898),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 11, 26, 29, 759, DateTimeKind.Local).AddTicks(1331));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eee595c7-11cf-4ea5-8cf1-11efc2c45d92", "AQAAAAEAACcQAAAAEEM8cBO2hvayBgfGU8I2qRlSM3ZRFseWsMufHL7ntEiqk0Pd63EMpc+i4KPps0U5Ow==", "84b8ae3d-e679-48cd-bede-38cb6e0e19c9" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 22, 13, 3, 32, 209, DateTimeKind.Local).AddTicks(2113), new DateTime(2024, 1, 22, 13, 3, 32, 209, DateTimeKind.Local).AddTicks(2349) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 22, 13, 3, 32, 223, DateTimeKind.Local).AddTicks(4250), new DateTime(2024, 1, 22, 13, 3, 32, 223, DateTimeKind.Local).AddTicks(4461) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 22, 13, 3, 32, 223, DateTimeKind.Local).AddTicks(4819), new DateTime(2024, 1, 22, 13, 3, 32, 223, DateTimeKind.Local).AddTicks(4824) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 22, 13, 3, 32, 223, DateTimeKind.Local).AddTicks(4831), new DateTime(2024, 1, 22, 13, 3, 32, 223, DateTimeKind.Local).AddTicks(4832) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 22, 13, 3, 32, 223, DateTimeKind.Local).AddTicks(4836), new DateTime(2024, 1, 22, 13, 3, 32, 223, DateTimeKind.Local).AddTicks(4837) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 22, 13, 3, 32, 223, DateTimeKind.Local).AddTicks(4841), new DateTime(2024, 1, 22, 13, 3, 32, 223, DateTimeKind.Local).AddTicks(4843) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 22, 13, 3, 32, 223, DateTimeKind.Local).AddTicks(4891), new DateTime(2024, 1, 22, 13, 3, 32, 223, DateTimeKind.Local).AddTicks(4892) });
        }
    }
}
