using Letsgo.Models;
using System.Text.Json;
using System.Text;

namespace Letsgo.Services
{
    public class ServizioGemini
    {
        public HttpClient _httpClient;
        public IConfiguration _configurazione;

        public ServizioGemini(HttpClient httpClient, IConfiguration configurazione)
        {
            _httpClient = httpClient;
            _configurazione = configurazione;
        }
        public async Task<string?> OttieniConsiglioAsync(string testoRichiesta)
        {
            var chiaveApi = _configurazione["Gemini:ApiKey"];

            if (string.IsNullOrWhiteSpace(chiaveApi))
            {
                throw new Exception("La chiave API di Gemini non è stata letta da appsettings.json");
            }

            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={Uri.EscapeDataString(chiaveApi)}";

            var corpo = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = testoRichiesta }
                        }
                    }
                }
            };

            var json = JsonSerializer.Serialize(corpo);
            var contenuto = new StringContent(json, Encoding.UTF8, "application/json");

            var risposta = await _httpClient.PostAsync(url, contenuto);
            var jsonRisposta = await risposta.Content.ReadAsStringAsync();

            if (!risposta.IsSuccessStatusCode)
            {
                throw new Exception($"Errore Gemini - Status: {(int)risposta.StatusCode} - Risposta: {jsonRisposta}");
            }

            var risultato = JsonSerializer.Deserialize<RispostaGemini>(jsonRisposta, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return risultato?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text;
        }
    }
}
