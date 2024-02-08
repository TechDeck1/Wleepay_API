using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class UpdateuserDeatilTableforHireDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StaffPermissionType",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 8, 18, 32, 10, 999, DateTimeKind.Local).AddTicks(575),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 8, 15, 17, 3, 461, DateTimeKind.Local).AddTicks(7395));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 8, 18, 32, 10, 999, DateTimeKind.Local).AddTicks(164),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 8, 15, 17, 3, 461, DateTimeKind.Local).AddTicks(6978));

            migrationBuilder.AlterColumn<DateTime>(
                name: "HireDate",
                table: "UserDetail",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 8, 18, 32, 10, 992, DateTimeKind.Local).AddTicks(5466),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 8, 15, 17, 3, 455, DateTimeKind.Local).AddTicks(13));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 8, 18, 32, 10, 971, DateTimeKind.Local).AddTicks(8763),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 8, 15, 17, 3, 430, DateTimeKind.Local).AddTicks(3024));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 8, 18, 32, 10, 970, DateTimeKind.Local).AddTicks(4794),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 8, 15, 17, 3, 427, DateTimeKind.Local).AddTicks(289));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8ea4d2b5-b2da-41d2-95d6-b63dc4d15606", "AQAAAAEAACcQAAAAENcxDfhc+/CEb+r0zynLELZF0AzEAaOZ0s/28Ilqz+vBPnsdIh0RG9uITBL1FRU4VQ==", "1e442363-5e8b-4c93-b50c-90d308874889" });

            migrationBuilder.UpdateData(
                table: "StaffPermissionType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "System Wallet");

            migrationBuilder.UpdateData(
                table: "StaffPermissionType",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Agent Management");

            migrationBuilder.UpdateData(
                table: "StaffPermissionType",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Subscriber Management");

            migrationBuilder.UpdateData(
                table: "StaffPermissionType",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Customer Service");

            migrationBuilder.UpdateData(
                table: "StaffPermissionType",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Reports");

            migrationBuilder.UpdateData(
                table: "StaffPermissionType",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Compliance & Risk Management");

            migrationBuilder.UpdateData(
                table: "StaffPermissionType",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "System Admin");

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "HireDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 8, 18, 32, 10, 988, DateTimeKind.Local).AddTicks(90), null, new DateTime(2024, 2, 8, 18, 32, 10, 988, DateTimeKind.Local).AddTicks(299) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 8, 18, 32, 10, 999, DateTimeKind.Local).AddTicks(9966), new DateTime(2024, 2, 8, 18, 32, 11, 0, DateTimeKind.Local).AddTicks(153) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 8, 18, 32, 11, 0, DateTimeKind.Local).AddTicks(425), new DateTime(2024, 2, 8, 18, 32, 11, 0, DateTimeKind.Local).AddTicks(428) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 8, 18, 32, 11, 0, DateTimeKind.Local).AddTicks(436), new DateTime(2024, 2, 8, 18, 32, 11, 0, DateTimeKind.Local).AddTicks(437) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 8, 18, 32, 11, 0, DateTimeKind.Local).AddTicks(441), new DateTime(2024, 2, 8, 18, 32, 11, 0, DateTimeKind.Local).AddTicks(442) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 8, 18, 32, 11, 0, DateTimeKind.Local).AddTicks(446), new DateTime(2024, 2, 8, 18, 32, 11, 0, DateTimeKind.Local).AddTicks(447) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 8, 18, 32, 11, 0, DateTimeKind.Local).AddTicks(497), new DateTime(2024, 2, 8, 18, 32, 11, 0, DateTimeKind.Local).AddTicks(498) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 8, 15, 17, 3, 461, DateTimeKind.Local).AddTicks(7395),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 8, 18, 32, 10, 999, DateTimeKind.Local).AddTicks(575));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 8, 15, 17, 3, 461, DateTimeKind.Local).AddTicks(6978),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 8, 18, 32, 10, 999, DateTimeKind.Local).AddTicks(164));

            migrationBuilder.AlterColumn<DateTime>(
                name: "HireDate",
                table: "UserDetail",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 8, 15, 17, 3, 455, DateTimeKind.Local).AddTicks(13),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 8, 18, 32, 10, 992, DateTimeKind.Local).AddTicks(5466));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 8, 15, 17, 3, 430, DateTimeKind.Local).AddTicks(3024),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 8, 18, 32, 10, 971, DateTimeKind.Local).AddTicks(8763));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 8, 15, 17, 3, 427, DateTimeKind.Local).AddTicks(289),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 8, 18, 32, 10, 970, DateTimeKind.Local).AddTicks(4794));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "48602f9c-c378-4379-a3b1-65ea080921e8", "AQAAAAEAACcQAAAAEOqOPFo9jKrPviNPwGRbBJGg4wxlbai013imtF747WcxouZIyxT/KXSNVDBgvdM/Mg==", "2d6bf4fe-a2b2-4e77-9368-04db4eb34db8" });

            migrationBuilder.UpdateData(
                table: "StaffPermissionType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Dashboard");

            migrationBuilder.UpdateData(
                table: "StaffPermissionType",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "System Wallet");

            migrationBuilder.UpdateData(
                table: "StaffPermissionType",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Agent Management");

            migrationBuilder.UpdateData(
                table: "StaffPermissionType",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Subscriber Management");

            migrationBuilder.UpdateData(
                table: "StaffPermissionType",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Customer Service");

            migrationBuilder.UpdateData(
                table: "StaffPermissionType",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Reports");

            migrationBuilder.UpdateData(
                table: "StaffPermissionType",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Compliance & Risk Management");

            migrationBuilder.InsertData(
                table: "StaffPermissionType",
                columns: new[] { "Id", "Name" },
                values: new object[] { 8, "System Admin" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "HireDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 8, 15, 17, 3, 449, DateTimeKind.Local).AddTicks(7245), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 8, 15, 17, 3, 449, DateTimeKind.Local).AddTicks(7463) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 8, 15, 17, 3, 462, DateTimeKind.Local).AddTicks(7472), new DateTime(2024, 2, 8, 15, 17, 3, 462, DateTimeKind.Local).AddTicks(7672) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 8, 15, 17, 3, 462, DateTimeKind.Local).AddTicks(7959), new DateTime(2024, 2, 8, 15, 17, 3, 462, DateTimeKind.Local).AddTicks(7963) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 8, 15, 17, 3, 462, DateTimeKind.Local).AddTicks(7970), new DateTime(2024, 2, 8, 15, 17, 3, 462, DateTimeKind.Local).AddTicks(7971) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 8, 15, 17, 3, 462, DateTimeKind.Local).AddTicks(7976), new DateTime(2024, 2, 8, 15, 17, 3, 462, DateTimeKind.Local).AddTicks(7977) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 8, 15, 17, 3, 462, DateTimeKind.Local).AddTicks(7981), new DateTime(2024, 2, 8, 15, 17, 3, 462, DateTimeKind.Local).AddTicks(7982) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 8, 15, 17, 3, 462, DateTimeKind.Local).AddTicks(7987), new DateTime(2024, 2, 8, 15, 17, 3, 462, DateTimeKind.Local).AddTicks(7988) });
        }
    }
}
