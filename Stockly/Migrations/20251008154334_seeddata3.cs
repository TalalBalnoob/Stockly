using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Stockly.Migrations
{
    /// <inheritdoc />
    public partial class seeddata3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CreatedAt", "Customer_contact", "Customer_name", "Payment_method", "Payment_notes", "Status", "Total_amount" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000001", "Customer 1", "Mada", "Auto-generated test order", "approved", 60m },
                    { 2, new DateTime(2024, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000002", "Customer 2", "Cash", "Auto-generated test order", "approved", 70m },
                    { 3, new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000003", "Customer 3", "Mada", "Auto-generated test order", "approved", 80m },
                    { 4, new DateTime(2024, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000004", "Customer 4", "Cash", "Auto-generated test order", "approved", 90m },
                    { 5, new DateTime(2024, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000005", "Customer 5", "Mada", "Auto-generated test order", "approved", 100m },
                    { 6, new DateTime(2024, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000006", "Customer 6", "Cash", "Auto-generated test order", "approved", 110m },
                    { 7, new DateTime(2024, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000007", "Customer 7", "Mada", "Auto-generated test order", "approved", 120m },
                    { 8, new DateTime(2024, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000008", "Customer 8", "Cash", "Auto-generated test order", "approved", 130m },
                    { 9, new DateTime(2024, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000009", "Customer 9", "Mada", "Auto-generated test order", "approved", 140m },
                    { 10, new DateTime(2024, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000010", "Customer 10", "Cash", "Auto-generated test order", "approved", 150m },
                    { 11, new DateTime(2024, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000011", "Customer 11", "Mada", "Auto-generated test order", "approved", 160m },
                    { 12, new DateTime(2024, 11, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000012", "Customer 12", "Cash", "Auto-generated test order", "approved", 170m },
                    { 13, new DateTime(2024, 11, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000013", "Customer 13", "Mada", "Auto-generated test order", "approved", 180m },
                    { 14, new DateTime(2024, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000014", "Customer 14", "Cash", "Auto-generated test order", "approved", 190m },
                    { 15, new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000015", "Customer 15", "Mada", "Auto-generated test order", "approved", 200m },
                    { 16, new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000016", "Customer 16", "Cash", "Auto-generated test order", "approved", 210m },
                    { 17, new DateTime(2024, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000017", "Customer 17", "Mada", "Auto-generated test order", "approved", 220m },
                    { 18, new DateTime(2024, 11, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000018", "Customer 18", "Cash", "Auto-generated test order", "approved", 230m },
                    { 19, new DateTime(2024, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000019", "Customer 19", "Mada", "Auto-generated test order", "approved", 240m },
                    { 20, new DateTime(2024, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000020", "Customer 20", "Cash", "Auto-generated test order", "approved", 250m },
                    { 21, new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000021", "Customer 21", "Mada", "Auto-generated test order", "approved", 260m },
                    { 22, new DateTime(2024, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000022", "Customer 22", "Cash", "Auto-generated test order", "approved", 270m },
                    { 23, new DateTime(2024, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000023", "Customer 23", "Mada", "Auto-generated test order", "approved", 280m },
                    { 24, new DateTime(2024, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000024", "Customer 24", "Cash", "Auto-generated test order", "approved", 290m },
                    { 25, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000025", "Customer 25", "Mada", "Auto-generated test order", "approved", 300m },
                    { 26, new DateTime(2024, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000026", "Customer 26", "Cash", "Auto-generated test order", "approved", 310m },
                    { 27, new DateTime(2024, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000027", "Customer 27", "Mada", "Auto-generated test order", "approved", 320m },
                    { 28, new DateTime(2024, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000028", "Customer 28", "Cash", "Auto-generated test order", "approved", 330m },
                    { 29, new DateTime(2024, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000029", "Customer 29", "Mada", "Auto-generated test order", "approved", 340m },
                    { 30, new DateTime(2024, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000030", "Customer 30", "Cash", "Auto-generated test order", "approved", 350m },
                    { 31, new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000031", "Customer 31", "Mada", "Auto-generated test order", "approved", 360m },
                    { 32, new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000032", "Customer 32", "Cash", "Auto-generated test order", "approved", 370m },
                    { 33, new DateTime(2025, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000033", "Customer 33", "Mada", "Auto-generated test order", "approved", 380m },
                    { 34, new DateTime(2025, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000034", "Customer 34", "Cash", "Auto-generated test order", "approved", 390m },
                    { 35, new DateTime(2025, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000035", "Customer 35", "Mada", "Auto-generated test order", "approved", 400m },
                    { 36, new DateTime(2025, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000036", "Customer 36", "Cash", "Auto-generated test order", "approved", 410m },
                    { 37, new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000037", "Customer 37", "Mada", "Auto-generated test order", "approved", 420m },
                    { 38, new DateTime(2025, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000038", "Customer 38", "Cash", "Auto-generated test order", "approved", 430m },
                    { 39, new DateTime(2025, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000039", "Customer 39", "Mada", "Auto-generated test order", "approved", 440m },
                    { 40, new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000040", "Customer 40", "Cash", "Auto-generated test order", "approved", 50m },
                    { 41, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000041", "Customer 41", "Mada", "Auto-generated test order", "approved", 60m },
                    { 42, new DateTime(2025, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000042", "Customer 42", "Cash", "Auto-generated test order", "approved", 70m },
                    { 43, new DateTime(2025, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000043", "Customer 43", "Mada", "Auto-generated test order", "approved", 80m },
                    { 44, new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000044", "Customer 44", "Cash", "Auto-generated test order", "approved", 90m },
                    { 45, new DateTime(2025, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000045", "Customer 45", "Mada", "Auto-generated test order", "approved", 100m },
                    { 46, new DateTime(2025, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000046", "Customer 46", "Cash", "Auto-generated test order", "approved", 110m },
                    { 47, new DateTime(2025, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000047", "Customer 47", "Mada", "Auto-generated test order", "approved", 120m },
                    { 48, new DateTime(2025, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000048", "Customer 48", "Cash", "Auto-generated test order", "approved", 130m },
                    { 49, new DateTime(2025, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000049", "Customer 49", "Mada", "Auto-generated test order", "approved", 140m },
                    { 50, new DateTime(2025, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000050", "Customer 50", "Cash", "Auto-generated test order", "approved", 150m },
                    { 51, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000051", "Customer 51", "Mada", "Auto-generated test order", "approved", 160m },
                    { 52, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000052", "Customer 52", "Cash", "Auto-generated test order", "approved", 170m },
                    { 53, new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000053", "Customer 53", "Mada", "Auto-generated test order", "approved", 180m },
                    { 54, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000054", "Customer 54", "Cash", "Auto-generated test order", "approved", 190m },
                    { 55, new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000055", "Customer 55", "Mada", "Auto-generated test order", "approved", 200m },
                    { 56, new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000056", "Customer 56", "Cash", "Auto-generated test order", "approved", 210m },
                    { 57, new DateTime(2025, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000057", "Customer 57", "Mada", "Auto-generated test order", "approved", 220m },
                    { 58, new DateTime(2025, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000058", "Customer 58", "Cash", "Auto-generated test order", "approved", 230m },
                    { 59, new DateTime(2025, 3, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000059", "Customer 59", "Mada", "Auto-generated test order", "approved", 240m },
                    { 60, new DateTime(2025, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000060", "Customer 60", "Cash", "Auto-generated test order", "approved", 250m },
                    { 61, new DateTime(2025, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000061", "Customer 61", "Mada", "Auto-generated test order", "approved", 260m },
                    { 62, new DateTime(2025, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000062", "Customer 62", "Cash", "Auto-generated test order", "approved", 270m },
                    { 63, new DateTime(2025, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000063", "Customer 63", "Mada", "Auto-generated test order", "approved", 280m },
                    { 64, new DateTime(2025, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000064", "Customer 64", "Cash", "Auto-generated test order", "approved", 290m },
                    { 65, new DateTime(2025, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000065", "Customer 65", "Mada", "Auto-generated test order", "approved", 300m },
                    { 66, new DateTime(2025, 4, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000066", "Customer 66", "Cash", "Auto-generated test order", "approved", 310m },
                    { 67, new DateTime(2025, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000067", "Customer 67", "Mada", "Auto-generated test order", "approved", 320m },
                    { 68, new DateTime(2025, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000068", "Customer 68", "Cash", "Auto-generated test order", "approved", 330m },
                    { 69, new DateTime(2025, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000069", "Customer 69", "Mada", "Auto-generated test order", "approved", 340m },
                    { 70, new DateTime(2025, 4, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000070", "Customer 70", "Cash", "Auto-generated test order", "approved", 350m },
                    { 71, new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000071", "Customer 71", "Mada", "Auto-generated test order", "approved", 360m },
                    { 72, new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000072", "Customer 72", "Cash", "Auto-generated test order", "approved", 370m },
                    { 73, new DateTime(2025, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000073", "Customer 73", "Mada", "Auto-generated test order", "approved", 380m },
                    { 74, new DateTime(2025, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000074", "Customer 74", "Cash", "Auto-generated test order", "approved", 390m },
                    { 75, new DateTime(2025, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000075", "Customer 75", "Mada", "Auto-generated test order", "approved", 400m },
                    { 76, new DateTime(2025, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000076", "Customer 76", "Cash", "Auto-generated test order", "approved", 410m },
                    { 77, new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000077", "Customer 77", "Mada", "Auto-generated test order", "approved", 420m },
                    { 78, new DateTime(2025, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000078", "Customer 78", "Cash", "Auto-generated test order", "approved", 430m },
                    { 79, new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000079", "Customer 79", "Mada", "Auto-generated test order", "approved", 440m },
                    { 80, new DateTime(2025, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000080", "Customer 80", "Cash", "Auto-generated test order", "approved", 50m },
                    { 81, new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000081", "Customer 81", "Mada", "Auto-generated test order", "approved", 60m },
                    { 82, new DateTime(2025, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000082", "Customer 82", "Cash", "Auto-generated test order", "approved", 70m },
                    { 83, new DateTime(2025, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000083", "Customer 83", "Mada", "Auto-generated test order", "approved", 80m },
                    { 84, new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000084", "Customer 84", "Cash", "Auto-generated test order", "approved", 90m },
                    { 85, new DateTime(2025, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000085", "Customer 85", "Mada", "Auto-generated test order", "approved", 100m },
                    { 86, new DateTime(2025, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000086", "Customer 86", "Cash", "Auto-generated test order", "approved", 110m },
                    { 87, new DateTime(2025, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000087", "Customer 87", "Mada", "Auto-generated test order", "approved", 120m },
                    { 88, new DateTime(2025, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000088", "Customer 88", "Cash", "Auto-generated test order", "approved", 130m },
                    { 89, new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000089", "Customer 89", "Mada", "Auto-generated test order", "approved", 140m },
                    { 90, new DateTime(2025, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000090", "Customer 90", "Cash", "Auto-generated test order", "approved", 150m },
                    { 91, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000091", "Customer 91", "Mada", "Auto-generated test order", "approved", 160m },
                    { 92, new DateTime(2025, 7, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000092", "Customer 92", "Cash", "Auto-generated test order", "approved", 170m },
                    { 93, new DateTime(2025, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000093", "Customer 93", "Mada", "Auto-generated test order", "approved", 180m },
                    { 94, new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000094", "Customer 94", "Cash", "Auto-generated test order", "approved", 190m },
                    { 95, new DateTime(2025, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000095", "Customer 95", "Mada", "Auto-generated test order", "approved", 200m },
                    { 96, new DateTime(2025, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000096", "Customer 96", "Cash", "Auto-generated test order", "approved", 210m },
                    { 97, new DateTime(2025, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000097", "Customer 97", "Mada", "Auto-generated test order", "approved", 220m },
                    { 98, new DateTime(2025, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000098", "Customer 98", "Cash", "Auto-generated test order", "approved", 230m },
                    { 99, new DateTime(2025, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000099", "Customer 99", "Mada", "Auto-generated test order", "approved", 240m },
                    { 100, new DateTime(2025, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "05000100", "Customer 100", "Cash", "Auto-generated test order", "approved", 250m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 100);
        }
    }
}
