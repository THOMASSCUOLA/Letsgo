using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Letsgo.Migrations
{
    /// <inheritdoc />
    public partial class AggiuntoNuovoCampo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DurataTotaleMinuti",
                table: "DestinazioniSalvate",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurataTotaleMinuti",
                table: "DestinazioniSalvate");
        }
    }
}
