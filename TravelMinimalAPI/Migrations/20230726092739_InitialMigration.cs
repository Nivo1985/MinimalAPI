using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelMinimalAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Destinations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActivityDestination",
                columns: table => new
                {
                    ActivitiesId = table.Column<int>(type: "INTEGER", nullable: false),
                    DestinationsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityDestination", x => new { x.ActivitiesId, x.DestinationsId });
                    table.ForeignKey(
                        name: "FK_ActivityDestination_Activities_ActivitiesId",
                        column: x => x.ActivitiesId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityDestination_Destinations_DestinationsId",
                        column: x => x.DestinationsId,
                        principalTable: "Destinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Activities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Nice weather" },
                    { 2, "Good food" },
                    { 3, "Castles" },
                    { 4, "Nice People" },
                    { 5, "Good beer" },
                    { 6, "Good wine" }
                });

            migrationBuilder.InsertData(
                table: "Destinations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Greece" },
                    { 2, "Spain" },
                    { 3, "Germany" },
                    { 4, "Poland" }
                });

            migrationBuilder.InsertData(
                table: "ActivityDestination",
                columns: new[] { "ActivitiesId", "DestinationsId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 4 },
                    { 3, 2 },
                    { 3, 3 },
                    { 3, 4 },
                    { 4, 1 },
                    { 4, 2 },
                    { 4, 4 },
                    { 5, 3 },
                    { 5, 4 },
                    { 6, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityDestination_DestinationsId",
                table: "ActivityDestination",
                column: "DestinationsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityDestination");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Destinations");
        }
    }
}
