using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class UpdatetableforstaffPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffPermissions_AspNetUsers_ApplicationUserId",
                table: "StaffPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffPermissions_StaffPermissionType_PermissionTypeId",
                table: "StaffPermissions");

            migrationBuilder.DropIndex(
                name: "IX_StaffPermissions_PermissionTypeId",
                table: "StaffPermissions");

            migrationBuilder.DropColumn(
                name: "PermissionTypeId",
                table: "StaffPermissions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StaffPermissions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 13, 50, 20, 973, DateTimeKind.Local).AddTicks(1680),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 13, 43, 46, 543, DateTimeKind.Local).AddTicks(6806));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 13, 50, 20, 973, DateTimeKind.Local).AddTicks(1300),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 13, 43, 46, 543, DateTimeKind.Local).AddTicks(6223));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 13, 50, 20, 966, DateTimeKind.Local).AddTicks(2109),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 13, 43, 46, 536, DateTimeKind.Local).AddTicks(4549));

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "StaffPermissions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "StaffPermissions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "StaffPermissions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 13, 50, 20, 930, DateTimeKind.Local).AddTicks(4378),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 13, 43, 46, 508, DateTimeKind.Local).AddTicks(6907));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 13, 50, 20, 928, DateTimeKind.Local).AddTicks(1735),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 13, 43, 46, 506, DateTimeKind.Local).AddTicks(9663));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a1432786-85cc-4261-8d61-05a1970b22ed", "AQAAAAEAACcQAAAAEPrZ3f7Q+Yi4XnvtgvSSUDdfkOp9iCEgZlz88yXfJsm+YxYaEax1qTiNcgawkhR/MQ==", "4b286ad3-cbc5-4c0a-b5f3-48e48a143a10" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 13, 50, 20, 960, DateTimeKind.Local).AddTicks(6988), new DateTime(2024, 1, 23, 13, 50, 20, 960, DateTimeKind.Local).AddTicks(7178) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 13, 50, 20, 974, DateTimeKind.Local).AddTicks(2322), new DateTime(2024, 1, 23, 13, 50, 20, 974, DateTimeKind.Local).AddTicks(2504) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 13, 50, 20, 974, DateTimeKind.Local).AddTicks(2760), new DateTime(2024, 1, 23, 13, 50, 20, 974, DateTimeKind.Local).AddTicks(2763) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 13, 50, 20, 974, DateTimeKind.Local).AddTicks(2768), new DateTime(2024, 1, 23, 13, 50, 20, 974, DateTimeKind.Local).AddTicks(2769) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 13, 50, 20, 974, DateTimeKind.Local).AddTicks(2773), new DateTime(2024, 1, 23, 13, 50, 20, 974, DateTimeKind.Local).AddTicks(2774) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 13, 50, 20, 974, DateTimeKind.Local).AddTicks(2777), new DateTime(2024, 1, 23, 13, 50, 20, 974, DateTimeKind.Local).AddTicks(2778) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 13, 50, 20, 974, DateTimeKind.Local).AddTicks(2783), new DateTime(2024, 1, 23, 13, 50, 20, 974, DateTimeKind.Local).AddTicks(2784) });

            migrationBuilder.CreateIndex(
                name: "IX_StaffPermissions_PermissionId",
                table: "StaffPermissions",
                column: "PermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffPermissions_AspNetUsers_ApplicationUserId",
                table: "StaffPermissions",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffPermissions_StaffPermissionType_PermissionId",
                table: "StaffPermissions",
                column: "PermissionId",
                principalTable: "StaffPermissionType",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffPermissions_AspNetUsers_ApplicationUserId",
                table: "StaffPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffPermissions_StaffPermissionType_PermissionId",
                table: "StaffPermissions");

            migrationBuilder.DropIndex(
                name: "IX_StaffPermissions_PermissionId",
                table: "StaffPermissions");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "StaffPermissions");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "StaffPermissions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 13, 43, 46, 543, DateTimeKind.Local).AddTicks(6806),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 13, 50, 20, 973, DateTimeKind.Local).AddTicks(1680));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 13, 43, 46, 543, DateTimeKind.Local).AddTicks(6223),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 13, 50, 20, 973, DateTimeKind.Local).AddTicks(1300));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 13, 43, 46, 536, DateTimeKind.Local).AddTicks(4549),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 13, 50, 20, 966, DateTimeKind.Local).AddTicks(2109));

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "StaffPermissions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "PermissionTypeId",
                table: "StaffPermissions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "StaffPermissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 13, 43, 46, 508, DateTimeKind.Local).AddTicks(6907),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 13, 50, 20, 930, DateTimeKind.Local).AddTicks(4378));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 13, 43, 46, 506, DateTimeKind.Local).AddTicks(9663),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 13, 50, 20, 928, DateTimeKind.Local).AddTicks(1735));

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
                name: "IX_StaffPermissions_PermissionTypeId",
                table: "StaffPermissions",
                column: "PermissionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffPermissions_AspNetUsers_ApplicationUserId",
                table: "StaffPermissions",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffPermissions_StaffPermissionType_PermissionTypeId",
                table: "StaffPermissions",
                column: "PermissionTypeId",
                principalTable: "StaffPermissionType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
