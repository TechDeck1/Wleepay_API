using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class seedDataInSecurityQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
                table: "SecurityQuestions",
                columns: new[] { "Id", "Question" },
                values: new object[,]
                {
                    { 1, "What is your mother’s maiden name" },
                    { 2, "Name of primary school attended" },
                    { 3, "Who is your favorite musician" }
                });

        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SecurityQuestions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SecurityQuestions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SecurityQuestions",
                keyColumn: "Id",
                keyValue: 3);

        }
    }
}
