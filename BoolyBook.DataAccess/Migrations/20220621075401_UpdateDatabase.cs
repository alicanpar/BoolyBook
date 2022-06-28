using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoolyBook.DataAccess.Migrations
{
    public partial class UpdateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymenStatus",
                table: "orderHeaders",
                newName: "PaymentStatus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentStatus",
                table: "orderHeaders",
                newName: "PaymenStatus");
        }
    }
}
