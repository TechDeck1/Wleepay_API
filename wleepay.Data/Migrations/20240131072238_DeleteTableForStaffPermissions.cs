using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class DeleteTableForStaffPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StaffPermissions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 31, 12, 52, 37, 529, DateTimeKind.Local).AddTicks(9119),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 31, 11, 40, 14, 291, DateTimeKind.Local).AddTicks(6355));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 31, 12, 52, 37, 529, DateTimeKind.Local).AddTicks(8720),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 31, 11, 40, 14, 291, DateTimeKind.Local).AddTicks(5929));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 31, 12, 52, 37, 524, DateTimeKind.Local).AddTicks(636),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 31, 11, 40, 14, 285, DateTimeKind.Local).AddTicks(6555));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 31, 12, 52, 37, 504, DateTimeKind.Local).AddTicks(895),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 31, 11, 40, 14, 262, DateTimeKind.Local).AddTicks(1881));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 31, 12, 52, 37, 502, DateTimeKind.Local).AddTicks(6110),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 31, 11, 40, 14, 259, DateTimeKind.Local).AddTicks(7364));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2e132c84-81b9-4295-97c1-c336c1f6a8f7", "AQAAAAEAACcQAAAAEDIeUPvYf2lNNdRpUwYx/tJ1U5wmi9pJP6Nx74Yl4RDG7khJu1s39Ur2EPTONYHfSg==", "e7775734-8dad-4b02-b354-f76b4c2c965f" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 31, 12, 52, 37, 519, DateTimeKind.Local).AddTicks(3475), new DateTime(2024, 1, 31, 12, 52, 37, 519, DateTimeKind.Local).AddTicks(3687) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 31, 12, 52, 37, 530, DateTimeKind.Local).AddTicks(8423), new DateTime(2024, 1, 31, 12, 52, 37, 530, DateTimeKind.Local).AddTicks(8599) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 31, 12, 52, 37, 530, DateTimeKind.Local).AddTicks(8853), new DateTime(2024, 1, 31, 12, 52, 37, 530, DateTimeKind.Local).AddTicks(8856) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 31, 12, 52, 37, 530, DateTimeKind.Local).AddTicks(8905), new DateTime(2024, 1, 31, 12, 52, 37, 530, DateTimeKind.Local).AddTicks(8907) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 31, 12, 52, 37, 530, DateTimeKind.Local).AddTicks(8911), new DateTime(2024, 1, 31, 12, 52, 37, 530, DateTimeKind.Local).AddTicks(8912) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 31, 12, 52, 37, 530, DateTimeKind.Local).AddTicks(8915), new DateTime(2024, 1, 31, 12, 52, 37, 530, DateTimeKind.Local).AddTicks(8916) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 31, 12, 52, 37, 530, DateTimeKind.Local).AddTicks(8922), new DateTime(2024, 1, 31, 12, 52, 37, 530, DateTimeKind.Local).AddTicks(8923) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 31, 11, 40, 14, 291, DateTimeKind.Local).AddTicks(6355),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 31, 12, 52, 37, 529, DateTimeKind.Local).AddTicks(9119));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 31, 11, 40, 14, 291, DateTimeKind.Local).AddTicks(5929),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 31, 12, 52, 37, 529, DateTimeKind.Local).AddTicks(8720));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 31, 11, 40, 14, 285, DateTimeKind.Local).AddTicks(6555),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 31, 12, 52, 37, 524, DateTimeKind.Local).AddTicks(636));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 31, 11, 40, 14, 262, DateTimeKind.Local).AddTicks(1881),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 31, 12, 52, 37, 504, DateTimeKind.Local).AddTicks(895));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 31, 11, 40, 14, 259, DateTimeKind.Local).AddTicks(7364),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 31, 12, 52, 37, 502, DateTimeKind.Local).AddTicks(6110));

            migrationBuilder.CreateTable(
                name: "StaffPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    Permissions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaffPermissions_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StaffPermissions_StaffPermissionType_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "StaffPermissionType",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_StaffPermissions_ApplicationUserId",
                table: "StaffPermissions",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffPermissions_PermissionId",
                table: "StaffPermissions",
                column: "PermissionId");
        }
    }
}
