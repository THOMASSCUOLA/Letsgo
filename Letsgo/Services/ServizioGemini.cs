using Letsgo.Models;
using System.Text;
using System.Text.Json;

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

            var corpo = new // Struttura del corpo della richiesta secondo le specifiche di Gemini
            {
                contents = new[] // Gemini si aspetta un array di contenuti, anche se noi ne inviamo solo uno
                {
                    new
                    {
                        parts = new[] // Ogni contenuto può avere più parti, noi ne inviamo solo una con il testo della richiesta
                        {
                            new { text = testoRichiesta } // Il testo della richiesta che vogliamo inviare a Gemini
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
                throw new Exception($"Errore Gemini - Status: {(int)risposta.StatusCode} - ci dispiace, se il codice di errore è 503 purtroppo non è colpa del nostro sito , ma è l'api di gemini che ha avuto un problema , riprovare tra poco.");
            }

            var risultato = JsonSerializer.Deserialize<RispostaGemini>(jsonRisposta, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return risultato?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text;
            
            
        }
    }
}
