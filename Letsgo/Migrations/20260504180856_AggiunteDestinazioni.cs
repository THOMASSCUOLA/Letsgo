using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Letsgo.Migrations
{
    /// <inheritdoc />
    public partial class AggiunteDestinazioni : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DestinazioniAree",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Citta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aeroporto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AreaGeografica = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DestinazioniAree", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DestinazioniAree",
                columns: new[] { "Id", "Aeroporto", "AreaGeografica", "Citta" },
                values: new object[,]
                {
                    { 1, "FCO", "Europa", "Roma" },
                    { 2, "MXP", "Europa", "Milano" },
                    { 3, "CDG", "Europa", "Parigi" },
                    { 4, "LHR", "Europa", "Londra" },
                    { 5, "MAD", "Europa", "Madrid" },
                    { 6, "BER", "Europa", "Berlino" },
                    { 7, "AMS", "Europa", "Amsterdam" },
                    { 8, "VIE", "Europa", "Vienna" },
                    { 9, "LIS", "Europa", "Lisbona" },
                    { 10, "ATH", "Europa", "Atene" },
                    { 11, "HND", "Asia", "Tokyo" },
                    { 12, "PEK", "Asia", "Pechino" },
                    { 13, "BKK", "Asia", "Bangkok" },
                    { 14, "SIN", "Asia", "Singapore" },
                    { 15, "DXB", "Asia", "Dubai" },
                    { 16, "ICN", "Asia", "Seoul" },
                    { 17, "BOM", "Asia", "Mumbai" },
                    { 18, "HKG", "Asia", "Hong Kong" },
                    { 19, "KUL", "Asia", "Kuala Lumpur" },
                    { 20, "TPE", "Asia", "Taipei" },
                    { 21, "JFK", "America", "New York" },
                    { 22, "LAX", "America", "Los Angeles" },
                    { 23, "MIA", "America", "Miami" },
                    { 24, "YYZ", "America", "Toronto" },
                    { 25, "GRU", "America", "San Paolo" },
                    { 26, "EZE", "America", "Buenos Aires" },
                    { 27, "MEX", "America", "Città del Messico" },
                    { 28, "ORD", "America", "Chicago" },
                    { 29, "SFO", "America", "San Francisco" },
                    { 30, "LIM", "America", "Lima" },
                    { 31, "CAI", "Africa", "Il Cairo" },
                    { 32, "RAK", "Africa", "Marrakech" },
                    { 33, "CPT", "Africa", "Città del Capo" },
                    { 34, "JNB", "Africa", "Johannesburg" },
                    { 35, "NBO", "Africa", "Nairobi" },
                    { 36, "TUN", "Africa", "Tunisi" },
                    { 37, "CMN", "Africa", "Casablanca" },
                    { 38, "ADD", "Africa", "Addis Abeba" },
                    { 39, "DSS", "Africa", "Dakar" },
                    { 40, "ACC", "Africa", "Accra" },
                    { 41, "SYD", "Oceania", "Sydney" },
                    { 42, "MEL", "Oceania", "Melbourne" },
                    { 43, "AKL", "Oceania", "Auckland" },
                    { 44, "BNE", "Oceania", "Brisbane" },
                    { 45, "PER", "Oceania", "Perth" },
                    { 46, "WLG", "Oceania", "Wellington" },
                    { 47, "CHC", "Oceania", "Christchurch" },
                    { 48, "ADL", "Oceania", "Adelaide" },
                    { 49, "NAN", "Oceania", "Nadi" },
                    { 50, "PPT", "Oceania", "Papeete" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DestinazioniAree");
        }
    }
}
