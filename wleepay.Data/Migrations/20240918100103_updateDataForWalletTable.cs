using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class updateDataForWalletTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 15, 31, 2, 252, DateTimeKind.Local).AddTicks(9648),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 13, 24, 20, 264, DateTimeKind.Local).AddTicks(5284));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 15, 31, 2, 252, DateTimeKind.Local).AddTicks(9360),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 13, 24, 20, 264, DateTimeKind.Local).AddTicks(4918));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 15, 31, 2, 246, DateTimeKind.Local).AddTicks(2716),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 13, 24, 20, 256, DateTimeKind.Local).AddTicks(4053));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 15, 31, 2, 225, DateTimeKind.Local).AddTicks(3622),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 13, 24, 20, 230, DateTimeKind.Local).AddTicks(51));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 15, 31, 2, 223, DateTimeKind.Local).AddTicks(4656),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 13, 24, 20, 228, DateTimeKind.Local).AddTicks(2203));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "15cdbfe3-a543-4d9a-b72c-752a375ecb87", "AQAAAAEAACcQAAAAEDQwblShPeY7e5K0bzzcBFF2WxNUxhWz4VSUyiYWqz8471YVdL4BKfjQibhA+kGFZg==", "b5028a7c-264b-4054-bbc8-6a202acb9f49" });

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

            migrationBuilder.InsertData(
                table: "Wallet",
                columns: new[] { "Id", "Balance", "CreatedDate", "CurrencyId", "Type", "UpdatedDate", "UserId" },
                values: new object[,]
                {
                    { 13L, 0.0, new DateTime(2024, 9, 18, 15, 31, 2, 253, DateTimeKind.Local).AddTicks(9529), 1, 9, new DateTime(2024, 9, 18, 15, 31, 2, 253, DateTimeKind.Local).AddTicks(9530), "07ac193d-26e7-4039-9440-fbb53988b878" },
                    { 14L, 0.0, new DateTime(2024, 9, 18, 15, 31, 2, 253, DateTimeKind.Local).AddTicks(9533), 2, 9, new DateTime(2024, 9, 18, 15, 31, 2, 253, DateTimeKind.Local).AddTicks(9534), "07ac193d-26e7-4039-9440-fbb53988b878" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 13, 24, 20, 264, DateTimeKind.Local).AddTicks(5284),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 15, 31, 2, 252, DateTimeKind.Local).AddTicks(9648));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 13, 24, 20, 264, DateTimeKind.Local).AddTicks(4918),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 15, 31, 2, 252, DateTimeKind.Local).AddTicks(9360));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 13, 24, 20, 256, DateTimeKind.Local).AddTicks(4053),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 15, 31, 2, 246, DateTimeKind.Local).AddTicks(2716));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 13, 24, 20, 230, DateTimeKind.Local).AddTicks(51),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 15, 31, 2, 225, DateTimeKind.Local).AddTicks(3622));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 13, 24, 20, 228, DateTimeKind.Local).AddTicks(2203),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 15, 31, 2, 223, DateTimeKind.Local).AddTicks(4656));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "043b5e2d-2647-49cb-ba84-839009422477", "AQAAAAEAACcQAAAAEJ27XN+ZLbqiuq7E49ZnabB49mY5iC/W+t3A7Edbo5pWg/ur+4EonrpRKOmrXft9TA==", "55287e53-cbc6-4a0c-9496-18af93ab2504" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 18, 13, 24, 20, 250, DateTimeKind.Local).AddTicks(1740), new DateTime(2024, 9, 18, 13, 24, 20, 250, DateTimeKind.Local).AddTicks(2096) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 18, 13, 24, 20, 265, DateTimeKind.Local).AddTicks(6765), new DateTime(2024, 9, 18, 13, 24, 20, 265, DateTimeKind.Local).AddTicks(7074) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 18, 13, 24, 20, 265, DateTimeKind.Local).AddTicks(7483), new DateTime(2024, 9, 18, 13, 24, 20, 265, DateTimeKind.Local).AddTicks(7488) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 18, 13, 24, 20, 265, DateTimeKind.Local).AddTicks(7496), new DateTime(2024, 9, 18, 13, 24, 20, 265, DateTimeKind.Local).AddTicks(7497) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 18, 13, 24, 20, 265, DateTimeKind.Local).AddTicks(7501), new DateTime(2024, 9, 18, 13, 24, 20, 265, DateTimeKind.Local).AddTicks(7502) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 18, 13, 24, 20, 265, DateTimeKind.Local).AddTicks(7507), new DateTime(2024, 9, 18, 13, 24, 20, 265, DateTimeKind.Local).AddTicks(7508) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 18, 13, 24, 20, 265, DateTimeKind.Local).AddTicks(7515), new DateTime(2024, 9, 18, 13, 24, 20, 265, DateTimeKind.Local).AddTicks(7516) });
        }
    }
}
