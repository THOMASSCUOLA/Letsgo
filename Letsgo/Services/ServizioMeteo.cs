using Letsgo.Models;
using System.Text.Json;

namespace Letsgo.Services
{
    public class ServizioMeteo
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configurazione;

        public ServizioMeteo(HttpClient httpClient, IConfiguration configurazione)
        {
            _httpClient = httpClient;
            _configurazione = configurazione;
        }

        public async Task<RisultatoMeteo?> OttieniMeteoAsync(string citta)
        {
            var chiaveApi = _configurazione["OpenWeather:ApiKey"];
            if (string.IsNullOrWhiteSpace(chiaveApi))
            {
                throw new Exception("La chiave API di OpenWeather non è stata letta da appsettings.json");
            }
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={citta}&appid={chiaveApi}&units=metric&lang=it";

            var risposta = await _httpClient.GetAsync(url);
            var json = await risposta.Content.ReadAsStringAsync();
            if (!risposta.IsSuccessStatusCode)
            {
                throw new Exception($"Errore OpenWeather - Status: {(int)risposta.StatusCode} - Risposta: {json}");
            }
            

            return JsonSerializer.Deserialize<RisultatoMeteo>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
