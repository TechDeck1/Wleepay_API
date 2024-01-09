using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class AddNewEntityForIssueType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "IssueType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueType", x => x.Id);
                });


            migrationBuilder.InsertData(
                table: "IssueType",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Signup/KYC" },
                    { 18, "Other issue (describe)" },
                    { 17, "Report System Outage" },
                    { 16, "Wallet Fraud/Theft" },
                    { 15, "Identity Theft" },
                    { 14, "Lost/Stolen Pin" },
                    { 12, "Agent Wallet" },
                    { 11, "Crypto Wallet" },
                    { 13, "Payment reversal" },
                    { 9, "Bill Payment" },
                    { 8, "Reversal" },
                    { 7, "Donation" },
                    { 6, "Payment" },
                    { 5, "Cashout" },
                    { 4, "Topup" },
                    { 3, "App Not Working" },
                    { 2, "Agent Commission" },
                    { 10, "Phone Topup" }
                });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IssueType");

        }
    }
}
