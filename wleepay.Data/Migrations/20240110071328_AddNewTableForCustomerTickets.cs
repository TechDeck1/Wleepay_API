using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class AddNewTableForCustomerTickets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "IssueType",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "IssueType",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "IssueType",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CustomerTickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    IssueTypeId = table.Column<int>(type: "int", nullable: false),
                    CustomerContact = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerTickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerTickets_IssueType_IssueTypeId",
                        column: x => x.IssueTypeId,
                        principalTable: "IssueType",
                        principalColumn: "Id");
                });

         
            migrationBuilder.UpdateData(
                table: "IssueType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Signup/KYC");

            migrationBuilder.UpdateData(
                table: "IssueType",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Agent Commission");

            migrationBuilder.UpdateData(
                table: "IssueType",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "App Not Working");

            migrationBuilder.UpdateData(
                table: "IssueType",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "Topup");

            migrationBuilder.UpdateData(
                table: "IssueType",
                keyColumn: "Id",
                keyValue: 5,
                column: "Description",
                value: "Cashout");

            migrationBuilder.UpdateData(
                table: "IssueType",
                keyColumn: "Id",
                keyValue: 6,
                column: "Description",
                value: "Payment");

            migrationBuilder.UpdateData(
                table: "IssueType",
                keyColumn: "Id",
                keyValue: 7,
                column: "Description",
                value: "Donation");

            migrationBuilder.UpdateData(
                table: "IssueType",
                keyColumn: "Id",
                keyValue: 8,
                column: "Description",
                value: "Reversal");

            migrationBuilder.UpdateData(
                table: "IssueType",
                keyColumn: "Id",
                keyValue: 9,
                column: "Description",
                value: "Bill Payment");

            migrationBuilder.UpdateData(
                table: "IssueType",
                keyColumn: "Id",
                keyValue: 10,
                column: "Description",
                value: "Phone Topup");

            migrationBuilder.UpdateData(
                table: "IssueType",
                keyColumn: "Id",
                keyValue: 11,
                column: "Description",
                value: "Crypto Wallet");

            migrationBuilder.UpdateData(
                table: "IssueType",
                keyColumn: "Id",
                keyValue: 12,
                column: "Description",
                value: "Agent Wallet");

            migrationBuilder.UpdateData(
                table: "IssueType",
                keyColumn: "Id",
                keyValue: 13,
                column: "Description",
                value: "Payment reversal");

            migrationBuilder.UpdateData(
                table: "IssueType",
                keyColumn: "Id",
                keyValue: 14,
                column: "Description",
                value: "Lost/Stolen Pin");

            migrationBuilder.UpdateData(
                table: "IssueType",
                keyColumn: "Id",
                keyValue: 15,
                column: "Description",
                value: "Identity Theft");

            migrationBuilder.UpdateData(
                table: "IssueType",
                keyColumn: "Id",
                keyValue: 16,
                column: "Description",
                value: "Wallet Fraud/Theft");

            migrationBuilder.UpdateData(
                table: "IssueType",
                keyColumn: "Id",
                keyValue: 17,
                column: "Description",
                value: "Report System Outage");

            migrationBuilder.UpdateData(
                table: "IssueType",
                keyColumn: "Id",
                keyValue: 18,
                column: "Description",
                value: "Report System Outage");

           
            migrationBuilder.CreateIndex(
                name: "IX_CustomerTickets_IssueTypeId",
                table: "CustomerTickets",
                column: "IssueTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerTickets");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "IssueType");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "IssueType");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "IssueType");

        }
    }
}
