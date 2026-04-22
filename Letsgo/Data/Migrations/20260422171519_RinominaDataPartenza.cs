using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Letsgo.Data.Migrations
{
    /// <inheritdoc />
    public partial class RinominaDataPartenza : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataAndata",
                table: "DestinazioneSalvata",
                newName: "DataPartenza");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataPartenza",
                table: "DestinazioneSalvata",
                newName: "DataAndata");
        }
    }
}
