using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class UpdatedTheCurrencyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "Wallet",
            //    keyColumn: "Id",
            //    keyValue: 13L);

            //migrationBuilder.DeleteData(
            //    table: "Wallet",
            //    keyColumn: "Id",
            //    keyValue: 14L);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 20, 16, 35, 37, 960, DateTimeKind.Local).AddTicks(7853),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 15, 31, 2, 252, DateTimeKind.Local).AddTicks(9648));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 20, 16, 35, 37, 960, DateTimeKind.Local).AddTicks(7393),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 15, 31, 2, 252, DateTimeKind.Local).AddTicks(9360));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 20, 16, 35, 37, 950, DateTimeKind.Local).AddTicks(6384),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 15, 31, 2, 246, DateTimeKind.Local).AddTicks(2716));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 20, 16, 35, 37, 910, DateTimeKind.Local).AddTicks(6746),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 15, 31, 2, 225, DateTimeKind.Local).AddTicks(3622));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 20, 16, 35, 37, 908, DateTimeKind.Local).AddTicks(2291),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 15, 31, 2, 223, DateTimeKind.Local).AddTicks(4656));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "83966b0f-6826-4472-aaa8-44dbd6313443", "AQAAAAEAACcQAAAAEKuvF+v/euw51OJxhh7Lyzjco2KmKXBq0aV1VllxlFV6pEnpmAkH6PeyINKjxYkcnA==", "92268efc-2743-4897-86f6-3fcda972e95f" });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "LD");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "USD");

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

            //migrationBuilder.InsertData(
            //    table: "Wallet",
            //    columns: new[] { "Id", "Balance", "CreatedDate", "CurrencyId", "Type", "UpdatedDate", "UserId" },
            //    values: new object[,]
            //    {
            //        { 7L, 0.0, new DateTime(2024, 9, 20, 16, 35, 37, 962, DateTimeKind.Local).AddTicks(2546), 1, 9, new DateTime(2024, 9, 20, 16, 35, 37, 962, DateTimeKind.Local).AddTicks(2547), "07ac193d-26e7-4039-9440-fbb53988b878" },
            //        { 8L, 0.0, new DateTime(2024, 9, 20, 16, 35, 37, 962, DateTimeKind.Local).AddTicks(2552), 2, 9, new DateTime(2024, 9, 20, 16, 35, 37, 962, DateTimeKind.Local).AddTicks(2553), "07ac193d-26e7-4039-9440-fbb53988b878" }
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "Wallet",
            //    keyColumn: "Id",
            //    keyValue: 7L);

            //migrationBuilder.DeleteData(
            //    table: "Wallet",
            //    keyColumn: "Id",
            //    keyValue: 8L);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 15, 31, 2, 252, DateTimeKind.Local).AddTicks(9648),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 20, 16, 35, 37, 960, DateTimeKind.Local).AddTicks(7853));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 15, 31, 2, 252, DateTimeKind.Local).AddTicks(9360),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 20, 16, 35, 37, 960, DateTimeKind.Local).AddTicks(7393));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 15, 31, 2, 246, DateTimeKind.Local).AddTicks(2716),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 20, 16, 35, 37, 950, DateTimeKind.Local).AddTicks(6384));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 15, 31, 2, 225, DateTimeKind.Local).AddTicks(3622),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 20, 16, 35, 37, 910, DateTimeKind.Local).AddTicks(6746));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 15, 31, 2, 223, DateTimeKind.Local).AddTicks(4656),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 20, 16, 35, 37, 908, DateTimeKind.Local).AddTicks(2291));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "15cdbfe3-a543-4d9a-b72c-752a375ecb87", "AQAAAAEAACcQAAAAEDQwblShPeY7e5K0bzzcBFF2WxNUxhWz4VSUyiYWqz8471YVdL4BKfjQibhA+kGFZg==", "b5028a7c-264b-4054-bbc8-6a202acb9f49" });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Liberian Dollar");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "United State Of America Dollar");

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 18, 15, 31, 2, 241, DateTimeKind.Local).AddTicks(2576), new DateTime(2024, 9, 18, 15, 31, 2, 241, DateTimeKind.Local).AddTicks(2859) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 18, 15, 31, 2, 253, DateTimeKind.Local).AddTicks(8913), new DateTime(2024, 9, 18, 15, 31, 2, 253, DateTimeKind.Local).AddTicks(9163) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 18, 15, 31, 2, 253, DateTimeKind.Local).AddTicks(9498), new DateTime(2024, 9, 18, 15, 31, 2, 253, DateTimeKind.Local).AddTicks(9502) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 18, 15, 31, 2, 253, DateTimeKind.Local).AddTicks(9508), new DateTime(2024, 9, 18, 15, 31, 2, 253, DateTimeKind.Local).AddTicks(9509) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 18, 15, 31, 2, 253, DateTimeKind.Local).AddTicks(9513), new DateTime(2024, 9, 18, 15, 31, 2, 253, DateTimeKind.Local).AddTicks(9514) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 18, 15, 31, 2, 253, DateTimeKind.Local).AddTicks(9517), new DateTime(2024, 9, 18, 15, 31, 2, 253, DateTimeKind.Local).AddTicks(9518) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 18, 15, 31, 2, 253, DateTimeKind.Local).AddTicks(9524), new DateTime(2024, 9, 18, 15, 31, 2, 253, DateTimeKind.Local).AddTicks(9525) });

            //migrationBuilder.InsertData(
            //    table: "Wallet",
            //    columns: new[] { "Id", "Balance", "CreatedDate", "CurrencyId", "Type", "UpdatedDate", "UserId" },
            //    values: new object[,]
            //    {
            //        { 13L, 0.0, new DateTime(2024, 9, 18, 15, 31, 2, 253, DateTimeKind.Local).AddTicks(9529), 1, 9, new DateTime(2024, 9, 18, 15, 31, 2, 253, DateTimeKind.Local).AddTicks(9530), "07ac193d-26e7-4039-9440-fbb53988b878" },
            //        { 14L, 0.0, new DateTime(2024, 9, 18, 15, 31, 2, 253, DateTimeKind.Local).AddTicks(9533), 2, 9, new DateTime(2024, 9, 18, 15, 31, 2, 253, DateTimeKind.Local).AddTicks(9534), "07ac193d-26e7-4039-9440-fbb53988b878" }
            //    });
        }
    }
}
