using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class updateDocumentTypeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 21, 18, 1, 0, 660, DateTimeKind.Local).AddTicks(6462),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 9, 12, 24, 43, 863, DateTimeKind.Local).AddTicks(7757));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 21, 18, 1, 0, 660, DateTimeKind.Local).AddTicks(5984),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 9, 12, 24, 43, 863, DateTimeKind.Local).AddTicks(7453));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 21, 18, 1, 0, 650, DateTimeKind.Local).AddTicks(2882),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 9, 12, 24, 43, 856, DateTimeKind.Local).AddTicks(8456));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 21, 18, 1, 0, 616, DateTimeKind.Local).AddTicks(9072),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 9, 12, 24, 43, 835, DateTimeKind.Local).AddTicks(6300));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 21, 18, 1, 0, 614, DateTimeKind.Local).AddTicks(3602),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 9, 12, 24, 43, 834, DateTimeKind.Local).AddTicks(752));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bd3f37e7-a20f-4cc4-a8d9-9441e20a3335", "AQAAAAEAACcQAAAAELu3Y+bWAd0nwyJzj1oY4TM0vl/ybMHrVe9sMC99dcvKOfA3uO5rNoyOrgaBwrBzCA==", "24ec25a6-5f23-4957-8352-59c7a6470a2b" });

            migrationBuilder.InsertData(
                table: "DocumentType",
                columns: new[] { "Id", "Name" },
                values: new object[] { 6, "Business License" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 21, 18, 1, 0, 643, DateTimeKind.Local).AddTicks(1293), new DateTime(2024, 10, 21, 18, 1, 0, 643, DateTimeKind.Local).AddTicks(1668) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 21, 18, 1, 0, 662, DateTimeKind.Local).AddTicks(1665), new DateTime(2024, 10, 21, 18, 1, 0, 662, DateTimeKind.Local).AddTicks(2139) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 21, 18, 1, 0, 662, DateTimeKind.Local).AddTicks(2719), new DateTime(2024, 10, 21, 18, 1, 0, 662, DateTimeKind.Local).AddTicks(2725) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 21, 18, 1, 0, 662, DateTimeKind.Local).AddTicks(2733), new DateTime(2024, 10, 21, 18, 1, 0, 662, DateTimeKind.Local).AddTicks(2734) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 21, 18, 1, 0, 662, DateTimeKind.Local).AddTicks(2739), new DateTime(2024, 10, 21, 18, 1, 0, 662, DateTimeKind.Local).AddTicks(2740) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 21, 18, 1, 0, 662, DateTimeKind.Local).AddTicks(2745), new DateTime(2024, 10, 21, 18, 1, 0, 662, DateTimeKind.Local).AddTicks(2746) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 21, 18, 1, 0, 662, DateTimeKind.Local).AddTicks(2755), new DateTime(2024, 10, 21, 18, 1, 0, 662, DateTimeKind.Local).AddTicks(2756) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 21, 18, 1, 0, 662, DateTimeKind.Local).AddTicks(2760), new DateTime(2024, 10, 21, 18, 1, 0, 662, DateTimeKind.Local).AddTicks(2762) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 21, 18, 1, 0, 662, DateTimeKind.Local).AddTicks(2768), new DateTime(2024, 10, 21, 18, 1, 0, 662, DateTimeKind.Local).AddTicks(2769) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DocumentType",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 9, 12, 24, 43, 863, DateTimeKind.Local).AddTicks(7757),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 21, 18, 1, 0, 660, DateTimeKind.Local).AddTicks(6462));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 9, 12, 24, 43, 863, DateTimeKind.Local).AddTicks(7453),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 21, 18, 1, 0, 660, DateTimeKind.Local).AddTicks(5984));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 9, 12, 24, 43, 856, DateTimeKind.Local).AddTicks(8456),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 21, 18, 1, 0, 650, DateTimeKind.Local).AddTicks(2882));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 9, 12, 24, 43, 835, DateTimeKind.Local).AddTicks(6300),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 21, 18, 1, 0, 616, DateTimeKind.Local).AddTicks(9072));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 9, 12, 24, 43, 834, DateTimeKind.Local).AddTicks(752),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 21, 18, 1, 0, 614, DateTimeKind.Local).AddTicks(3602));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "557d8b1b-47ae-455a-89dd-b7684b262d6d", "AQAAAAEAACcQAAAAEH41eKo3IptwlFUWMQq3A7mWds996XyU05pErDe51MvSgQFV5kf4xK6STeJZlLkv2w==", "704d6b4d-3bb0-41fd-b9c8-3af1484b1afd" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 9, 12, 24, 43, 851, DateTimeKind.Local).AddTicks(4777), new DateTime(2024, 10, 9, 12, 24, 43, 851, DateTimeKind.Local).AddTicks(5078) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 9, 12, 24, 43, 864, DateTimeKind.Local).AddTicks(7675), new DateTime(2024, 10, 9, 12, 24, 43, 864, DateTimeKind.Local).AddTicks(7933) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 9, 12, 24, 43, 864, DateTimeKind.Local).AddTicks(8283), new DateTime(2024, 10, 9, 12, 24, 43, 864, DateTimeKind.Local).AddTicks(8286) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 9, 12, 24, 43, 864, DateTimeKind.Local).AddTicks(8293), new DateTime(2024, 10, 9, 12, 24, 43, 864, DateTimeKind.Local).AddTicks(8294) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 9, 12, 24, 43, 864, DateTimeKind.Local).AddTicks(8298), new DateTime(2024, 10, 9, 12, 24, 43, 864, DateTimeKind.Local).AddTicks(8299) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 9, 12, 24, 43, 864, DateTimeKind.Local).AddTicks(8303), new DateTime(2024, 10, 9, 12, 24, 43, 864, DateTimeKind.Local).AddTicks(8304) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 9, 12, 24, 43, 864, DateTimeKind.Local).AddTicks(8310), new DateTime(2024, 10, 9, 12, 24, 43, 864, DateTimeKind.Local).AddTicks(8311) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 9, 12, 24, 43, 864, DateTimeKind.Local).AddTicks(8315), new DateTime(2024, 10, 9, 12, 24, 43, 864, DateTimeKind.Local).AddTicks(8316) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 9, 12, 24, 43, 864, DateTimeKind.Local).AddTicks(8319), new DateTime(2024, 10, 9, 12, 24, 43, 864, DateTimeKind.Local).AddTicks(8320) });
        }
    }
}
