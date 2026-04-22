using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Letsgo.Data.Migrations
{
    /// <inheritdoc />
    public partial class AggiuntaDestinazioniSalvate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DestinazioneSalvata",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUtente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AeroportoPartenza = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AreaGeografica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CittaDestinazione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AeroportoDestinazione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataAndata = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataRitorno = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Prezzo = table.Column<int>(type: "int", nullable: true),
                    Temperatura = table.Column<double>(type: "float", nullable: true),
                    DescrizioneMeteo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataSalvataggio = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DestinazioneSalvata", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DestinazioneSalvata");
        }
    }
}
