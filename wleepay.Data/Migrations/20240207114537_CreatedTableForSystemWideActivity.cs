using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class CreatedTableForSystemWideActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 17, 15, 36, 264, DateTimeKind.Local).AddTicks(8664),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 13, 39, 5, 115, DateTimeKind.Local).AddTicks(6333));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 17, 15, 36, 264, DateTimeKind.Local).AddTicks(8250),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 13, 39, 5, 115, DateTimeKind.Local).AddTicks(5897));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 17, 15, 36, 258, DateTimeKind.Local).AddTicks(9046),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 13, 39, 5, 109, DateTimeKind.Local).AddTicks(6835));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 17, 15, 36, 218, DateTimeKind.Local).AddTicks(5725),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 13, 39, 5, 65, DateTimeKind.Local).AddTicks(518));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 17, 15, 36, 213, DateTimeKind.Local).AddTicks(9422),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 13, 39, 5, 62, DateTimeKind.Local).AddTicks(5636));

            migrationBuilder.CreateTable(
                name: "SystemStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemStatus", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "29233aa3-8a97-4faa-a617-4c17c82fce65", "AQAAAAEAACcQAAAAEDVIyOM+Wh4cs17tEPxlDzC6DIvRWwBGqqNXNjENlkMU8cKgvT8LOWkVa/EgfKPhxQ==", "c1fe32fa-b7f6-4eab-870e-faedd754fe22" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemStatus");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 13, 39, 5, 115, DateTimeKind.Local).AddTicks(6333),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 17, 15, 36, 264, DateTimeKind.Local).AddTicks(8664));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 13, 39, 5, 115, DateTimeKind.Local).AddTicks(5897),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 17, 15, 36, 264, DateTimeKind.Local).AddTicks(8250));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 13, 39, 5, 109, DateTimeKind.Local).AddTicks(6835),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 17, 15, 36, 258, DateTimeKind.Local).AddTicks(9046));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 13, 39, 5, 65, DateTimeKind.Local).AddTicks(518),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 17, 15, 36, 218, DateTimeKind.Local).AddTicks(5725));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 7, 13, 39, 5, 62, DateTimeKind.Local).AddTicks(5636),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 7, 17, 15, 36, 213, DateTimeKind.Local).AddTicks(9422));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "70d364e0-d3a0-42b1-9ca1-5d2eb871c4a0", "AQAAAAEAACcQAAAAEG/k0eWQE71WD88wLMmKJiY/9VKuggpi/RyD5SOBhZTL4eddr88n0jFCbk4oqyBcew==", "bfee7eee-43c5-4b54-aeb8-3e60e2400499" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 13, 39, 5, 102, DateTimeKind.Local).AddTicks(1191), new DateTime(2024, 2, 7, 13, 39, 5, 102, DateTimeKind.Local).AddTicks(1405) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 13, 39, 5, 116, DateTimeKind.Local).AddTicks(5512), new DateTime(2024, 2, 7, 13, 39, 5, 116, DateTimeKind.Local).AddTicks(5687) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 13, 39, 5, 116, DateTimeKind.Local).AddTicks(5933), new DateTime(2024, 2, 7, 13, 39, 5, 116, DateTimeKind.Local).AddTicks(5938) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 13, 39, 5, 116, DateTimeKind.Local).AddTicks(5944), new DateTime(2024, 2, 7, 13, 39, 5, 116, DateTimeKind.Local).AddTicks(5945) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 13, 39, 5, 116, DateTimeKind.Local).AddTicks(6012), new DateTime(2024, 2, 7, 13, 39, 5, 116, DateTimeKind.Local).AddTicks(6013) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 13, 39, 5, 116, DateTimeKind.Local).AddTicks(6018), new DateTime(2024, 2, 7, 13, 39, 5, 116, DateTimeKind.Local).AddTicks(6019) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 7, 13, 39, 5, 116, DateTimeKind.Local).AddTicks(6024), new DateTime(2024, 2, 7, 13, 39, 5, 116, DateTimeKind.Local).AddTicks(6025) });
        }
    }
}
