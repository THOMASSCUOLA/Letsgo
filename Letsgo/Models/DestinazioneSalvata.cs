using System.ComponentModel.DataAnnotations;

namespace Letsgo.Models
{
    public class DestinazioneSalvata
    {
        public int Id { get; set; }

        [Required]
        public string IdUtente { get; set; } = "";

        [Required]
        public string AeroportoPartenza { get; set; } = "";

        [Required]
        public string AreaGeografica { get; set; } = "";

        [Required]
        public string CittaDestinazione { get; set; } = "";

        [Required]
        public string AeroportoDestinazione { get; set; } = "";

        public DateTime DataPartenza { get; set; }

        public DateTime? DataRitorno { get; set; }

        public int? Prezzo { get; set; }

        public double? Temperatura { get; set; }

        public string? DescrizioneMeteo { get; set; }

        public DateTime DataSalvataggio { get; set; } = DateTime.Now;
    }
}
