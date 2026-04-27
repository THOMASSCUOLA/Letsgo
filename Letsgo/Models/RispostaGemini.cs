using System.Text.Json.Serialization;

namespace Letsgo.Models
{
   
        public class RispostaGemini
        {
            [JsonPropertyName("candidates")]
            public List<CandidatoGemini>? Candidates { get; set; }
        }

        public class CandidatoGemini
        {
            [JsonPropertyName("content")]
            public ContenutoGemini? Content { get; set; }
        }

        public class ContenutoGemini
        {
            [JsonPropertyName("parts")]
            public List<ParteGemini>? Parts { get; set; }
        }

        public class ParteGemini
        {
            [JsonPropertyName("text")]
            public string? Text { get; set; }
        }
    
}
