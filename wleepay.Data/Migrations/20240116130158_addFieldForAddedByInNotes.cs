using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class addFieldForAddedByInNotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 16, 18, 31, 57, 867, DateTimeKind.Local).AddTicks(3364),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 15, 15, 30, 10, 845, DateTimeKind.Local).AddTicks(4769));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 16, 18, 31, 57, 867, DateTimeKind.Local).AddTicks(2892),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 15, 15, 30, 10, 845, DateTimeKind.Local).AddTicks(4200));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 16, 18, 31, 57, 861, DateTimeKind.Local).AddTicks(3086),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 15, 15, 30, 10, 835, DateTimeKind.Local).AddTicks(5307));

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Notes",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AddColumn<string>(
                name: "AddedBy",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 16, 18, 31, 57, 816, DateTimeKind.Local).AddTicks(8785),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 15, 15, 30, 10, 808, DateTimeKind.Local).AddTicks(7086));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 16, 18, 31, 57, 813, DateTimeKind.Local).AddTicks(9671),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 15, 15, 30, 10, 806, DateTimeKind.Local).AddTicks(5180));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedBy",
                table: "Notes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 15, 15, 30, 10, 845, DateTimeKind.Local).AddTicks(4769),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 16, 18, 31, 57, 867, DateTimeKind.Local).AddTicks(3364));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 15, 15, 30, 10, 845, DateTimeKind.Local).AddTicks(4200),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 16, 18, 31, 57, 867, DateTimeKind.Local).AddTicks(2892));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 15, 15, 30, 10, 835, DateTimeKind.Local).AddTicks(5307),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 16, 18, 31, 57, 861, DateTimeKind.Local).AddTicks(3086));

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Notes",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 15, 15, 30, 10, 808, DateTimeKind.Local).AddTicks(7086),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 16, 18, 31, 57, 816, DateTimeKind.Local).AddTicks(8785));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 15, 15, 30, 10, 806, DateTimeKind.Local).AddTicks(5180),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 16, 18, 31, 57, 813, DateTimeKind.Local).AddTicks(9671));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "952ccf11-10c4-48ed-8ddd-dd324499e044", "AQAAAAEAACcQAAAAEAEulok2qivD2Sl1AhunXlg+C2wdcpFAtU/RG4CLWE3OprznGTcuZljOrLV5o16l/w==", "00591472-fd8b-47ff-9216-9e0aa8f64fe7" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 15, 15, 30, 10, 830, DateTimeKind.Local).AddTicks(4249), new DateTime(2024, 1, 15, 15, 30, 10, 830, DateTimeKind.Local).AddTicks(4464) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 15, 15, 30, 10, 846, DateTimeKind.Local).AddTicks(8289), new DateTime(2024, 1, 15, 15, 30, 10, 846, DateTimeKind.Local).AddTicks(8537) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 15, 15, 30, 10, 846, DateTimeKind.Local).AddTicks(8903), new DateTime(2024, 1, 15, 15, 30, 10, 846, DateTimeKind.Local).AddTicks(8908) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 15, 15, 30, 10, 846, DateTimeKind.Local).AddTicks(8916), new DateTime(2024, 1, 15, 15, 30, 10, 846, DateTimeKind.Local).AddTicks(8918) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 15, 15, 30, 10, 846, DateTimeKind.Local).AddTicks(8923), new DateTime(2024, 1, 15, 15, 30, 10, 846, DateTimeKind.Local).AddTicks(8924) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 15, 15, 30, 10, 846, DateTimeKind.Local).AddTicks(8929), new DateTime(2024, 1, 15, 15, 30, 10, 846, DateTimeKind.Local).AddTicks(8931) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 15, 15, 30, 10, 846, DateTimeKind.Local).AddTicks(8939), new DateTime(2024, 1, 15, 15, 30, 10, 846, DateTimeKind.Local).AddTicks(8940) });
        }
    }
}
