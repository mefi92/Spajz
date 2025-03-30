using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SpajzManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class StorageTypeInsteadOfStrings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Storages");

            migrationBuilder.AddColumn<int>(
                name: "StorageTypeId",
                table: "Storages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StorageTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "StorageTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Ideiglenes tároló" },
                    { 2, "Hűtő" },
                    { 3, "Fagyasztó" },
                    { 4, "Tároló " },
                    { 5, "Spájz" },
                    { 6, "Pince" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Storages_StorageTypeId",
                table: "Storages",
                column: "StorageTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Storages_StorageTypes_StorageTypeId",
                table: "Storages",
                column: "StorageTypeId",
                principalTable: "StorageTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Storages_StorageTypes_StorageTypeId",
                table: "Storages");

            migrationBuilder.DropTable(
                name: "StorageTypes");

            migrationBuilder.DropIndex(
                name: "IX_Storages_StorageTypeId",
                table: "Storages");

            migrationBuilder.DropColumn(
                name: "StorageTypeId",
                table: "Storages");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Storages",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Storages",
                columns: new[] { "Id", "Description", "HouseholdId", "Name" },
                values: new object[,]
                {
                    { 1, null, 1, "Hűtőszekrény" },
                    { 2, null, 1, "Kamra" },
                    { 3, null, 2, "Fagyasztó" }
                });
        }
    }
}
