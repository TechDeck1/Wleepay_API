using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class AddedNewTransaferTypeForAgentCashout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 20, 11, 48, 56, 896, DateTimeKind.Local).AddTicks(3713),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 5, 17, 20, 57, 420, DateTimeKind.Local).AddTicks(873));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 20, 11, 48, 56, 896, DateTimeKind.Local).AddTicks(3247),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 5, 17, 20, 57, 420, DateTimeKind.Local).AddTicks(436));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 20, 11, 48, 56, 887, DateTimeKind.Local).AddTicks(2176),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 5, 17, 20, 57, 410, DateTimeKind.Local).AddTicks(8238));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 20, 11, 48, 56, 854, DateTimeKind.Local).AddTicks(9083),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 5, 17, 20, 57, 376, DateTimeKind.Local).AddTicks(8697));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 20, 11, 48, 56, 852, DateTimeKind.Local).AddTicks(818),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 5, 17, 20, 57, 373, DateTimeKind.Local).AddTicks(8070));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1ed9cb64-dd01-461c-adc2-462319ec31d0", "AQAAAAEAACcQAAAAEH1MdTj2uHN76o12VRnhP+2qri0G9l9V3zhdBlu8kiZoOXNSxeb2kVtqHvw/yRvQlQ==", "6b374b7d-20ad-4f0a-981b-f52d11091efd" });

            migrationBuilder.InsertData(
                table: "TransferType",
                columns: new[] { "Id", "Description", "FromWalletType", "Name", "ToWalletType", "TransferFee" },
                values: new object[,]
                {
                    //{ 10, "Transfer from member a/c to system float a/c", 7, "Mobile Recharge Transfer", 1, null },
                    //{ 11, "Transfer from member a/c to system float a/c", 7, "Bill Payment", 1, null },
                    { 12, "Transfer from agent a/c to general cash account float a/c", 6, "On Hand Agent Cashout", 1, null }
                });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 20, 11, 48, 56, 880, DateTimeKind.Local).AddTicks(4890), new DateTime(2024, 8, 20, 11, 48, 56, 880, DateTimeKind.Local).AddTicks(5367) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 20, 11, 48, 56, 897, DateTimeKind.Local).AddTicks(7746), new DateTime(2024, 8, 20, 11, 48, 56, 897, DateTimeKind.Local).AddTicks(8187) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 20, 11, 48, 56, 897, DateTimeKind.Local).AddTicks(8651), new DateTime(2024, 8, 20, 11, 48, 56, 897, DateTimeKind.Local).AddTicks(8656) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 20, 11, 48, 56, 897, DateTimeKind.Local).AddTicks(8663), new DateTime(2024, 8, 20, 11, 48, 56, 897, DateTimeKind.Local).AddTicks(8664) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 20, 11, 48, 56, 897, DateTimeKind.Local).AddTicks(8668), new DateTime(2024, 8, 20, 11, 48, 56, 897, DateTimeKind.Local).AddTicks(8669) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 20, 11, 48, 56, 897, DateTimeKind.Local).AddTicks(8673), new DateTime(2024, 8, 20, 11, 48, 56, 897, DateTimeKind.Local).AddTicks(8674) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 20, 11, 48, 56, 897, DateTimeKind.Local).AddTicks(8681), new DateTime(2024, 8, 20, 11, 48, 56, 897, DateTimeKind.Local).AddTicks(8682) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "TransferType",
            //    keyColumn: "Id",
            //    keyValue: 10);

            //migrationBuilder.DeleteData(
            //    table: "TransferType",
            //    keyColumn: "Id",
            //    keyValue: 11);

            migrationBuilder.DeleteData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 5, 17, 20, 57, 420, DateTimeKind.Local).AddTicks(873),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 20, 11, 48, 56, 896, DateTimeKind.Local).AddTicks(3713));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 5, 17, 20, 57, 420, DateTimeKind.Local).AddTicks(436),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 20, 11, 48, 56, 896, DateTimeKind.Local).AddTicks(3247));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 5, 17, 20, 57, 410, DateTimeKind.Local).AddTicks(8238),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 20, 11, 48, 56, 887, DateTimeKind.Local).AddTicks(2176));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 5, 17, 20, 57, 376, DateTimeKind.Local).AddTicks(8697),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 20, 11, 48, 56, 854, DateTimeKind.Local).AddTicks(9083));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 5, 17, 20, 57, 373, DateTimeKind.Local).AddTicks(8070),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 20, 11, 48, 56, 852, DateTimeKind.Local).AddTicks(818));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f6c534e8-4a7c-482a-a0df-08e2387783e6", "AQAAAAEAACcQAAAAEHKBLeIx8lsFXXdId1Nrye28/aF+Yutue0qmboBNz5sUmppo0mhO8VIAJHzHspc1iQ==", "c2514672-99eb-4240-8ff5-b9c8bfb5edf4" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 5, 17, 20, 57, 403, DateTimeKind.Local).AddTicks(9115), new DateTime(2024, 8, 5, 17, 20, 57, 403, DateTimeKind.Local).AddTicks(9453) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 5, 17, 20, 57, 421, DateTimeKind.Local).AddTicks(4438), new DateTime(2024, 8, 5, 17, 20, 57, 421, DateTimeKind.Local).AddTicks(4789) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 5, 17, 20, 57, 421, DateTimeKind.Local).AddTicks(5212), new DateTime(2024, 8, 5, 17, 20, 57, 421, DateTimeKind.Local).AddTicks(5217) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 5, 17, 20, 57, 421, DateTimeKind.Local).AddTicks(5225), new DateTime(2024, 8, 5, 17, 20, 57, 421, DateTimeKind.Local).AddTicks(5226) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 5, 17, 20, 57, 421, DateTimeKind.Local).AddTicks(5231), new DateTime(2024, 8, 5, 17, 20, 57, 421, DateTimeKind.Local).AddTicks(5232) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 5, 17, 20, 57, 421, DateTimeKind.Local).AddTicks(5237), new DateTime(2024, 8, 5, 17, 20, 57, 421, DateTimeKind.Local).AddTicks(5238) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 5, 17, 20, 57, 421, DateTimeKind.Local).AddTicks(5244), new DateTime(2024, 8, 5, 17, 20, 57, 421, DateTimeKind.Local).AddTicks(5245) });
        }
    }
}
