using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DSU_Grupp3.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeSoDTOs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeSoDTOs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DesoCodeInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RegionCode = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Municipality = table.Column<string>(type: "TEXT", nullable: false),
                    MunicipalityCode = table.Column<int>(type: "INTEGER", nullable: false),
                    Deso = table.Column<string>(type: "TEXT", nullable: false),
                    DesoName = table.Column<string>(type: "TEXT", nullable: false),
                    TotalPopulation = table.Column<int>(type: "INTEGER", nullable: false),
                    PopulationMales = table.Column<int>(type: "INTEGER", nullable: false),
                    PopulationFemales = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalVaccinated = table.Column<int>(type: "INTEGER", nullable: false),
                    VaccinatedFemales = table.Column<int>(type: "INTEGER", nullable: false),
                    VaccinatedMales = table.Column<int>(type: "INTEGER", nullable: false),
                    VaccinatedOtherGender = table.Column<int>(type: "INTEGER", nullable: false),
                    DeSoDTOId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesoCodeInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DesoCodeInformation_DeSoDTOs_DeSoDTOId",
                        column: x => x.DeSoDTOId,
                        principalTable: "DeSoDTOs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DesoCodeInformation_DeSoDTOId",
                table: "DesoCodeInformation",
                column: "DeSoDTOId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DesoCodeInformation");

            migrationBuilder.DropTable(
                name: "DeSoDTOs");
        }
    }
}
