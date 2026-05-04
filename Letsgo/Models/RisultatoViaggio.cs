namespace Letsgo.Models
{
    public class RisultatoViaggio
    {
        public string Citta { get; set; } = "";
        public string AeroportoDestinazione { get; set; } = "";
        public int? Prezzo { get; set; }
        public int? DurataTotaleMinuti { get; set; }
        public double? DurataInOre => DurataTotaleMinuti.HasValue ? (double?)DurataTotaleMinuti.Value / 60 : null;
        public string? Compagnia { get; set; }
        public int NumeroScali { get; set; }
        public double? Temperatura { get; set; }
        public string? DescrizioneMeteo { get; set; }

        public bool ConsigliatoDaGemini { get; set; } // Indica se questa destinazione è stata consigliata da Gemini
    }
}
