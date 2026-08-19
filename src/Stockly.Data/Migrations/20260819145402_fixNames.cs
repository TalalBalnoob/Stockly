using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stockly.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerContect",
                table: "Orders",
                newName: "CustomerContact");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerContact",
                table: "Orders",
                newName: "CustomerContect");
        }
    }
}
