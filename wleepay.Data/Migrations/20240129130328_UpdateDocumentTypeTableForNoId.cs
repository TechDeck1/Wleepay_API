using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class UpdateDocumentTypeTableForNoId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 18, 33, 27, 290, DateTimeKind.Local).AddTicks(7310),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 16, 51, 40, 135, DateTimeKind.Local).AddTicks(4306));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 18, 33, 27, 290, DateTimeKind.Local).AddTicks(6912),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 16, 51, 40, 135, DateTimeKind.Local).AddTicks(3883));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 18, 33, 27, 284, DateTimeKind.Local).AddTicks(2348),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 16, 51, 40, 127, DateTimeKind.Local).AddTicks(939));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 18, 33, 27, 261, DateTimeKind.Local).AddTicks(2161),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 16, 51, 40, 93, DateTimeKind.Local).AddTicks(8926));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 18, 33, 27, 258, DateTimeKind.Local).AddTicks(5001),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 16, 51, 40, 92, DateTimeKind.Local).AddTicks(1489));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "14f9d7b2-5d15-4758-ad5f-6416fe05f35f", "AQAAAAEAACcQAAAAEFOrEqTbcdAhOntdepnOXd9w8tgK1/49udBQx8Vxxgwm/kvZGklseQr+tSPzUYTPBw==", "3410af65-b5b5-4526-95d0-f058e180d21b" });

            migrationBuilder.UpdateData(
                table: "DocumentType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Driver’s License");

            migrationBuilder.UpdateData(
                table: "DocumentType",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "National ID Card");

            migrationBuilder.UpdateData(
                table: "DocumentType",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "No Id");

            migrationBuilder.InsertData(
                table: "DocumentType",
                columns: new[] { "Id", "Name" },
                values: new object[] { 5, "Voter Card" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 18, 33, 27, 279, DateTimeKind.Local).AddTicks(2344), new DateTime(2024, 1, 29, 18, 33, 27, 279, DateTimeKind.Local).AddTicks(2534) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 18, 33, 27, 291, DateTimeKind.Local).AddTicks(7031), new DateTime(2024, 1, 29, 18, 33, 27, 291, DateTimeKind.Local).AddTicks(7208) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 18, 33, 27, 291, DateTimeKind.Local).AddTicks(7468), new DateTime(2024, 1, 29, 18, 33, 27, 291, DateTimeKind.Local).AddTicks(7472) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 18, 33, 27, 291, DateTimeKind.Local).AddTicks(7478), new DateTime(2024, 1, 29, 18, 33, 27, 291, DateTimeKind.Local).AddTicks(7479) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 18, 33, 27, 291, DateTimeKind.Local).AddTicks(7483), new DateTime(2024, 1, 29, 18, 33, 27, 291, DateTimeKind.Local).AddTicks(7484) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 18, 33, 27, 291, DateTimeKind.Local).AddTicks(7488), new DateTime(2024, 1, 29, 18, 33, 27, 291, DateTimeKind.Local).AddTicks(7489) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 18, 33, 27, 291, DateTimeKind.Local).AddTicks(7495), new DateTime(2024, 1, 29, 18, 33, 27, 291, DateTimeKind.Local).AddTicks(7496) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DocumentType",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 16, 51, 40, 135, DateTimeKind.Local).AddTicks(4306),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 18, 33, 27, 290, DateTimeKind.Local).AddTicks(7310));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 16, 51, 40, 135, DateTimeKind.Local).AddTicks(3883),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 18, 33, 27, 290, DateTimeKind.Local).AddTicks(6912));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 16, 51, 40, 127, DateTimeKind.Local).AddTicks(939),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 18, 33, 27, 284, DateTimeKind.Local).AddTicks(2348));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 16, 51, 40, 93, DateTimeKind.Local).AddTicks(8926),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 18, 33, 27, 261, DateTimeKind.Local).AddTicks(2161));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 16, 51, 40, 92, DateTimeKind.Local).AddTicks(1489),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 18, 33, 27, 258, DateTimeKind.Local).AddTicks(5001));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8f3f28e3-669d-4c95-9d83-3487952b2bc4", "AQAAAAEAACcQAAAAEFQ0d7uh4hXw7654XtwLFzhrqYMnf8ju5OltkAL5KCwB4rYKcc/LS/C+d5Q7Z506hA==", "73b2790a-f916-4ce6-a573-6347046a58b4" });

            migrationBuilder.UpdateData(
                table: "DocumentType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "National ID Card");

            migrationBuilder.UpdateData(
                table: "DocumentType",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Driver’s License");

            migrationBuilder.UpdateData(
                table: "DocumentType",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Voter Card");

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 16, 51, 40, 119, DateTimeKind.Local).AddTicks(5937), new DateTime(2024, 1, 29, 16, 51, 40, 119, DateTimeKind.Local).AddTicks(6287) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 16, 51, 40, 136, DateTimeKind.Local).AddTicks(3793), new DateTime(2024, 1, 29, 16, 51, 40, 136, DateTimeKind.Local).AddTicks(3980) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 16, 51, 40, 136, DateTimeKind.Local).AddTicks(4266), new DateTime(2024, 1, 29, 16, 51, 40, 136, DateTimeKind.Local).AddTicks(4270) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 16, 51, 40, 136, DateTimeKind.Local).AddTicks(4275), new DateTime(2024, 1, 29, 16, 51, 40, 136, DateTimeKind.Local).AddTicks(4276) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 16, 51, 40, 136, DateTimeKind.Local).AddTicks(4280), new DateTime(2024, 1, 29, 16, 51, 40, 136, DateTimeKind.Local).AddTicks(4281) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 16, 51, 40, 136, DateTimeKind.Local).AddTicks(4284), new DateTime(2024, 1, 29, 16, 51, 40, 136, DateTimeKind.Local).AddTicks(4285) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 16, 51, 40, 136, DateTimeKind.Local).AddTicks(4291), new DateTime(2024, 1, 29, 16, 51, 40, 136, DateTimeKind.Local).AddTicks(4292) });
        }
    }
}
