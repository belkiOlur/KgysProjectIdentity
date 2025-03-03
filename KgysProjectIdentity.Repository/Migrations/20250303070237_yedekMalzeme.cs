using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KgysProjectIdentity.Repository.Migrations
{
    /// <inheritdoc />
    public partial class yedekMalzeme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpareMaterialDefinations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpareMaterialName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpareMaterialCode = table.Column<int>(type: "int", nullable: false),
                    Sorted = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpareMaterialDefinations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpareMaterialDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpareMaterialDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpareMaterials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpareMaterialId = table.Column<int>(type: "int", nullable: false),
                    Properties = table.Column<int>(type: "int", nullable: false),
                    MaterialDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pieces = table.Column<int>(type: "int", nullable: false),
                    Measurement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descriptions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestOrGet = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EBYSNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhoWantIt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpareMaterials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpareMaterialsMeasurement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Measurement = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpareMaterialsMeasurement", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpareMaterialDefinations");

            migrationBuilder.DropTable(
                name: "SpareMaterialDetails");

            migrationBuilder.DropTable(
                name: "SpareMaterials");

            migrationBuilder.DropTable(
                name: "SpareMaterialsMeasurement");
        }
    }
}
