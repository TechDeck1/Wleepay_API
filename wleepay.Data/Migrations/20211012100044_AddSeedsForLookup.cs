using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class AddSeedsForLookup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 12, 15, 30, 43, 716, DateTimeKind.Local).AddTicks(1979),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 12, 12, 29, 58, 189, DateTimeKind.Local).AddTicks(6267));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 12, 15, 30, 43, 716, DateTimeKind.Local).AddTicks(1582),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 12, 12, 29, 58, 189, DateTimeKind.Local).AddTicks(5948));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 12, 15, 30, 43, 709, DateTimeKind.Local).AddTicks(8202),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 12, 12, 29, 58, 184, DateTimeKind.Local).AddTicks(1229));

            migrationBuilder.AddColumn<double>(
                name: "AccountBalanceLimit",
                table: "KYCLevel",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TransactionDayLimit",
                table: "KYCLevel",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TransactionMonthLimit",
                table: "KYCLevel",
                type: "float",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 12, 15, 30, 43, 689, DateTimeKind.Local).AddTicks(4358),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 12, 12, 29, 58, 170, DateTimeKind.Local).AddTicks(5661));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 12, 15, 30, 43, 687, DateTimeKind.Local).AddTicks(5968),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 12, 12, 29, 58, 168, DateTimeKind.Local).AddTicks(8761));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "88b7d36f-2f30-4682-b801-fbd6b40b2af6", "0aefcb24-3cff-4c92-9bb6-18fef511f3ed", "Member", "MEMBER" },
                    { "dbcebf6c-174e-407f-b170-136cbc68813e", "3f4da7d6-e6ae-493e-a343-07732ad7bc65", "SuperAdmin", "SUPERADMIN" },
                    { "e4e7188b-6ecb-4278-aeee-17271f20d7ce", "64559bb4-bcea-4c00-a7c9-ac0cd7037bfd", "Agent", "AGENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "07ac193d-26e7-4039-9440-fbb53988b878", 0, "f160074d-3bdc-4f9a-83ff-1659fd474cd8", "admin@wlee.com", true, false, null, "ADMIN@WLEE.COM", "ADMIN@WLEE.COM", "AQAAAAEAACcQAAAAEOE1iUyyJNt7Gz0XmlaJ+5T024PzYmG3k1WC7NQiD5ALB7BT/8G0N1BVwsUhkN7rPg==", false, "8463aff9-c897-4898-b4a3-6e201afbecaa", false, "admin@wlee.com" });

            migrationBuilder.InsertData(
                table: "County",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 15, "Sinoe" },
                    { 14, "River Gee" },
                    { 13, "River Cess" },
                    { 12, "Maryland" },
                    { 11, "Grand Kru" },
                    { 10, "Grand Gedeh" },
                    { 9, "Lofa" },
                    { 7, "Gbarpolu" },
                    { 6, "Nimba" },
                    { 5, "Montserrado" },
                    { 4, "Margibi" },
                    { 3, "Grand Bassa" },
                    { 2, "Bong" },
                    { 1, "Bomi" },
                    { 8, "Grant Cape Mount" }
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Name", "Symbol" },
                values: new object[,]
                {
                    { 2, "United State Of America Dollar", "$" },
                    { 1, "Liberian Dollar", "L$" }
                });

            migrationBuilder.InsertData(
                table: "DocumentType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 3, "Voter Card" },
                    { 2, "Driver’s License" },
                    { 1, "National ID Card" }
                });

            migrationBuilder.InsertData(
                table: "KYCLevel",
                columns: new[] { "Id", "AccountBalanceLimit", "Description", "Name", "TransactionDayLimit", "TransactionMonthLimit" },
                values: new object[,]
                {
                    { 1, 1000.0, null, "L1", 250.0, 2000.0 },
                    { 2, 4000.0, null, "L2", 1000.0, 8000.0 },
                    { 3, null, null, "L3", null, null }
                });

            migrationBuilder.InsertData(
                table: "KYCStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Pending" },
                    { 2, "Approved" },
                    { 3, "Rejected" },
                    { 4, "Under Review" }
                });

            migrationBuilder.InsertData(
                table: "TransactionStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 5, "Cancel" },
                    { 4, "Failed" },
                    { 3, "Approved" },
                    { 2, "Rejected" },
                    { 1, "Pending" }
                });

            migrationBuilder.InsertData(
                table: "TransferFee",
                columns: new[] { "Id", "AgentCommission", "Description", "OrganizationFee", "TaxAmount" },
                values: new object[] { 1, 1.3500000000000001, "CashOut Type transfer fee and taxes", 1.21, 0.14000000000000001 });

            migrationBuilder.InsertData(
                table: "WalletType",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "System Account", "Debit float N" },
                    { 2, "System Account", "Organization P" },
                    { 3, "System Account", "Wlee Merchant Fees" },
                    { 4, "System Account", "WleeTax" },
                    { 5, "User Account", "WleePay Agent Comission Account" }
                });

            migrationBuilder.InsertData(
                table: "WalletType",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 6, "User Account", "WleePay Agent Account" });

            migrationBuilder.InsertData(
                table: "WalletType",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 7, "User Account", "WleePay Member Account" });

            migrationBuilder.InsertData(
                table: "WalletType",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 8, "User Account", "WleePay Merchant Account" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "dbcebf6c-174e-407f-b170-136cbc68813e", "07ac193d-26e7-4039-9440-fbb53988b878" });

            migrationBuilder.InsertData(
                table: "TransferType",
                columns: new[] { "Id", "Description", "FromWalletType", "Name", "ToWalletType", "TransferFee" },
                values: new object[,]
                {
                    { 7, "Fill Agent a/c from system float n a/c", 1, "Fill Agent", 6, null },
                    { 1, "Transfer from member a/c to agent a/c", 7, "Cash Out", 6, 1 },
                    { 2, "Transfer from memeber to aget commission account", 7, "Generated Agent Commission", 5, null },
                    { 3, "Transfer from aget to wlee tax account", 7, "Wlee Tax Transfer", 4, null },
                    { 4, "Transfer from member to organization account p", 7, "Cash Out Standard Fee Split", 2, null },
                    { 5, "Transfer from wleepay member acount to other member account", 7, "Wlee Transfer", 7, null },
                    { 6, "TopUp member wallet from agent a/c", 6, "Top Up", 7, null }
                });

            migrationBuilder.InsertData(
                table: "UserDetail",
                columns: new[] { "Id", "AccountNumber", "CountyId", "CreatedDate", "DOB", "FirstName", "Gender", "Image", "IsLoggedIn", "KYCLevel", "KYCStatusId", "LastName", "PhoneNumber", "UpdatedDate", "UserId" },
                values: new object[] { 1, "50285368", 2, new DateTime(2021, 10, 12, 15, 30, 43, 704, DateTimeKind.Local).AddTicks(4597), new DateTime(1985, 9, 20, 5, 1, 50, 563, DateTimeKind.Unspecified), "Admin", "Male", null, true, null, 2, "Admin", "+919845654543", new DateTime(2021, 10, 12, 15, 30, 43, 704, DateTimeKind.Local).AddTicks(5012), "07ac193d-26e7-4039-9440-fbb53988b878" });

            migrationBuilder.InsertData(
                table: "Wallet",
                columns: new[] { "Id", "Balance", "CreatedDate", "CurrencyId", "Type", "UpdatedDate", "UserId" },
                values: new object[,]
                {
                    { 1L, 0.0, new DateTime(2021, 10, 12, 15, 30, 43, 717, DateTimeKind.Local).AddTicks(3334), 1, 1, new DateTime(2021, 10, 12, 15, 30, 43, 717, DateTimeKind.Local).AddTicks(3723), "07ac193d-26e7-4039-9440-fbb53988b878" },
                    { 2L, 0.0, new DateTime(2021, 10, 12, 15, 30, 43, 717, DateTimeKind.Local).AddTicks(4216), 2, 1, new DateTime(2021, 10, 12, 15, 30, 43, 717, DateTimeKind.Local).AddTicks(4220), "07ac193d-26e7-4039-9440-fbb53988b878" },
                    { 3L, 0.0, new DateTime(2021, 10, 12, 15, 30, 43, 717, DateTimeKind.Local).AddTicks(4227), 1, 2, new DateTime(2021, 10, 12, 15, 30, 43, 717, DateTimeKind.Local).AddTicks(4228), "07ac193d-26e7-4039-9440-fbb53988b878" },
                    { 4L, 0.0, new DateTime(2021, 10, 12, 15, 30, 43, 717, DateTimeKind.Local).AddTicks(4232), 2, 2, new DateTime(2021, 10, 12, 15, 30, 43, 717, DateTimeKind.Local).AddTicks(4233), "07ac193d-26e7-4039-9440-fbb53988b878" },
                    { 5L, 0.0, new DateTime(2021, 10, 12, 15, 30, 43, 717, DateTimeKind.Local).AddTicks(4237), 1, 4, new DateTime(2021, 10, 12, 15, 30, 43, 717, DateTimeKind.Local).AddTicks(4238), "07ac193d-26e7-4039-9440-fbb53988b878" },
                    { 6L, 0.0, new DateTime(2021, 10, 12, 15, 30, 43, 717, DateTimeKind.Local).AddTicks(4244), 2, 4, new DateTime(2021, 10, 12, 15, 30, 43, 717, DateTimeKind.Local).AddTicks(4245), "07ac193d-26e7-4039-9440-fbb53988b878" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "88b7d36f-2f30-4682-b801-fbd6b40b2af6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4e7188b-6ecb-4278-aeee-17271f20d7ce");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dbcebf6c-174e-407f-b170-136cbc68813e", "07ac193d-26e7-4039-9440-fbb53988b878" });

            migrationBuilder.DeleteData(
                table: "County",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "County",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "County",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "County",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "County",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "County",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "County",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "County",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "County",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "County",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "County",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "County",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "County",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "County",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "DocumentType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DocumentType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DocumentType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "KYCLevel",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "KYCLevel",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "KYCLevel",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "KYCStatus",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "KYCStatus",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "KYCStatus",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TransactionStatus",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TransactionStatus",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TransactionStatus",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TransactionStatus",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TransactionStatus",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TransferType",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "UserDetail",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "WalletType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "WalletType",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dbcebf6c-174e-407f-b170-136cbc68813e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07ac193d-26e7-4039-9440-fbb53988b878");

            migrationBuilder.DeleteData(
                table: "County",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "KYCStatus",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TransferFee",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WalletType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WalletType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WalletType",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "WalletType",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "WalletType",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "WalletType",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "AccountBalanceLimit",
                table: "KYCLevel");

            migrationBuilder.DropColumn(
                name: "TransactionDayLimit",
                table: "KYCLevel");

            migrationBuilder.DropColumn(
                name: "TransactionMonthLimit",
                table: "KYCLevel");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 12, 12, 29, 58, 189, DateTimeKind.Local).AddTicks(6267),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 12, 15, 30, 43, 716, DateTimeKind.Local).AddTicks(1979));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Wallet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 12, 12, 29, 58, 189, DateTimeKind.Local).AddTicks(5948),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 12, 15, 30, 43, 716, DateTimeKind.Local).AddTicks(1582));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 12, 12, 29, 58, 184, DateTimeKind.Local).AddTicks(1229),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 12, 15, 30, 43, 709, DateTimeKind.Local).AddTicks(8202));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 12, 12, 29, 58, 170, DateTimeKind.Local).AddTicks(5661),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 12, 15, 30, 43, 689, DateTimeKind.Local).AddTicks(4358));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "KYCDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 12, 12, 29, 58, 168, DateTimeKind.Local).AddTicks(8761),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 12, 15, 30, 43, 687, DateTimeKind.Local).AddTicks(5968));

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
