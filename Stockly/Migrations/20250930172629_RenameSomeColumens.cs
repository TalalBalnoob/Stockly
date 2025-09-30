using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stockly.Migrations
{
    /// <inheritdoc />
    public partial class RenameSomeColumens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Customer_Name",
                table: "Orders",
                newName: "Customer_name");

            migrationBuilder.RenameColumn(
                name: "Customer_Contact",
                table: "Orders",
                newName: "Customer_contact");

            migrationBuilder.RenameColumn(
                name: "PaymentNotes",
                table: "Orders",
                newName: "Payment_notes");

            migrationBuilder.RenameColumn(
                name: "PaymentMethod",
                table: "Orders",
                newName: "Payment_method");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Customer_name",
                table: "Orders",
                newName: "Customer_Name");

            migrationBuilder.RenameColumn(
                name: "Customer_contact",
                table: "Orders",
                newName: "Customer_Contact");

            migrationBuilder.RenameColumn(
                name: "Payment_notes",
                table: "Orders",
                newName: "PaymentNotes");

            migrationBuilder.RenameColumn(
                name: "Payment_method",
                table: "Orders",
                newName: "PaymentMethod");
        }
    }
}
