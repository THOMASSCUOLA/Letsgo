using System.Text.Json.Serialization;

namespace Letsgo.Models
{
    public class RisultatoVoli
    {
        [JsonPropertyName("best_flights")]
        public List<VoloSerpApi>? MiglioriVoli { get; set; }

        [JsonPropertyName("other_flights")]
        public List<VoloSerpApi>? AltriVoli { get; set; }
    }

    public class VoloSerpApi
    {
        [JsonPropertyName("price")]
        public int? Prezzo { get; set; }

        [JsonPropertyName("total_duration")]
        public int? DurataTotaleMinuti { get; set; }

        [JsonPropertyName("flights")]
        public List<TrattaVolo>? Tratte { get; set; }
    }

    public class TrattaVolo
    {
        [JsonPropertyName("airline")]
        public string? Compagnia { get; set; }

        [JsonPropertyName("flight_number")]
        public string? NumeroVolo { get; set; }

        [JsonPropertyName("departure_airport")]
        public AeroportoVolo? AeroportoPartenza { get; set; }

        [JsonPropertyName("arrival_airport")]
        public AeroportoVolo? AeroportoArrivo { get; set; }
    }

    public class AeroportoVolo
    {
        [JsonPropertyName("name")]
        public string? Nome { get; set; }

        [JsonPropertyName("id")]
        public string? Codice { get; set; }

        [JsonPropertyName("time")]
        public string? Orario { get; set; }
    }
}
