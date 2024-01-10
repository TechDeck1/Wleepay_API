using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wleepay.Data.Migrations
{
    public partial class RemoveDuplicateCoulmn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_CustomerTickets_CustomerTicketsId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_CustomerTicketsId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "CustomerTicketsId",
                table: "Notes");
          
            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Notes",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_CustomerTicketId",
                table: "Notes",
                column: "CustomerTicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_CustomerTickets_CustomerTicketId",
                table: "Notes",
                column: "CustomerTicketId",
                principalTable: "CustomerTickets",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_CustomerTickets_CustomerTicketId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_CustomerTicketId",
                table: "Notes");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AddColumn<int>(
                name: "CustomerTicketsId",
                table: "Notes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_CustomerTicketsId",
                table: "Notes",
                column: "CustomerTicketsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_CustomerTickets_CustomerTicketsId",
                table: "Notes",
                column: "CustomerTicketsId",
                principalTable: "CustomerTickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
