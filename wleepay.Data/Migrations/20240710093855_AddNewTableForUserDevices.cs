using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class AddNewTableForUserDevices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 10, 15, 8, 54, 130, DateTimeKind.Local).AddTicks(5035),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 10, 13, 1, 55, 906, DateTimeKind.Local).AddTicks(1812));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 10, 15, 8, 54, 130, DateTimeKind.Local).AddTicks(4626),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 10, 13, 1, 55, 906, DateTimeKind.Local).AddTicks(1263));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 10, 15, 8, 54, 121, DateTimeKind.Local).AddTicks(6649),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 10, 13, 1, 55, 878, DateTimeKind.Local).AddTicks(623));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 10, 15, 8, 54, 95, DateTimeKind.Local).AddTicks(5748),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 10, 13, 1, 55, 837, DateTimeKind.Local).AddTicks(3082));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 10, 15, 8, 54, 94, DateTimeKind.Local).AddTicks(2710),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 10, 13, 1, 55, 834, DateTimeKind.Local).AddTicks(5806));

            migrationBuilder.CreateTable(
                name: "UserDevices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    DeviceId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FcmToken = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDevices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDevices_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_UserDevices_UserId",
                table: "UserDevices",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDevices");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 10, 13, 1, 55, 906, DateTimeKind.Local).AddTicks(1812),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 10, 15, 8, 54, 130, DateTimeKind.Local).AddTicks(5035));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 10, 13, 1, 55, 906, DateTimeKind.Local).AddTicks(1263),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 10, 15, 8, 54, 130, DateTimeKind.Local).AddTicks(4626));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 10, 13, 1, 55, 878, DateTimeKind.Local).AddTicks(623),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 10, 15, 8, 54, 121, DateTimeKind.Local).AddTicks(6649));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 10, 13, 1, 55, 837, DateTimeKind.Local).AddTicks(3082),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 10, 15, 8, 54, 95, DateTimeKind.Local).AddTicks(5748));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 10, 13, 1, 55, 834, DateTimeKind.Local).AddTicks(5806),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 10, 15, 8, 54, 94, DateTimeKind.Local).AddTicks(2710));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ffd545d1-43ff-4671-a221-be282044d530", "AQAAAAEAACcQAAAAENK/hp/W3eMbcyjBIJfaTJccJPdHdtlxhOiJfghscCQbH3bHE8+JapojhAtvUTyswQ==", "584063a7-514c-4aa6-ba3d-221f8169918c" });

            migrationBuilder.UpdateData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 10, 13, 1, 55, 866, DateTimeKind.Local).AddTicks(1594), new DateTime(2024, 7, 10, 13, 1, 55, 866, DateTimeKind.Local).AddTicks(2147) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 10, 13, 1, 55, 907, DateTimeKind.Local).AddTicks(5721), new DateTime(2024, 7, 10, 13, 1, 55, 907, DateTimeKind.Local).AddTicks(6040) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 10, 13, 1, 55, 907, DateTimeKind.Local).AddTicks(6496), new DateTime(2024, 7, 10, 13, 1, 55, 907, DateTimeKind.Local).AddTicks(6501) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 10, 13, 1, 55, 907, DateTimeKind.Local).AddTicks(6511), new DateTime(2024, 7, 10, 13, 1, 55, 907, DateTimeKind.Local).AddTicks(6512) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 10, 13, 1, 55, 907, DateTimeKind.Local).AddTicks(6517), new DateTime(2024, 7, 10, 13, 1, 55, 907, DateTimeKind.Local).AddTicks(6519) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 10, 13, 1, 55, 907, DateTimeKind.Local).AddTicks(6584), new DateTime(2024, 7, 10, 13, 1, 55, 907, DateTimeKind.Local).AddTicks(6585) });

            migrationBuilder.UpdateData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 10, 13, 1, 55, 907, DateTimeKind.Local).AddTicks(6592), new DateTime(2024, 7, 10, 13, 1, 55, 907, DateTimeKind.Local).AddTicks(6593) });
        }
    }
}
