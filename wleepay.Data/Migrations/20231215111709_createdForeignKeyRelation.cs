using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class createdForeignKeyRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateIndex(
                name: "IX_UserDetail_SecurityQuestionId",
                table: "UserDetail",
                column: "SecurityQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDetail_SecurityQuestions_SecurityQuestionId",
                table: "UserDetail",
                column: "SecurityQuestionId",
                principalTable: "SecurityQuestions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDetail_SecurityQuestions_SecurityQuestionId",
                table: "UserDetail");

            migrationBuilder.DropIndex(
                name: "IX_UserDetail_SecurityQuestionId",
                table: "UserDetail");

        }
    }
}
