using Letsgo.Models;
using System.Text.Json;

namespace Letsgo.Services
{
    public class ServizioVoli
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configurazione; // Iniezione di IConfiguration per leggere la chiave API da appsettings.json

        public ServizioVoli(HttpClient httpClient, IConfiguration configurazione)
        {
            _httpClient = httpClient;
            _configurazione = configurazione;
        }

        public async Task<RisultatoVoli?> OttieniVoliAsync(string aeroportoPartenza, string aeroportiArrivo, DateTime dataPartenza, DateTime? dataRitorno)
        {
            var chiaveApi = _configurazione["SerpApi:ApiKey"];

            if (string.IsNullOrWhiteSpace(chiaveApi))
            {
                throw new Exception("La chiave API di SerpApi non è stata letta da appsettings.json");
            }

            var url = $"https://serpapi.com/search.json?engine=google_flights" +
                      $"&departure_id={Uri.EscapeDataString(aeroportoPartenza)}" +
                      $"&arrival_id={Uri.EscapeDataString(aeroportiArrivo)}" +
                      $"&outbound_date={dataPartenza:yyyy-MM-dd}" +
                      $"&currency=EUR" +
                      $"&hl=it" +
                      $"&gl=it" +
                      $"&api_key={Uri.EscapeDataString(chiaveApi)}";

            if (dataRitorno.HasValue)
            {
                url += $"&return_date={dataRitorno.Value:yyyy-MM-dd}";
            }

            var risposta = await _httpClient.GetAsync(url);
            var json = await risposta.Content.ReadAsStringAsync();

            if (!risposta.IsSuccessStatusCode)
            {
                throw new Exception($"Errore SerpApi - Status: {(int)risposta.StatusCode} - Risposta: {json}");
            }

            return JsonSerializer.Deserialize<RisultatoVoli>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
