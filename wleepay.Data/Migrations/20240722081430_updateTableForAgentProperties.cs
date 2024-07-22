using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class updateTableForAgentProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 22, 13, 44, 29, 333, DateTimeKind.Local).AddTicks(8164),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 10, 15, 8, 54, 130, DateTimeKind.Local).AddTicks(5035));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 22, 13, 44, 29, 333, DateTimeKind.Local).AddTicks(7809),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 10, 15, 8, 54, 130, DateTimeKind.Local).AddTicks(4626));

            migrationBuilder.AddColumn<string>(
                name: "Merchandise",
                table: "UserDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TraningCompletionDate",
                table: "UserDetail",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 22, 13, 44, 29, 326, DateTimeKind.Local).AddTicks(6582),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 10, 15, 8, 54, 121, DateTimeKind.Local).AddTicks(6649));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 22, 13, 44, 29, 279, DateTimeKind.Local).AddTicks(6684),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 10, 15, 8, 54, 95, DateTimeKind.Local).AddTicks(5748));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 22, 13, 44, 29, 276, DateTimeKind.Local).AddTicks(8089),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 10, 15, 8, 54, 94, DateTimeKind.Local).AddTicks(2710));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "96f02597-febc-4ea0-8c5f-66c90e773116", "AQAAAAEAACcQAAAAECjefyrVyhjEGxg0clIC/rFkSaNO9GefhASEut1+DHea8Lkt+Vi0dT2Wq+k6vmGlvQ==", "e5f6bc18-3b12-4d51-894e-e0cf268eabe5" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 22, 13, 44, 29, 321, DateTimeKind.Local).AddTicks(2962), new DateTime(2024, 7, 22, 13, 44, 29, 321, DateTimeKind.Local).AddTicks(3274) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 22, 13, 44, 29, 334, DateTimeKind.Local).AddTicks(9250), new DateTime(2024, 7, 22, 13, 44, 29, 334, DateTimeKind.Local).AddTicks(9536) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 22, 13, 44, 29, 335, DateTimeKind.Local).AddTicks(41), new DateTime(2024, 7, 22, 13, 44, 29, 335, DateTimeKind.Local).AddTicks(45) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 22, 13, 44, 29, 335, DateTimeKind.Local).AddTicks(52), new DateTime(2024, 7, 22, 13, 44, 29, 335, DateTimeKind.Local).AddTicks(53) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 22, 13, 44, 29, 335, DateTimeKind.Local).AddTicks(56), new DateTime(2024, 7, 22, 13, 44, 29, 335, DateTimeKind.Local).AddTicks(57) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 22, 13, 44, 29, 335, DateTimeKind.Local).AddTicks(61), new DateTime(2024, 7, 22, 13, 44, 29, 335, DateTimeKind.Local).AddTicks(62) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 22, 13, 44, 29, 335, DateTimeKind.Local).AddTicks(68), new DateTime(2024, 7, 22, 13, 44, 29, 335, DateTimeKind.Local).AddTicks(69) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Merchandise",
                table: "UserDetail");

            migrationBuilder.DropColumn(
                name: "TraningCompletionDate",
                table: "UserDetail");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 10, 15, 8, 54, 130, DateTimeKind.Local).AddTicks(5035),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 22, 13, 44, 29, 333, DateTimeKind.Local).AddTicks(8164));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 10, 15, 8, 54, 130, DateTimeKind.Local).AddTicks(4626),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 22, 13, 44, 29, 333, DateTimeKind.Local).AddTicks(7809));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 10, 15, 8, 54, 121, DateTimeKind.Local).AddTicks(6649),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 22, 13, 44, 29, 326, DateTimeKind.Local).AddTicks(6582));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 10, 15, 8, 54, 95, DateTimeKind.Local).AddTicks(5748),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 22, 13, 44, 29, 279, DateTimeKind.Local).AddTicks(6684));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 10, 15, 8, 54, 94, DateTimeKind.Local).AddTicks(2710),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 22, 13, 44, 29, 276, DateTimeKind.Local).AddTicks(8089));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d54e5202-e173-479d-99aa-af25d7eef134", "AQAAAAEAACcQAAAAEG2R+D+4kzgeG4bmxKWd0BORrlGFe7y8tJFspAfaG/nn9YYxSj2rGLoqo+TSgqpzCg==", "cb5537c6-fe77-465e-85bf-e87eb67f7ffb" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 10, 15, 8, 54, 115, DateTimeKind.Local).AddTicks(4250), new DateTime(2024, 7, 10, 15, 8, 54, 115, DateTimeKind.Local).AddTicks(4615) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 10, 15, 8, 54, 131, DateTimeKind.Local).AddTicks(7899), new DateTime(2024, 7, 10, 15, 8, 54, 131, DateTimeKind.Local).AddTicks(8202) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 10, 15, 8, 54, 131, DateTimeKind.Local).AddTicks(8598), new DateTime(2024, 7, 10, 15, 8, 54, 131, DateTimeKind.Local).AddTicks(8603) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 10, 15, 8, 54, 131, DateTimeKind.Local).AddTicks(8611), new DateTime(2024, 7, 10, 15, 8, 54, 131, DateTimeKind.Local).AddTicks(8612) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 10, 15, 8, 54, 131, DateTimeKind.Local).AddTicks(8617), new DateTime(2024, 7, 10, 15, 8, 54, 131, DateTimeKind.Local).AddTicks(8619) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 10, 15, 8, 54, 131, DateTimeKind.Local).AddTicks(8624), new DateTime(2024, 7, 10, 15, 8, 54, 131, DateTimeKind.Local).AddTicks(8625) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 10, 15, 8, 54, 131, DateTimeKind.Local).AddTicks(8633), new DateTime(2024, 7, 10, 15, 8, 54, 131, DateTimeKind.Local).AddTicks(8634) });
        }
    }
}
