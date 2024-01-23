using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class AddnewtableforstaffPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 13, 43, 46, 543, DateTimeKind.Local).AddTicks(6806),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 13, 10, 53, 569, DateTimeKind.Local).AddTicks(307));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 13, 43, 46, 543, DateTimeKind.Local).AddTicks(6223),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 13, 10, 53, 568, DateTimeKind.Local).AddTicks(9869));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 13, 43, 46, 536, DateTimeKind.Local).AddTicks(4549),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 13, 10, 53, 561, DateTimeKind.Local).AddTicks(5275));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 13, 43, 46, 508, DateTimeKind.Local).AddTicks(6907),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 13, 10, 53, 539, DateTimeKind.Local).AddTicks(1827));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 13, 43, 46, 506, DateTimeKind.Local).AddTicks(9663),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 13, 10, 53, 537, DateTimeKind.Local).AddTicks(7198));

            migrationBuilder.CreateTable(
                name: "StaffPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    PermissionTypeId = table.Column<int>(type: "int", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaffPermissions_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StaffPermissions_StaffPermissionType_PermissionTypeId",
                        column: x => x.PermissionTypeId,
                        principalTable: "StaffPermissionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "906603c9-d321-4cca-a0bc-aa24537c2bd7", "AQAAAAEAACcQAAAAEONc1HKRIEzIw7kQreaO5BRgcPM5Rgd5ysz5RwSbJOgeKe261FKHlF2/+FVhuztXdA==", "9c1f6170-b8be-45b9-b917-61374e57853b" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 13, 43, 46, 530, DateTimeKind.Local).AddTicks(4394), new DateTime(2024, 1, 23, 13, 43, 46, 530, DateTimeKind.Local).AddTicks(4592) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 13, 43, 46, 544, DateTimeKind.Local).AddTicks(7138), new DateTime(2024, 1, 23, 13, 43, 46, 544, DateTimeKind.Local).AddTicks(7319) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 13, 43, 46, 544, DateTimeKind.Local).AddTicks(7814), new DateTime(2024, 1, 23, 13, 43, 46, 544, DateTimeKind.Local).AddTicks(7818) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 13, 43, 46, 544, DateTimeKind.Local).AddTicks(7825), new DateTime(2024, 1, 23, 13, 43, 46, 544, DateTimeKind.Local).AddTicks(7826) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 13, 43, 46, 544, DateTimeKind.Local).AddTicks(7830), new DateTime(2024, 1, 23, 13, 43, 46, 544, DateTimeKind.Local).AddTicks(7831) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 13, 43, 46, 544, DateTimeKind.Local).AddTicks(7835), new DateTime(2024, 1, 23, 13, 43, 46, 544, DateTimeKind.Local).AddTicks(7836) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 13, 43, 46, 544, DateTimeKind.Local).AddTicks(7842), new DateTime(2024, 1, 23, 13, 43, 46, 544, DateTimeKind.Local).AddTicks(7843) });

            migrationBuilder.CreateIndex(
                name: "IX_StaffPermissions_ApplicationUserId",
                table: "StaffPermissions",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffPermissions_PermissionTypeId",
                table: "StaffPermissions",
                column: "PermissionTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StaffPermissions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 13, 10, 53, 569, DateTimeKind.Local).AddTicks(307),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 13, 43, 46, 543, DateTimeKind.Local).AddTicks(6806));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 13, 10, 53, 568, DateTimeKind.Local).AddTicks(9869),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 13, 43, 46, 543, DateTimeKind.Local).AddTicks(6223));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 13, 10, 53, 561, DateTimeKind.Local).AddTicks(5275),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 13, 43, 46, 536, DateTimeKind.Local).AddTicks(4549));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 13, 10, 53, 539, DateTimeKind.Local).AddTicks(1827),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 13, 43, 46, 508, DateTimeKind.Local).AddTicks(6907));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 13, 10, 53, 537, DateTimeKind.Local).AddTicks(7198),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 13, 43, 46, 506, DateTimeKind.Local).AddTicks(9663));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "06eb5d17-5494-46a4-b7fc-fab1b1be9904", "AQAAAAEAACcQAAAAEKXfBDdskbCu8z6cb4KlxcCUR2uB6GjDpNMR+FyqVHd33x59xkfaIL82/6nQiX0Xcg==", "9c30438a-8655-4101-8dab-0d346bcc12fb" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 13, 10, 53, 556, DateTimeKind.Local).AddTicks(9018), new DateTime(2024, 1, 23, 13, 10, 53, 556, DateTimeKind.Local).AddTicks(9209) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 13, 10, 53, 569, DateTimeKind.Local).AddTicks(9176), new DateTime(2024, 1, 23, 13, 10, 53, 569, DateTimeKind.Local).AddTicks(9354) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 13, 10, 53, 569, DateTimeKind.Local).AddTicks(9616), new DateTime(2024, 1, 23, 13, 10, 53, 569, DateTimeKind.Local).AddTicks(9619) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 13, 10, 53, 569, DateTimeKind.Local).AddTicks(9698), new DateTime(2024, 1, 23, 13, 10, 53, 569, DateTimeKind.Local).AddTicks(9699) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 13, 10, 53, 569, DateTimeKind.Local).AddTicks(9704), new DateTime(2024, 1, 23, 13, 10, 53, 569, DateTimeKind.Local).AddTicks(9704) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 13, 10, 53, 569, DateTimeKind.Local).AddTicks(9708), new DateTime(2024, 1, 23, 13, 10, 53, 569, DateTimeKind.Local).AddTicks(9709) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 13, 10, 53, 569, DateTimeKind.Local).AddTicks(9715), new DateTime(2024, 1, 23, 13, 10, 53, 569, DateTimeKind.Local).AddTicks(9716) });
        }
    }
}
