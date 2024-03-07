using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KgysProjectIdentity.Repository.Migrations
{
    /// <inheritdoc />
    public partial class IpPhoneProjectAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IpPhoneProject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Campus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Central = table.Column<bool>(type: "bit", nullable: false),
                    CentralBrand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PoeSwitch = table.Column<bool>(type: "bit", nullable: false),
                    CableIsTrue = table.Column<bool>(type: "bit", nullable: false),
                    PhonePieces = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    UpdatedPersonel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IpPhoneProject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriorityForPhone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PriorityName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriorityForPhone", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IpPhoneProject");

            migrationBuilder.DropTable(
                name: "PriorityForPhone");
        }
    }
}
