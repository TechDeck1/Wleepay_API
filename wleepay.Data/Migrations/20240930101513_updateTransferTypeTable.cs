using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class updateTransferTypeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 30, 15, 45, 12, 462, DateTimeKind.Local).AddTicks(7947),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 20, 16, 35, 37, 960, DateTimeKind.Local).AddTicks(7853));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 30, 15, 45, 12, 462, DateTimeKind.Local).AddTicks(7410),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 20, 16, 35, 37, 960, DateTimeKind.Local).AddTicks(7393));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 30, 15, 45, 12, 450, DateTimeKind.Local).AddTicks(6770),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 20, 16, 35, 37, 950, DateTimeKind.Local).AddTicks(6384));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 30, 15, 45, 12, 416, DateTimeKind.Local).AddTicks(370),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 20, 16, 35, 37, 910, DateTimeKind.Local).AddTicks(6746));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 30, 15, 45, 12, 413, DateTimeKind.Local).AddTicks(5357),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 20, 16, 35, 37, 908, DateTimeKind.Local).AddTicks(2291));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dd799b45-1c15-4323-9fa0-ff346a8f8b8b", "AQAAAAEAACcQAAAAEFBUUBKLQCLu26HxuKSkWYnRQhfATrmLZrwbCvtlJEW3rLDRxXetjqy6wVXwspG7KQ==", "d7bc6374-d183-406c-84de-c8f01ce3d63c" });

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Cash Out");

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Top Up");

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 30, 15, 45, 12, 442, DateTimeKind.Local).AddTicks(8616), new DateTime(2024, 9, 30, 15, 45, 12, 442, DateTimeKind.Local).AddTicks(9266) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 30, 15, 45, 12, 464, DateTimeKind.Local).AddTicks(3797), new DateTime(2024, 9, 30, 15, 45, 12, 464, DateTimeKind.Local).AddTicks(4224) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 30, 15, 45, 12, 464, DateTimeKind.Local).AddTicks(4750), new DateTime(2024, 9, 30, 15, 45, 12, 464, DateTimeKind.Local).AddTicks(4756) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 30, 15, 45, 12, 464, DateTimeKind.Local).AddTicks(4765), new DateTime(2024, 9, 30, 15, 45, 12, 464, DateTimeKind.Local).AddTicks(4766) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 30, 15, 45, 12, 464, DateTimeKind.Local).AddTicks(4771), new DateTime(2024, 9, 30, 15, 45, 12, 464, DateTimeKind.Local).AddTicks(4773) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 30, 15, 45, 12, 464, DateTimeKind.Local).AddTicks(4778), new DateTime(2024, 9, 30, 15, 45, 12, 464, DateTimeKind.Local).AddTicks(4779) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 30, 15, 45, 12, 464, DateTimeKind.Local).AddTicks(4786), new DateTime(2024, 9, 30, 15, 45, 12, 464, DateTimeKind.Local).AddTicks(4787) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 30, 15, 45, 12, 464, DateTimeKind.Local).AddTicks(4792), new DateTime(2024, 9, 30, 15, 45, 12, 464, DateTimeKind.Local).AddTicks(4793) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 30, 15, 45, 12, 464, DateTimeKind.Local).AddTicks(4798), new DateTime(2024, 9, 30, 15, 45, 12, 464, DateTimeKind.Local).AddTicks(4799) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 20, 16, 35, 37, 960, DateTimeKind.Local).AddTicks(7853),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 30, 15, 45, 12, 462, DateTimeKind.Local).AddTicks(7947));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 20, 16, 35, 37, 960, DateTimeKind.Local).AddTicks(7393),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 30, 15, 45, 12, 462, DateTimeKind.Local).AddTicks(7410));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 20, 16, 35, 37, 950, DateTimeKind.Local).AddTicks(6384),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 30, 15, 45, 12, 450, DateTimeKind.Local).AddTicks(6770));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 20, 16, 35, 37, 910, DateTimeKind.Local).AddTicks(6746),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 30, 15, 45, 12, 416, DateTimeKind.Local).AddTicks(370));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 20, 16, 35, 37, 908, DateTimeKind.Local).AddTicks(2291),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 30, 15, 45, 12, 413, DateTimeKind.Local).AddTicks(5357));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "83966b0f-6826-4472-aaa8-44dbd6313443", "AQAAAAEAACcQAAAAEKuvF+v/euw51OJxhh7Lyzjco2KmKXBq0aV1VllxlFV6pEnpmAkH6PeyINKjxYkcnA==", "92268efc-2743-4897-86f6-3fcda972e95f" });

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Subscriber Cash Out");

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Subscriber Top Up");

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 20, 16, 35, 37, 943, DateTimeKind.Local).AddTicks(6211), new DateTime(2024, 9, 20, 16, 35, 37, 943, DateTimeKind.Local).AddTicks(6651) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 20, 16, 35, 37, 962, DateTimeKind.Local).AddTicks(1570), new DateTime(2024, 9, 20, 16, 35, 37, 962, DateTimeKind.Local).AddTicks(1890) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 20, 16, 35, 37, 962, DateTimeKind.Local).AddTicks(2499), new DateTime(2024, 9, 20, 16, 35, 37, 962, DateTimeKind.Local).AddTicks(2505) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 20, 16, 35, 37, 962, DateTimeKind.Local).AddTicks(2521), new DateTime(2024, 9, 20, 16, 35, 37, 962, DateTimeKind.Local).AddTicks(2522) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 20, 16, 35, 37, 962, DateTimeKind.Local).AddTicks(2527), new DateTime(2024, 9, 20, 16, 35, 37, 962, DateTimeKind.Local).AddTicks(2528) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 20, 16, 35, 37, 962, DateTimeKind.Local).AddTicks(2532), new DateTime(2024, 9, 20, 16, 35, 37, 962, DateTimeKind.Local).AddTicks(2533) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 20, 16, 35, 37, 962, DateTimeKind.Local).AddTicks(2541), new DateTime(2024, 9, 20, 16, 35, 37, 962, DateTimeKind.Local).AddTicks(2542) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 20, 16, 35, 37, 962, DateTimeKind.Local).AddTicks(2546), new DateTime(2024, 9, 20, 16, 35, 37, 962, DateTimeKind.Local).AddTicks(2547) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 20, 16, 35, 37, 962, DateTimeKind.Local).AddTicks(2552), new DateTime(2024, 9, 20, 16, 35, 37, 962, DateTimeKind.Local).AddTicks(2553) });
        }
    }
}
