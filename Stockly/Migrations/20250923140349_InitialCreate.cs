using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stockly.Migrations {
	/// <inheritdoc />
	public partial class InitialCreate : Migration {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.CreateTable(
				name: "Orders",
				columns: table => new {
					Id = table.Column<int>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					Customer_Name = table.Column<string>(type: "TEXT", nullable: true),
					Customer_Contact = table.Column<string>(type: "TEXT", nullable: true),
					Status = table.Column<string>(type: "TEXT", nullable: false),
					Total_amount = table.Column<decimal>(type: "TEXT", nullable: false),
					PaymentMethod = table.Column<string>(type: "TEXT", nullable: true),
					PaymentNotes = table.Column<string>(type: "TEXT", nullable: true),
					CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_Orders", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Products",
				columns: table => new {
					Id = table.Column<int>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					Name = table.Column<string>(type: "TEXT", nullable: false),
					Description = table.Column<string>(type: "TEXT", nullable: true),
					Storage_Note = table.Column<string>(type: "TEXT", nullable: true),
					Price = table.Column<decimal>(type: "TEXT", nullable: false),
					Quantity = table.Column<int>(type: "INTEGER", nullable: false),
					IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
					CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_Products", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "OrderItems",
				columns: table => new {
					Id = table.Column<int>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					OrderId = table.Column<int>(type: "INTEGER", nullable: false),
					ProductId = table.Column<int>(type: "INTEGER", nullable: false),
					Quantity = table.Column<int>(type: "INTEGER", nullable: false),
					Price = table.Column<decimal>(type: "TEXT", nullable: false),
					Total = table.Column<decimal>(type: "TEXT", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_OrderItems", x => x.Id);
					table.ForeignKey(
						name: "FK_OrderItems_Orders_OrderId",
						column: x => x.OrderId,
						principalTable: "Orders",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_OrderItems_Products_ProductId",
						column: x => x.ProductId,
						principalTable: "Products",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "StockAdjustment",
				columns: table => new {
					Id = table.Column<int>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					Product_Id = table.Column<int>(type: "INTEGER", nullable: false),
					Related_Order_Id = table.Column<int>(type: "INTEGER", nullable: true),
					Change = table.Column<int>(type: "INTEGER", nullable: false),
					Reason = table.Column<string>(type: "TEXT", nullable: true),
					CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_StockAdjustment", x => x.Id);
					table.ForeignKey(
						name: "FK_StockAdjustment_Orders_Related_Order_Id",
						column: x => x.Related_Order_Id,
						principalTable: "Orders",
						principalColumn: "Id");
					table.ForeignKey(
						name: "FK_StockAdjustment_Products_Product_Id",
						column: x => x.Product_Id,
						principalTable: "Products",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_OrderItems_OrderId",
				table: "OrderItems",
				column: "OrderId");

			migrationBuilder.CreateIndex(
				name: "IX_OrderItems_ProductId",
				table: "OrderItems",
				column: "ProductId");

			migrationBuilder.CreateIndex(
				name: "IX_StockAdjustment_Product_Id",
				table: "StockAdjustment",
				column: "Product_Id");

			migrationBuilder.CreateIndex(
				name: "IX_StockAdjustment_Related_Order_Id",
				table: "StockAdjustment",
				column: "Related_Order_Id");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropTable(
				name: "OrderItems");

			migrationBuilder.DropTable(
				name: "StockAdjustment");

			migrationBuilder.DropTable(
				name: "Orders");

			migrationBuilder.DropTable(
				name: "Products");
		}
	}
}
