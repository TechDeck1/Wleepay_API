using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class UpdateTransactionTableForMakeValuesNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 13, 24, 20, 264, DateTimeKind.Local).AddTicks(5284),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 17, 15, 37, 52, 447, DateTimeKind.Local).AddTicks(8316));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 13, 24, 20, 264, DateTimeKind.Local).AddTicks(4918),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 17, 15, 37, 52, 447, DateTimeKind.Local).AddTicks(7893));

            migrationBuilder.AlterColumn<long>(
                name: "ToWalletId",
                table: "Transaction",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "FromWalletId",
                table: "Transaction",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 13, 24, 20, 256, DateTimeKind.Local).AddTicks(4053),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 17, 15, 37, 52, 440, DateTimeKind.Local).AddTicks(209));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 13, 24, 20, 230, DateTimeKind.Local).AddTicks(51),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 17, 15, 37, 52, 416, DateTimeKind.Local).AddTicks(2581));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 13, 24, 20, 228, DateTimeKind.Local).AddTicks(2203),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 17, 15, 37, 52, 414, DateTimeKind.Local).AddTicks(7498));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 17, 15, 37, 52, 447, DateTimeKind.Local).AddTicks(8316),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 13, 24, 20, 264, DateTimeKind.Local).AddTicks(5284));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 17, 15, 37, 52, 447, DateTimeKind.Local).AddTicks(7893),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 13, 24, 20, 264, DateTimeKind.Local).AddTicks(4918));

            migrationBuilder.AlterColumn<long>(
                name: "ToWalletId",
                table: "Transaction",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "FromWalletId",
                table: "Transaction",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 17, 15, 37, 52, 440, DateTimeKind.Local).AddTicks(209),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 13, 24, 20, 256, DateTimeKind.Local).AddTicks(4053));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 17, 15, 37, 52, 416, DateTimeKind.Local).AddTicks(2581),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 13, 24, 20, 230, DateTimeKind.Local).AddTicks(51));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 17, 15, 37, 52, 414, DateTimeKind.Local).AddTicks(7498),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 13, 24, 20, 228, DateTimeKind.Local).AddTicks(2203));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "76ff5fd2-c2b5-4219-9f2b-60eb74427dd8", "AQAAAAEAACcQAAAAECFfi7Xgy8SK1VZymOP7LYUyi4j4GOkQcbICRgHrZ285CojL6tkP7t6k7PAmszpvQQ==", "fd2d5d7c-123b-4dba-be1c-51554c505051" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 17, 15, 37, 52, 434, DateTimeKind.Local).AddTicks(5296), new DateTime(2024, 9, 17, 15, 37, 52, 434, DateTimeKind.Local).AddTicks(5609) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 17, 15, 37, 52, 448, DateTimeKind.Local).AddTicks(8992), new DateTime(2024, 9, 17, 15, 37, 52, 448, DateTimeKind.Local).AddTicks(9268) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 17, 15, 37, 52, 448, DateTimeKind.Local).AddTicks(9633), new DateTime(2024, 9, 17, 15, 37, 52, 448, DateTimeKind.Local).AddTicks(9637) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 17, 15, 37, 52, 448, DateTimeKind.Local).AddTicks(9643), new DateTime(2024, 9, 17, 15, 37, 52, 448, DateTimeKind.Local).AddTicks(9644) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 17, 15, 37, 52, 448, DateTimeKind.Local).AddTicks(9648), new DateTime(2024, 9, 17, 15, 37, 52, 448, DateTimeKind.Local).AddTicks(9648) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 17, 15, 37, 52, 448, DateTimeKind.Local).AddTicks(9652), new DateTime(2024, 9, 17, 15, 37, 52, 448, DateTimeKind.Local).AddTicks(9653) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 17, 15, 37, 52, 448, DateTimeKind.Local).AddTicks(9659), new DateTime(2024, 9, 17, 15, 37, 52, 448, DateTimeKind.Local).AddTicks(9660) });
        }
    }
}
