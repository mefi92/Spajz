using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SpajzManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedStorages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Storages",
                columns: new[] { "Id", "HouseholdId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Hűtőszekrény" },
                    { 2, 1, "Kamra" },
                    { 3, 2, "Fagyasztó" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Storages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Storages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Storages",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
