using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KgysProjectIdentity.Repository.Migrations
{
    /// <inheritdoc />
    public partial class CctvProjectStatusAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "CctvProjectDetail",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "CctvProjectDetail");
        }
    }
}
