using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KgysProjectIdentity.Repository.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPlanRequestAndTelekomndecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ProjectCoordinateY",
                table: "Products",
                type: "decimal(8,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ProjectCoordinateX",
                table: "Products",
                type: "decimal(8,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ProjectCoordinateY",
                table: "KgysRequest",
                type: "decimal(8,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ProjectCoordinateX",
                table: "KgysRequest",
                type: "decimal(8,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ProjectCoordinateY",
                table: "KgysPlanned",
                type: "decimal(8,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ProjectCoordinateX",
                table: "KgysPlanned",
                type: "decimal(8,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ProjectCoordinateY",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ProjectCoordinateX",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ProjectCoordinateY",
                table: "KgysRequest",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ProjectCoordinateX",
                table: "KgysRequest",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ProjectCoordinateY",
                table: "KgysPlanned",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ProjectCoordinateX",
                table: "KgysPlanned",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,6)");
        }
    }
}
