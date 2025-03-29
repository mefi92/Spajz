using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpajzManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStorageRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StorageId",
                table: "Items",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Storages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    HouseholdId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Storages_Households_HouseholdId",
                        column: x => x.HouseholdId,
                        principalTable: "Households",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_StorageId",
                table: "Items",
                column: "StorageId");

            migrationBuilder.CreateIndex(
                name: "IX_Storages_HouseholdId",
                table: "Storages",
                column: "HouseholdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Storages_StorageId",
                table: "Items",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Storages_StorageId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "Storages");

            migrationBuilder.DropIndex(
                name: "IX_Items_StorageId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "StorageId",
                table: "Items");
        }
    }
}
