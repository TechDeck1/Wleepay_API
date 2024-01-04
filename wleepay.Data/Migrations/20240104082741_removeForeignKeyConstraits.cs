using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class removeForeignKeyConstraits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDetail_SecurityQuestions_SecurityQuestionId",
                table: "UserDetail");

            migrationBuilder.DropIndex(
                name: "IX_UserDetail_SecurityQuestionId",
                table: "UserDetail");


            migrationBuilder.AlterColumn<int>(
                name: "SecurityQuestionsId",
                table: "UserDetail",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.AlterColumn<int>(
                name: "SecurityQuestionsId",
                table: "UserDetail",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

         
            migrationBuilder.CreateIndex(
                name: "IX_UserDetail_SecurityQuestionId",
                table: "UserDetail",
                column: "SecurityQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDetail_SecurityQuestions_SecurityQuestionId",
                table: "UserDetail",
                column: "SecurityQuestionId",
                principalTable: "SecurityQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
