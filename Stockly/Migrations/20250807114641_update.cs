using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stockly.Migrations {
	/// <inheritdoc />
	public partial class update : Migration {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.AlterColumn<int>(
				name: "Id",
				table: "StockAdjustment",
				type: "int",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "varchar(255)")
				.Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
				.OldAnnotation("MySql:CharSet", "utf8mb4");

			migrationBuilder.AddColumn<int>(
				name: "Related_Order_Id",
				table: "StockAdjustment",
				type: "int",
				nullable: true);

			migrationBuilder.AlterColumn<bool>(
				name: "IsActive",
				table: "Products",
				type: "tinyint(1)",
				nullable: true,
				oldClrType: typeof(bool),
				oldType: "tinyint(1)");

			migrationBuilder.AlterColumn<string>(
				name: "Customer_Name",
				table: "Orders",
				type: "longtext",
				nullable: true,
				oldClrType: typeof(string),
				oldType: "longtext")
				.Annotation("MySql:CharSet", "utf8mb4")
				.OldAnnotation("MySql:CharSet", "utf8mb4");

			migrationBuilder.AlterColumn<string>(
				name: "Customer_Contact",
				table: "Orders",
				type: "longtext",
				nullable: true,
				oldClrType: typeof(string),
				oldType: "longtext")
				.Annotation("MySql:CharSet", "utf8mb4")
				.OldAnnotation("MySql:CharSet", "utf8mb4");

			migrationBuilder.CreateIndex(
				name: "IX_StockAdjustment_Related_Order_Id",
				table: "StockAdjustment",
				column: "Related_Order_Id");

			migrationBuilder.AddForeignKey(
				name: "FK_StockAdjustment_Orders_Related_Order_Id",
				table: "StockAdjustment",
				column: "Related_Order_Id",
				principalTable: "Orders",
				principalColumn: "Id");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropForeignKey(
				name: "FK_StockAdjustment_Orders_Related_Order_Id",
				table: "StockAdjustment");

			migrationBuilder.DropIndex(
				name: "IX_StockAdjustment_Related_Order_Id",
				table: "StockAdjustment");

			migrationBuilder.DropColumn(
				name: "Related_Order_Id",
				table: "StockAdjustment");

			migrationBuilder.AlterColumn<string>(
				name: "Id",
				table: "StockAdjustment",
				type: "varchar(255)",
				nullable: false,
				oldClrType: typeof(int),
				oldType: "int")
				.Annotation("MySql:CharSet", "utf8mb4")
				.OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

			migrationBuilder.AlterColumn<bool>(
				name: "IsActive",
				table: "Products",
				type: "tinyint(1)",
				nullable: false,
				defaultValue: false,
				oldClrType: typeof(bool),
				oldType: "tinyint(1)",
				oldNullable: true);

			migrationBuilder.UpdateData(
				table: "Orders",
				keyColumn: "Customer_Name",
				keyValue: null,
				column: "Customer_Name",
				value: "");

			migrationBuilder.AlterColumn<string>(
				name: "Customer_Name",
				table: "Orders",
				type: "longtext",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "longtext",
				oldNullable: true)
				.Annotation("MySql:CharSet", "utf8mb4")
				.OldAnnotation("MySql:CharSet", "utf8mb4");

			migrationBuilder.UpdateData(
				table: "Orders",
				keyColumn: "Customer_Contact",
				keyValue: null,
				column: "Customer_Contact",
				value: "");

			migrationBuilder.AlterColumn<string>(
				name: "Customer_Contact",
				table: "Orders",
				type: "longtext",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "longtext",
				oldNullable: true)
				.Annotation("MySql:CharSet", "utf8mb4")
				.OldAnnotation("MySql:CharSet", "utf8mb4");
		}
	}
}
