using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class UpdateUserDetailTableForDuplicateSecurityQ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<int>(
                name: "SecurityQuestionsId",
                table: "UserDetail",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserDetail_SecurityQuestionsId",
                table: "UserDetail",
                column: "SecurityQuestionsId");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropColumn(
             name: "SecurityQuestionsId",
             table: "UserDetail");

            migrationBuilder.DropIndex(
             name: "IX_UserDetail_SecurityQuestionsId",
             table: "UserDetail");

        }
    }
}
