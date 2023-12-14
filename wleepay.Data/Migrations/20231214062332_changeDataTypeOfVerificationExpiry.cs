using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class changeDataTypeOfVerificationExpiry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AlterColumn<DateTime>(
                name: "VerificationExpiry",
                table: "UserDetail",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
          
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.AlterColumn<string>(
                name: "VerificationExpiry",
                table: "UserDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

           
        }
    }
}
