using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class UpdateUserDetailTableForCreatedBY : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "UserDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.DropIndex(
                name: "IX_UserDetail_SecurityQuestionsId",
                table: "UserDetail");

            migrationBuilder.DropColumn(
                name: "SecurityQuestionsId",
                table: "UserDetail");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "UserDetail");


            migrationBuilder.CreateIndex(
                name: "IX_UserDetail_SecurityQuestionsId",
                table: "UserDetail",
                column: "SecurityQuestionsId");

            migrationBuilder.AddColumn<int>(
                name: "SecurityQuestionsId",
                table: "UserDetail",
                type: "int",
                nullable: true);


        }
    }
}
