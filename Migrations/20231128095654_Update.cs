using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LosCasaAngular.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Listings",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Area",
                table: "Listings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rooms",
                table: "Listings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Toilets",
                table: "Listings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "Rooms",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "Toilets",
                table: "Listings");
        }
    }
}
