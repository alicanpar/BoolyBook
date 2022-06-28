using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoolyBook.DataAccess.Migrations
{
    public partial class UpdateOrderHeaderPaymenToPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymenDueDate",
                table: "orderHeaders",
                newName: "PaymentDueDate");

            migrationBuilder.RenameColumn(
                name: "PaymenDate",
                table: "orderHeaders",
                newName: "PaymentDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentDueDate",
                table: "orderHeaders",
                newName: "PaymenDueDate");

            migrationBuilder.RenameColumn(
                name: "PaymentDate",
                table: "orderHeaders",
                newName: "PaymenDate");
        }
    }
}
