using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class UpdateTransaferTypeTableforType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 8, 15, 17, 3, 461, DateTimeKind.Local).AddTicks(7395),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 17, 15, 36, 264, DateTimeKind.Local).AddTicks(8664));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 8, 15, 17, 3, 461, DateTimeKind.Local).AddTicks(6978),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 17, 15, 36, 264, DateTimeKind.Local).AddTicks(8250));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 8, 15, 17, 3, 455, DateTimeKind.Local).AddTicks(13),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 17, 15, 36, 258, DateTimeKind.Local).AddTicks(9046));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 8, 15, 17, 3, 430, DateTimeKind.Local).AddTicks(3024),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 17, 15, 36, 218, DateTimeKind.Local).AddTicks(5725));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 8, 15, 17, 3, 427, DateTimeKind.Local).AddTicks(289),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 17, 15, 36, 213, DateTimeKind.Local).AddTicks(9422));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "48602f9c-c378-4379-a3b1-65ea080921e8", "AQAAAAEAACcQAAAAEOqOPFo9jKrPviNPwGRbBJGg4wxlbai013imtF747WcxouZIyxT/KXSNVDBgvdM/Mg==", "2d6bf4fe-a2b2-4e77-9368-04db4eb34db8" });

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Transfer from system float n a/c to sytem wallet", "Top Up System Wallet" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 8, 15, 17, 3, 449, DateTimeKind.Local).AddTicks(7245), new DateTime(2024, 2, 8, 15, 17, 3, 449, DateTimeKind.Local).AddTicks(7463) });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 17, 15, 36, 264, DateTimeKind.Local).AddTicks(8664),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 8, 15, 17, 3, 461, DateTimeKind.Local).AddTicks(7395));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 17, 15, 36, 264, DateTimeKind.Local).AddTicks(8250),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 8, 15, 17, 3, 461, DateTimeKind.Local).AddTicks(6978));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 17, 15, 36, 258, DateTimeKind.Local).AddTicks(9046),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 8, 15, 17, 3, 455, DateTimeKind.Local).AddTicks(13));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 17, 15, 36, 218, DateTimeKind.Local).AddTicks(5725),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 8, 15, 17, 3, 430, DateTimeKind.Local).AddTicks(3024));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 17, 15, 36, 213, DateTimeKind.Local).AddTicks(9422),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 8, 15, 17, 3, 427, DateTimeKind.Local).AddTicks(289));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "29233aa3-8a97-4faa-a617-4c17c82fce65", "AQAAAAEAACcQAAAAEDVIyOM+Wh4cs17tEPxlDzC6DIvRWwBGqqNXNjENlkMU8cKgvT8LOWkVa/EgfKPhxQ==", "c1fe32fa-b7f6-4eab-870e-faedd754fe22" });

            migrationBuilder.UpdateData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Transfer from system float n a/c to sytem admin a/c", "Cash On Hand" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 17, 15, 36, 254, DateTimeKind.Local).AddTicks(350), new DateTime(2024, 2, 7, 17, 15, 36, 254, DateTimeKind.Local).AddTicks(561) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 17, 15, 36, 265, DateTimeKind.Local).AddTicks(7895), new DateTime(2024, 2, 7, 17, 15, 36, 265, DateTimeKind.Local).AddTicks(8079) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 17, 15, 36, 265, DateTimeKind.Local).AddTicks(8340), new DateTime(2024, 2, 7, 17, 15, 36, 265, DateTimeKind.Local).AddTicks(8343) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 17, 15, 36, 265, DateTimeKind.Local).AddTicks(8349), new DateTime(2024, 2, 7, 17, 15, 36, 265, DateTimeKind.Local).AddTicks(8350) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 17, 15, 36, 265, DateTimeKind.Local).AddTicks(8354), new DateTime(2024, 2, 7, 17, 15, 36, 265, DateTimeKind.Local).AddTicks(8355) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 17, 15, 36, 265, DateTimeKind.Local).AddTicks(8359), new DateTime(2024, 2, 7, 17, 15, 36, 265, DateTimeKind.Local).AddTicks(8361) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 17, 15, 36, 265, DateTimeKind.Local).AddTicks(8367), new DateTime(2024, 2, 7, 17, 15, 36, 265, DateTimeKind.Local).AddTicks(8368) });
        }
    }
}
