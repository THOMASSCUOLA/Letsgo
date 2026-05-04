using System.Security.Claims;
using Letsgo.Data;
using Letsgo.Models;
using Letsgo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Letsgo.Pages
{
    public class SearchModel : PageModel
    {
        private readonly ServizioVoli _servizioVoli;
        private readonly ServizioMeteo _servizioMeteo;
        private readonly ApplicationDbContext _context;
        private readonly ServizioGemini _servizioGemini;
        private readonly ServizioDestinazioni _servizioDestinazioni ;

        public SearchModel(
            ServizioVoli servizioVoli,
            ServizioMeteo servizioMeteo,
            ApplicationDbContext context,
            ServizioGemini servizioGemini,
            ServizioDestinazioni servizioDestinazioni)
        {
            _servizioVoli = servizioVoli;
            _servizioMeteo = servizioMeteo;
            _context = context;
            _servizioGemini = servizioGemini;
            _servizioDestinazioni = servizioDestinazioni;
        }

        [BindProperty]
        public string? AeroportoPartenza { get; set; }

        [BindProperty]
        public string? AreaSelezionata { get; set; }

        [BindProperty]
        public DateTime? DataPartenza { get; set; }

        [BindProperty]
        public DateTime? DataRitorno { get; set; }

        public string? Messaggio { get; set; }
        public string? MessaggioErrore { get; set; }



        public List<RisultatoViaggio> RisultatiViaggio { get; set; } = new();

        public string? ConsiglioGemini { get; set; }


      

        public async Task OnPostAsync()
        {

            if (string.IsNullOrWhiteSpace(AeroportoPartenza))
            {
                MessaggioErrore = "Inserisci un aeroporto di partenza.";
                return;
            }

            if (string.IsNullOrWhiteSpace(AreaSelezionata))
            {
                MessaggioErrore = "Seleziona un'area geografica.";
                return;
            }

            if (!DataPartenza.HasValue)
            {
                MessaggioErrore = "Inserisci la data di partenza.";
                return;
            }

           
            var destinazioniArea = await _servizioDestinazioni.OttieniDestinazioniDaAreaAsync(AreaSelezionata);
            // Mescola le città in modo casuale e tienine al massimo 5 per non bloccare l'API
            var random = new Random();
            destinazioniArea = destinazioniArea.OrderBy(d => random.Next()).Take(5).ToList();
            if (destinazioniArea.Count == 0)
            {
                MessaggioErrore = "Area geografica non valida.";
                return;
            }

            var aeroportiArrivo = string.Join(",", destinazioniArea.Select(d => d.Aeroporto));

            try
            {
                var RisultatoVoli = await _servizioVoli.OttieniVoliAsync(
                     AeroportoPartenza.ToUpper(),
                    aeroportiArrivo,
                    DataPartenza.Value,
                    DataRitorno);

                var voli = new List<VoloSerpApi>();

                if (RisultatoVoli?.MiglioriVoli != null)
                    voli.AddRange(RisultatoVoli.MiglioriVoli); // Aggiungiamo i migliori voli alla lista dei voli da processare 

                if (RisultatoVoli?.AltriVoli != null)
                    voli.AddRange(RisultatoVoli.AltriVoli);


                var aeroportiGiaAggiunti = new HashSet<string>();
                foreach (var volo in voli)
                {
                    var primaTratta = volo.Tratte?.FirstOrDefault();
                    var ultimaTratta = volo.Tratte?.LastOrDefault();

                    if (ultimaTratta?.AeroportoArrivo?.Codice == null)
                        continue;

                    var aeroportoFinale = ultimaTratta.AeroportoArrivo.Codice;
                    if (aeroportiGiaAggiunti.Contains(aeroportoFinale))
                        continue;

                    aeroportiGiaAggiunti.Add(aeroportoFinale);
                    var destinazione = destinazioniArea
                        .FirstOrDefault(d => d.Aeroporto == aeroportoFinale);

                    if (destinazione == null)
                        continue;

                    var meteo = await _servizioMeteo.OttieniMeteoAsync(destinazione.Citta);

                    RisultatiViaggio.Add(new RisultatoViaggio
                    {
                        Citta = destinazione.Citta,
                        AeroportoDestinazione = destinazione.Aeroporto,
                        Prezzo = volo.Prezzo,
                        DurataTotaleMinuti = volo.DurataTotaleMinuti,
                        
                        Compagnia = primaTratta?.Compagnia,
                        NumeroScali = volo.Tratte != null && volo.Tratte.Count > 1 ? volo.Tratte.Count - 1 : 0,
                        Temperatura = meteo?.Main?.Temp,
                        DescrizioneMeteo = meteo?.Weather?.FirstOrDefault()?.Description
                    });
                   
                }

                if (RisultatiViaggio.Count == 0)
                {
                    MessaggioErrore = "Non sono stati trovati voli validi per quest'area.";
                    return;
                }
                var testoPerGemini = CreaTestoPerGemini(RisultatiViaggio);

                ConsiglioGemini = await _servizioGemini.OttieniConsiglioAsync(testoPerGemini);
                var CittaConsigliataGemini = TrovaCittaConsigliata(ConsiglioGemini, RisultatiViaggio);

                if (!string.IsNullOrWhiteSpace(CittaConsigliataGemini))
                {
                    foreach (var risultato in RisultatiViaggio)
                    {
                        risultato.ConsigliatoDaGemini =
                            risultato.Citta.Equals(CittaConsigliataGemini, StringComparison.OrdinalIgnoreCase);
                    }
                }

                Messaggio = $"Ho trovato {RisultatiViaggio.Count} destinazioni per l'area {AreaSelezionata}.";


            }
            catch (Exception ex)
            {
                MessaggioErrore = ex.Message;
            }
        }
        private string CreaTestoPerGemini(List<RisultatoViaggio> risultati)
        {
            var testo = """
    Valuta queste destinazioni di viaggio in base a prezzo, meteo e comodità.
    Scegli UNA sola destinazione migliore.

    Rispondi in italiano con questo formato:
    CITTA: nome città
    CONSIGLIO: breve spiegazione massimo 3 righe
    ITINERARIO:
    - punto 1
    - punto 2
    - punto 3

    Destinazioni:
    """;

            foreach (var r in risultati)
            {
                testo += $"""
        
        Città: {r.Citta}
        Aeroporto: {r.AeroportoDestinazione}
        Prezzo: {(r.Prezzo.HasValue ? r.Prezzo + " euro" : "non disponibile")}
        Meteo: {(r.Temperatura.HasValue ? r.Temperatura + " gradi" : "temperatura non disponibile")} - {r.DescrizioneMeteo}
        Scali: {(r.NumeroScali == 0 ? "nessuno" : r.NumeroScali)}
        Compagnia: {r.Compagnia}
        """;
            }

            return testo;
        }

        private string? TrovaCittaConsigliata(string? consiglio, List<RisultatoViaggio> risultati)
        {
            if (string.IsNullOrWhiteSpace(consiglio))
                return null;

            foreach (var risultato in risultati)
            {
                if (consiglio.Contains(risultato.Citta, StringComparison.OrdinalIgnoreCase))
                {
                    return risultato.Citta;
                }
            }

            return null;
        }
        public async Task<IActionResult> OnPostSalvaAsync(
            string aeroportoPartenza,
            string areaSelezionata,
            DateTime? dataPartenza,
            DateTime? dataRitorno,
            string cittaDestinazione,
            string aeroportoDestinazione,
            int? prezzoDaSalvare,
            double? temperaturaDaSalvare,
            string? descrizioneMeteoDaSalvare,
            int? durataTotaleMinutiDaSalvare
            )
        {
            if (!User.Identity!.IsAuthenticated)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var idUtente = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(idUtente))
            {
                MessaggioErrore = "Utente non valido.";
                return Page();
            }

            if (string.IsNullOrWhiteSpace(aeroportoPartenza) ||
                string.IsNullOrWhiteSpace(areaSelezionata) ||
                string.IsNullOrWhiteSpace(cittaDestinazione) ||
                string.IsNullOrWhiteSpace(aeroportoDestinazione) ||
                !dataPartenza.HasValue)
            {
                MessaggioErrore = "Dati non completi per il salvataggio.";
                return Page();
            }

            var destinazione = new DestinazioneSalvata
            {
                IdUtente = idUtente,
                EmailUtente = User.Identity?.Name,
                AeroportoPartenza = aeroportoPartenza,
                AreaGeografica = areaSelezionata,
                CittaDestinazione = cittaDestinazione,
                AeroportoDestinazione = aeroportoDestinazione,
                DataPartenza = dataPartenza.Value,
                DataRitorno = dataRitorno,
                Prezzo = prezzoDaSalvare,
                Temperatura = temperaturaDaSalvare,
                DescrizioneMeteo = descrizioneMeteoDaSalvare,
                DurataTotaleMinuti = durataTotaleMinutiDaSalvare
            };

            _context.DestinazioniSalvate.Add(destinazione);
            await _context.SaveChangesAsync();

            TempData["MessaggioSuccesso"] = "Destinazione salvata correttamente."; //tempdata serve per passare dati tra richieste HTTP, in questo caso per mostrare un messaggio di successo dopo il redirect

            return RedirectToPage("/Preferiti");
        }

       
     

       

        
    }
}