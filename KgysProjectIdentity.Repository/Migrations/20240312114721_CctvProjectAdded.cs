using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KgysProjectIdentity.Repository.Migrations
{
    /// <inheritdoc />
    public partial class CctvProjectAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CctvProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DetailId = table.Column<int>(type: "int", nullable: false),
                    FloorOfSystem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductModel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FloorOfProduct = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlannedPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductPieces = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CctvProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CctvProjectDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectDistrict = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExProjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectReason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CctvProjectDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CctvProjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectYear = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CctvProjects", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CctvProducts");

            migrationBuilder.DropTable(
                name: "CctvProjectDetail");

            migrationBuilder.DropTable(
                name: "CctvProjects");
        }
    }
}
