using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class AddedNewCoulmnsToUserDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDetail_SecurityQuestions_SecurityQuestionId",
                table: "UserDetail");

            migrationBuilder.DropIndex(
                name: "IX_UserDetail_SecurityQuestionId",
                table: "UserDetail");

            migrationBuilder.DropColumn(
                name: "LastTimeLoggedIn",
                table: "UserDetail");         

            migrationBuilder.AddColumn<DateTime>(
                name: "HireDate",
                table: "UserDetail",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SecurityQuestionsId",
                table: "UserDetail",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WleePhoneNumber",
                table: "UserDetail",
                type: "nvarchar(max)",
                nullable: true);

            
            migrationBuilder.InsertData(
                table: "DocumentType",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "Passport" });

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserDetail_SecurityQuestions_SecurityQuestionsId",
            //    table: "UserDetail");

            //migrationBuilder.DropIndex(
            //    name: "IX_UserDetail_SecurityQuestionsId",
            //    table: "UserDetail");

            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: "F58A3546-E959-4B1D-A40C-518EAF9D9788");

            migrationBuilder.DeleteData(
                table: "DocumentType",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "HireDate",
                table: "UserDetail");

            migrationBuilder.DropColumn(
                name: "SecurityQuestionsId",
                table: "UserDetail");

            migrationBuilder.DropColumn(
                name: "WleePhoneNumber",
                table: "UserDetail");

            
            migrationBuilder.AddColumn<DateTime>(
                name: "LastTimeLoggedIn",
                table: "UserDetail",
                type: "datetime2",
                nullable: true);

           
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
    }
}
