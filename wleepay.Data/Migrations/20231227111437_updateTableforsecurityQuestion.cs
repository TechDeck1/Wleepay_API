using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class updateTableforsecurityQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.AddColumn<int>(
                name: "SecurityQuestionsId",
                table: "UserDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserDetail_SecurityQuestionsId",
                table: "UserDetail",
                column: "SecurityQuestionsId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserDetail_SecurityQuestions_SecurityQuestionsId",
            //    table: "UserDetail",
            //    column: "SecurityQuestionsId",
            //    principalTable: "SecurityQuestions",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserDetail_SecurityQuestions_SecurityQuestionsId",
            //    table: "UserDetail");


            migrationBuilder.DropColumn(
                name: "SecurityQuestionId",
                table: "UserDetail");

            migrationBuilder.DropColumn(
                name: "SecurityQuestionsId",
                table: "UserDetail");



        }
    }
}
