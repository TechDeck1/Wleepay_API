using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class AddNewFieldInCustomerTickets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 19, 11, 57, 44, 617, DateTimeKind.Local).AddTicks(4443),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 16, 18, 31, 57, 867, DateTimeKind.Local).AddTicks(3364));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 19, 11, 57, 44, 617, DateTimeKind.Local).AddTicks(3956),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 16, 18, 31, 57, 867, DateTimeKind.Local).AddTicks(2892));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 19, 11, 57, 44, 608, DateTimeKind.Local).AddTicks(7822),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 16, 18, 31, 57, 861, DateTimeKind.Local).AddTicks(3086));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 19, 11, 57, 44, 561, DateTimeKind.Local).AddTicks(9074),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 16, 18, 31, 57, 816, DateTimeKind.Local).AddTicks(8785));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 19, 11, 57, 44, 559, DateTimeKind.Local).AddTicks(7788),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 16, 18, 31, 57, 813, DateTimeKind.Local).AddTicks(9671));

            migrationBuilder.AddColumn<bool>(
                name: "IsSeen",
                table: "CustomerTickets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fed5facc-49f2-47e0-8fd9-f6622716d55b", "AQAAAAEAACcQAAAAEIWhxHhRMx791UCz6YOGN9KhAnlZp3beBHCH0eG/k3PSbJ2r7NYscP3TG05fWqNteQ==", "939bd675-dc6d-4339-9f3a-6a22164b2e28" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 19, 11, 57, 44, 602, DateTimeKind.Local).AddTicks(3012), new DateTime(2024, 1, 19, 11, 57, 44, 602, DateTimeKind.Local).AddTicks(3253) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 19, 11, 57, 44, 619, DateTimeKind.Local).AddTicks(1617), new DateTime(2024, 1, 19, 11, 57, 44, 619, DateTimeKind.Local).AddTicks(1816) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 19, 11, 57, 44, 619, DateTimeKind.Local).AddTicks(2113), new DateTime(2024, 1, 19, 11, 57, 44, 619, DateTimeKind.Local).AddTicks(2117) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 19, 11, 57, 44, 619, DateTimeKind.Local).AddTicks(2174), new DateTime(2024, 1, 19, 11, 57, 44, 619, DateTimeKind.Local).AddTicks(2175) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 19, 11, 57, 44, 619, DateTimeKind.Local).AddTicks(2180), new DateTime(2024, 1, 19, 11, 57, 44, 619, DateTimeKind.Local).AddTicks(2181) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 19, 11, 57, 44, 619, DateTimeKind.Local).AddTicks(2185), new DateTime(2024, 1, 19, 11, 57, 44, 619, DateTimeKind.Local).AddTicks(2186) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 19, 11, 57, 44, 619, DateTimeKind.Local).AddTicks(2192), new DateTime(2024, 1, 19, 11, 57, 44, 619, DateTimeKind.Local).AddTicks(2193) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSeen",
                table: "CustomerTickets");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 16, 18, 31, 57, 867, DateTimeKind.Local).AddTicks(3364),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 19, 11, 57, 44, 617, DateTimeKind.Local).AddTicks(4443));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 16, 18, 31, 57, 867, DateTimeKind.Local).AddTicks(2892),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 19, 11, 57, 44, 617, DateTimeKind.Local).AddTicks(3956));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 16, 18, 31, 57, 861, DateTimeKind.Local).AddTicks(3086),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 19, 11, 57, 44, 608, DateTimeKind.Local).AddTicks(7822));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 16, 18, 31, 57, 816, DateTimeKind.Local).AddTicks(8785),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 19, 11, 57, 44, 561, DateTimeKind.Local).AddTicks(9074));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 16, 18, 31, 57, 813, DateTimeKind.Local).AddTicks(9671),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 19, 11, 57, 44, 559, DateTimeKind.Local).AddTicks(7788));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5e3d5f42-1ac9-4f27-a001-ace86a5ace7a", "AQAAAAEAACcQAAAAECXJeoKMLKOhlQIeZGHldD4yRH2/vYJxxLJN0ujpE3+IwNFSb+SrEJTB6CJ3gzo0XQ==", "1d0045e0-ea14-4adc-ada8-33e6d24ea93d" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 16, 18, 31, 57, 856, DateTimeKind.Local).AddTicks(4500), new DateTime(2024, 1, 16, 18, 31, 57, 856, DateTimeKind.Local).AddTicks(4764) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 16, 18, 31, 57, 868, DateTimeKind.Local).AddTicks(3132), new DateTime(2024, 1, 16, 18, 31, 57, 868, DateTimeKind.Local).AddTicks(3307) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 16, 18, 31, 57, 868, DateTimeKind.Local).AddTicks(3557), new DateTime(2024, 1, 16, 18, 31, 57, 868, DateTimeKind.Local).AddTicks(3561) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 16, 18, 31, 57, 868, DateTimeKind.Local).AddTicks(3566), new DateTime(2024, 1, 16, 18, 31, 57, 868, DateTimeKind.Local).AddTicks(3567) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 16, 18, 31, 57, 868, DateTimeKind.Local).AddTicks(3570), new DateTime(2024, 1, 16, 18, 31, 57, 868, DateTimeKind.Local).AddTicks(3571) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 16, 18, 31, 57, 868, DateTimeKind.Local).AddTicks(3574), new DateTime(2024, 1, 16, 18, 31, 57, 868, DateTimeKind.Local).AddTicks(3575) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 16, 18, 31, 57, 868, DateTimeKind.Local).AddTicks(3628), new DateTime(2024, 1, 16, 18, 31, 57, 868, DateTimeKind.Local).AddTicks(3629) });
        }
    }
}
