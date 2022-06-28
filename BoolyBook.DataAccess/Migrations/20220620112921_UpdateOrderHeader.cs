using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoolyBook.DataAccess.Migrations
{
    public partial class UpdateOrderHeader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "orderHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "orderHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "orderHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "orderHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "orderHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StreetAddress",
                table: "orderHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "orderHeaders");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "orderHeaders");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "orderHeaders");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "orderHeaders");

            migrationBuilder.DropColumn(
                name: "State",
                table: "orderHeaders");

            migrationBuilder.DropColumn(
                name: "StreetAddress",
                table: "orderHeaders");
        }
    }
}
