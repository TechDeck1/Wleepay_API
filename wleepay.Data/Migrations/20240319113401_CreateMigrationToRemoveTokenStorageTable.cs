using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class CreateMigrationToRemoveTokenStorageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TokenStorage");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 19, 17, 4, 0, 401, DateTimeKind.Local).AddTicks(467),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 21, 13, 56, 27, 841, DateTimeKind.Local).AddTicks(1102));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 19, 17, 4, 0, 401, DateTimeKind.Local).AddTicks(90),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 21, 13, 56, 27, 841, DateTimeKind.Local).AddTicks(515));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 19, 17, 4, 0, 394, DateTimeKind.Local).AddTicks(9749),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 21, 13, 56, 27, 834, DateTimeKind.Local).AddTicks(1590));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 19, 17, 4, 0, 370, DateTimeKind.Local).AddTicks(356),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 21, 13, 56, 27, 806, DateTimeKind.Local).AddTicks(6401));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 19, 17, 4, 0, 368, DateTimeKind.Local).AddTicks(4818),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 21, 13, 56, 27, 804, DateTimeKind.Local).AddTicks(1359));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d9bbf0f9-2478-40a4-8550-190ed5f2e17b", "AQAAAAEAACcQAAAAEFWLBvqobIOgpqDCnityIkp6zgCF7N0PBlKelpd/68+vZRYAbzXSP08q8JJ0fhHLQw==", "8ec8bcea-7d49-4568-9ef2-d2f99d3b9e2b" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 3, 19, 17, 4, 0, 390, DateTimeKind.Local).AddTicks(3666), new DateTime(2024, 3, 19, 17, 4, 0, 390, DateTimeKind.Local).AddTicks(3864) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 3, 19, 17, 4, 0, 402, DateTimeKind.Local).AddTicks(962), new DateTime(2024, 3, 19, 17, 4, 0, 402, DateTimeKind.Local).AddTicks(1144) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 3, 19, 17, 4, 0, 402, DateTimeKind.Local).AddTicks(1397), new DateTime(2024, 3, 19, 17, 4, 0, 402, DateTimeKind.Local).AddTicks(1401) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 3, 19, 17, 4, 0, 402, DateTimeKind.Local).AddTicks(1407), new DateTime(2024, 3, 19, 17, 4, 0, 402, DateTimeKind.Local).AddTicks(1408) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 3, 19, 17, 4, 0, 402, DateTimeKind.Local).AddTicks(1412), new DateTime(2024, 3, 19, 17, 4, 0, 402, DateTimeKind.Local).AddTicks(1413) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 3, 19, 17, 4, 0, 402, DateTimeKind.Local).AddTicks(1417), new DateTime(2024, 3, 19, 17, 4, 0, 402, DateTimeKind.Local).AddTicks(1418) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 3, 19, 17, 4, 0, 402, DateTimeKind.Local).AddTicks(1423), new DateTime(2024, 3, 19, 17, 4, 0, 402, DateTimeKind.Local).AddTicks(1424) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 21, 13, 56, 27, 841, DateTimeKind.Local).AddTicks(1102),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 19, 17, 4, 0, 401, DateTimeKind.Local).AddTicks(467));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 21, 13, 56, 27, 841, DateTimeKind.Local).AddTicks(515),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 19, 17, 4, 0, 401, DateTimeKind.Local).AddTicks(90));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 21, 13, 56, 27, 834, DateTimeKind.Local).AddTicks(1590),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 19, 17, 4, 0, 394, DateTimeKind.Local).AddTicks(9749));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 21, 13, 56, 27, 806, DateTimeKind.Local).AddTicks(6401),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 19, 17, 4, 0, 370, DateTimeKind.Local).AddTicks(356));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 21, 13, 56, 27, 804, DateTimeKind.Local).AddTicks(1359),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 19, 17, 4, 0, 368, DateTimeKind.Local).AddTicks(4818));

            migrationBuilder.CreateTable(
                name: "TokenStorage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenStorage", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1f54253-db9e-4e2a-b78b-278b565e9deb", "AQAAAAEAACcQAAAAEGk1IkbiKE3XxxqVTvdzCx82kZbA7GUpShCiadi4b7HXv/kryNmZZCTQbrnrCB2tuA==", "ea32bb5f-0d17-4a03-9956-73d34e7169e4" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 21, 13, 56, 27, 827, DateTimeKind.Local).AddTicks(5567), new DateTime(2024, 2, 21, 13, 56, 27, 827, DateTimeKind.Local).AddTicks(5763) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 21, 13, 56, 27, 842, DateTimeKind.Local).AddTicks(1727), new DateTime(2024, 2, 21, 13, 56, 27, 842, DateTimeKind.Local).AddTicks(1906) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 21, 13, 56, 27, 842, DateTimeKind.Local).AddTicks(2157), new DateTime(2024, 2, 21, 13, 56, 27, 842, DateTimeKind.Local).AddTicks(2161) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 21, 13, 56, 27, 842, DateTimeKind.Local).AddTicks(2166), new DateTime(2024, 2, 21, 13, 56, 27, 842, DateTimeKind.Local).AddTicks(2167) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 21, 13, 56, 27, 842, DateTimeKind.Local).AddTicks(2216), new DateTime(2024, 2, 21, 13, 56, 27, 842, DateTimeKind.Local).AddTicks(2217) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 21, 13, 56, 27, 842, DateTimeKind.Local).AddTicks(2221), new DateTime(2024, 2, 21, 13, 56, 27, 842, DateTimeKind.Local).AddTicks(2222) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 2, 21, 13, 56, 27, 842, DateTimeKind.Local).AddTicks(2228), new DateTime(2024, 2, 21, 13, 56, 27, 842, DateTimeKind.Local).AddTicks(2229) });
        }
    }
}
