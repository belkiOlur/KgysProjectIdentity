﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KgysProjectIdentity.Repository.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPlanRequestAndTelekomn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ProjectCoordinateX",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProjectCoordinateY",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProjectCoordinateX",
                table: "KgysRequest",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProjectCoordinateY",
                table: "KgysRequest",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProjectCoordinateX",
                table: "KgysPlanned",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProjectCoordinateY",
                table: "KgysPlanned",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectCoordinateX",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProjectCoordinateY",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProjectCoordinateX",
                table: "KgysRequest");

            migrationBuilder.DropColumn(
                name: "ProjectCoordinateY",
                table: "KgysRequest");

            migrationBuilder.DropColumn(
                name: "ProjectCoordinateX",
                table: "KgysPlanned");

            migrationBuilder.DropColumn(
                name: "ProjectCoordinateY",
                table: "KgysPlanned");
        }
    }
}
