using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class UpdateAddressTableAddedNewField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 16, 51, 40, 135, DateTimeKind.Local).AddTicks(4306),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 16, 40, 34, 287, DateTimeKind.Local).AddTicks(3982));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 16, 51, 40, 135, DateTimeKind.Local).AddTicks(3883),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 16, 40, 34, 287, DateTimeKind.Local).AddTicks(3584));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 16, 51, 40, 127, DateTimeKind.Local).AddTicks(939),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 16, 40, 34, 281, DateTimeKind.Local).AddTicks(8066));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 16, 51, 40, 93, DateTimeKind.Local).AddTicks(8926),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 16, 40, 34, 260, DateTimeKind.Local).AddTicks(8129));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 16, 51, 40, 92, DateTimeKind.Local).AddTicks(1489),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 16, 40, 34, 259, DateTimeKind.Local).AddTicks(675));

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "Address",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8f3f28e3-669d-4c95-9d83-3487952b2bc4", "AQAAAAEAACcQAAAAEFQ0d7uh4hXw7654XtwLFzhrqYMnf8ju5OltkAL5KCwB4rYKcc/LS/C+d5Q7Z506hA==", "73b2790a-f916-4ce6-a573-6347046a58b4" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "District",
                table: "Address");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 16, 40, 34, 287, DateTimeKind.Local).AddTicks(3982),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 16, 51, 40, 135, DateTimeKind.Local).AddTicks(4306));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 16, 40, 34, 287, DateTimeKind.Local).AddTicks(3584),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 16, 51, 40, 135, DateTimeKind.Local).AddTicks(3883));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 16, 40, 34, 281, DateTimeKind.Local).AddTicks(8066),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 16, 51, 40, 127, DateTimeKind.Local).AddTicks(939));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 16, 40, 34, 260, DateTimeKind.Local).AddTicks(8129),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 16, 51, 40, 93, DateTimeKind.Local).AddTicks(8926));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 29, 16, 40, 34, 259, DateTimeKind.Local).AddTicks(675),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 29, 16, 51, 40, 92, DateTimeKind.Local).AddTicks(1489));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c9fe633d-3200-49ac-8435-612342d29086", "AQAAAAEAACcQAAAAELCoi74fBuJiQ8Xxpjb6iHk3Y0QhD7QacpmC0Q31F8dHZP0sSHTHdbx8kwbkJrcQiQ==", "6fd52dd3-daf9-48dc-8f8b-aafd308ad0d7" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 16, 40, 34, 276, DateTimeKind.Local).AddTicks(8348), new DateTime(2024, 1, 29, 16, 40, 34, 276, DateTimeKind.Local).AddTicks(8533) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 16, 40, 34, 288, DateTimeKind.Local).AddTicks(4378), new DateTime(2024, 1, 29, 16, 40, 34, 288, DateTimeKind.Local).AddTicks(4838) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 16, 40, 34, 288, DateTimeKind.Local).AddTicks(5287), new DateTime(2024, 1, 29, 16, 40, 34, 288, DateTimeKind.Local).AddTicks(5292) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 16, 40, 34, 288, DateTimeKind.Local).AddTicks(5307), new DateTime(2024, 1, 29, 16, 40, 34, 288, DateTimeKind.Local).AddTicks(5310) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 16, 40, 34, 288, DateTimeKind.Local).AddTicks(5318), new DateTime(2024, 1, 29, 16, 40, 34, 288, DateTimeKind.Local).AddTicks(5320) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 16, 40, 34, 288, DateTimeKind.Local).AddTicks(5324), new DateTime(2024, 1, 29, 16, 40, 34, 288, DateTimeKind.Local).AddTicks(5325) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 29, 16, 40, 34, 288, DateTimeKind.Local).AddTicks(5438), new DateTime(2024, 1, 29, 16, 40, 34, 288, DateTimeKind.Local).AddTicks(5442) });
        }
    }
}
