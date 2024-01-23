using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class UpdatetableforAspNetRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 17, 7, 22, 387, DateTimeKind.Local).AddTicks(2484),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 13, 50, 20, 973, DateTimeKind.Local).AddTicks(1680));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 17, 7, 22, 387, DateTimeKind.Local).AddTicks(2065),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 13, 50, 20, 973, DateTimeKind.Local).AddTicks(1300));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 17, 7, 22, 375, DateTimeKind.Local).AddTicks(1955),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 13, 50, 20, 966, DateTimeKind.Local).AddTicks(2109));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 17, 7, 22, 342, DateTimeKind.Local).AddTicks(1576),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 13, 50, 20, 930, DateTimeKind.Local).AddTicks(4378));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 17, 7, 22, 339, DateTimeKind.Local).AddTicks(9746),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 13, 50, 20, 928, DateTimeKind.Local).AddTicks(1735));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "F58A3546-E959-4B1D-A40C-518EAF9D9788",
                column: "Name",
                value: "Staff");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fbf8256c-0824-432b-9543-95b61d993c3c", "4dcf6d38-0024-4491-8d17-3f968d22677a", "SystemAdmin", "SYSTEMADMIN" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6df86b33-47c9-4927-bc58-2b734d0ee1e9", "AQAAAAEAACcQAAAAEAuDIsVQmWIYtUioytYPjRVcVA4hb+QvJ6EW8j9REhbZ7qk2mTAXkIi/npmWB3eQTg==", "cbc86539-e915-48d5-aa24-ed82b339fdaf" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 17, 7, 22, 368, DateTimeKind.Local).AddTicks(1404), new DateTime(2024, 1, 23, 17, 7, 22, 368, DateTimeKind.Local).AddTicks(1599) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 17, 7, 22, 388, DateTimeKind.Local).AddTicks(2150), new DateTime(2024, 1, 23, 17, 7, 22, 388, DateTimeKind.Local).AddTicks(2331) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 17, 7, 22, 388, DateTimeKind.Local).AddTicks(2871), new DateTime(2024, 1, 23, 17, 7, 22, 388, DateTimeKind.Local).AddTicks(2874) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 17, 7, 22, 388, DateTimeKind.Local).AddTicks(2881), new DateTime(2024, 1, 23, 17, 7, 22, 388, DateTimeKind.Local).AddTicks(2883) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 17, 7, 22, 388, DateTimeKind.Local).AddTicks(2889), new DateTime(2024, 1, 23, 17, 7, 22, 388, DateTimeKind.Local).AddTicks(2891) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 17, 7, 22, 388, DateTimeKind.Local).AddTicks(2905), new DateTime(2024, 1, 23, 17, 7, 22, 388, DateTimeKind.Local).AddTicks(2906) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 23, 17, 7, 22, 388, DateTimeKind.Local).AddTicks(2917), new DateTime(2024, 1, 23, 17, 7, 22, 388, DateTimeKind.Local).AddTicks(2918) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fbf8256c-0824-432b-9543-95b61d993c3c");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 13, 50, 20, 973, DateTimeKind.Local).AddTicks(1680),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 17, 7, 22, 387, DateTimeKind.Local).AddTicks(2484));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 13, 50, 20, 973, DateTimeKind.Local).AddTicks(1300),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 17, 7, 22, 387, DateTimeKind.Local).AddTicks(2065));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 13, 50, 20, 966, DateTimeKind.Local).AddTicks(2109),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 17, 7, 22, 375, DateTimeKind.Local).AddTicks(1955));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 13, 50, 20, 930, DateTimeKind.Local).AddTicks(4378),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 17, 7, 22, 342, DateTimeKind.Local).AddTicks(1576));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 23, 13, 50, 20, 928, DateTimeKind.Local).AddTicks(1735),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 23, 17, 7, 22, 339, DateTimeKind.Local).AddTicks(9746));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "F58A3546-E959-4B1D-A40C-518EAF9D9788",
                column: "Name",
                value: "staff");

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
        }
    }
}
