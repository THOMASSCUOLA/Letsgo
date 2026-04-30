using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Letsgo.Migrations
{
    /// <inheritdoc />
    public partial class AggiornamentoPreferiti : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DestinazioneSalvata",
                table: "DestinazioneSalvata");

            migrationBuilder.RenameTable(
                name: "DestinazioneSalvata",
                newName: "DestinazioniSalvate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DestinazioniSalvate",
                table: "DestinazioniSalvate",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DestinazioniSalvate",
                table: "DestinazioniSalvate");

            migrationBuilder.RenameTable(
                name: "DestinazioniSalvate",
                newName: "DestinazioneSalvata");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DestinazioneSalvata",
                table: "DestinazioneSalvata",
                column: "Id");
        }
    }
}
