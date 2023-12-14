using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class AddNewColumnsInUserDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
 
            migrationBuilder.AddColumn<DateTime>(
                name: "LastTimeLoggedIn",
                table: "UserDetail",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerificationCode",
                table: "UserDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerificationExpiry",
                table: "UserDetail",
                type: "nvarchar(max)",
                nullable: true);          
        
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropColumn(
                name: "LastTimeLoggedIn",
                table: "UserDetail");

            migrationBuilder.DropColumn(
                name: "VerificationCode",
                table: "UserDetail");

            migrationBuilder.DropColumn(
                name: "VerificationExpiry",
                table: "UserDetail");

          
        }
    }
}
