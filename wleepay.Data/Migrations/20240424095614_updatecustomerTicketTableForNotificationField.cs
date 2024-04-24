using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class updatecustomerTicketTableForNotificationField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 24, 15, 26, 13, 271, DateTimeKind.Local).AddTicks(5058),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 19, 17, 4, 0, 401, DateTimeKind.Local).AddTicks(467));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 24, 15, 26, 13, 271, DateTimeKind.Local).AddTicks(4706),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 19, 17, 4, 0, 401, DateTimeKind.Local).AddTicks(90));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 24, 15, 26, 13, 264, DateTimeKind.Local).AddTicks(7853),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 19, 17, 4, 0, 394, DateTimeKind.Local).AddTicks(9749));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 24, 15, 26, 13, 217, DateTimeKind.Local).AddTicks(4741),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 19, 17, 4, 0, 370, DateTimeKind.Local).AddTicks(356));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 24, 15, 26, 13, 215, DateTimeKind.Local).AddTicks(1755),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 19, 17, 4, 0, 368, DateTimeKind.Local).AddTicks(4818));

            migrationBuilder.AddColumn<int>(
                name: "NotificationStatus",
                table: "CustomerTickets",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cf5056eb-4c2c-4966-903a-86e97f0e90af", "AQAAAAEAACcQAAAAEBnU1c1TwC6DZ5is59hCw9V02esc3ogGhRG9NxeC/M9iTdLP62W2/1npdYWy0cRvyA==", "60011faa-ffc9-4689-afda-d5462de4cdd3" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 4, 24, 15, 26, 13, 259, DateTimeKind.Local).AddTicks(4426), new DateTime(2024, 4, 24, 15, 26, 13, 259, DateTimeKind.Local).AddTicks(4722) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 4, 24, 15, 26, 13, 272, DateTimeKind.Local).AddTicks(4872), new DateTime(2024, 4, 24, 15, 26, 13, 272, DateTimeKind.Local).AddTicks(5126) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 4, 24, 15, 26, 13, 272, DateTimeKind.Local).AddTicks(5476), new DateTime(2024, 4, 24, 15, 26, 13, 272, DateTimeKind.Local).AddTicks(5480) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 4, 24, 15, 26, 13, 272, DateTimeKind.Local).AddTicks(5487), new DateTime(2024, 4, 24, 15, 26, 13, 272, DateTimeKind.Local).AddTicks(5488) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 4, 24, 15, 26, 13, 272, DateTimeKind.Local).AddTicks(5492), new DateTime(2024, 4, 24, 15, 26, 13, 272, DateTimeKind.Local).AddTicks(5493) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 4, 24, 15, 26, 13, 272, DateTimeKind.Local).AddTicks(5497), new DateTime(2024, 4, 24, 15, 26, 13, 272, DateTimeKind.Local).AddTicks(5498) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 4, 24, 15, 26, 13, 272, DateTimeKind.Local).AddTicks(5502), new DateTime(2024, 4, 24, 15, 26, 13, 272, DateTimeKind.Local).AddTicks(5503) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotificationStatus",
                table: "CustomerTickets");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 19, 17, 4, 0, 401, DateTimeKind.Local).AddTicks(467),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 24, 15, 26, 13, 271, DateTimeKind.Local).AddTicks(5058));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 19, 17, 4, 0, 401, DateTimeKind.Local).AddTicks(90),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 24, 15, 26, 13, 271, DateTimeKind.Local).AddTicks(4706));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 19, 17, 4, 0, 394, DateTimeKind.Local).AddTicks(9749),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 24, 15, 26, 13, 264, DateTimeKind.Local).AddTicks(7853));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 19, 17, 4, 0, 370, DateTimeKind.Local).AddTicks(356),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 24, 15, 26, 13, 217, DateTimeKind.Local).AddTicks(4741));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 19, 17, 4, 0, 368, DateTimeKind.Local).AddTicks(4818),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 24, 15, 26, 13, 215, DateTimeKind.Local).AddTicks(1755));

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
    }
}
