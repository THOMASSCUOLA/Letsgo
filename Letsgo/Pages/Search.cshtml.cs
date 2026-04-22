using Letsgo.Data;
using Letsgo.Models;
using Letsgo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Letsgo.Pages
{
    public class SearchModel : PageModel

    {
        private readonly ServizioMeteo _servizioMeteo;
        private readonly ServizioVoli _servizioVoli;
        private readonly ApplicationDbContext _context;
        public SearchModel(ServizioMeteo servizioMeteo, ServizioVoli servizioVoli, ApplicationDbContext context)
        {
            _servizioMeteo = servizioMeteo;
            _servizioVoli = servizioVoli;
            _context = context;
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

        public string? CittaDiTest { get; set; }
        public string? AeroportoArrivoTest { get; set; }
        [BindProperty]
        public int? PrezzoDaSalvare { get; set; }
        [BindProperty]
        public double? TemperaturaDaSalvare { get; set; }

        [BindProperty]
        public string? DescrizioneMeteoDaSalvare { get; set; }

        public RisultatoMeteo? MeteoCorrente { get; set; }
        public RisultatoVoli? RisultatoVoli { get; set; }
        public string? MessaggioSuccesso { get; set; }
        public void OnGet()
        {
        }
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
                MessaggioErrore = "Inserisci la data di andata.";
                return;
            }

            var destinazioneTest = OttieniDestinazioneDiTestDaArea(AreaSelezionata);

            if (destinazioneTest == null)
            {
                MessaggioErrore = "Area geografica non valida.";
                return;
            }

            CittaDiTest = destinazioneTest.Citta;
            AeroportoArrivoTest = destinazioneTest.Aeroporto;

            try
            {
                MeteoCorrente = await _servizioMeteo.OttieniMeteoAsync(CittaDiTest);

                RisultatoVoli = await _servizioVoli.OttieniVoliAsync(
                    AeroportoPartenza.ToUpper(),
                    AeroportoArrivoTest,
                    DataPartenza.Value,
                    DataRitorno);

                Messaggio = $"Per l'area {AreaSelezionata} sto usando {CittaDiTest} ({AeroportoArrivoTest}) come destinazione di test.";
            }
            catch (Exception ex)
            {
                MessaggioErrore = ex.Message;
            }
        }
        public async Task<IActionResult> OnPostSalvaAsync(
     string aeroportoPartenza,
     string areaSelezionata,
     DateTime? dataPartenza,
     DateTime? dataRitorno,
     string cittaDiTest,
     string aeroportoArrivoTest,
     int? prezzoDaSalvare,
     double? temperaturaDaSalvare,
     string? descrizioneMeteoDaSalvare)
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
                string.IsNullOrWhiteSpace(cittaDiTest) ||
                string.IsNullOrWhiteSpace(aeroportoArrivoTest) ||
                !dataPartenza.HasValue)
            {
                MessaggioErrore = "Dati non completi per il salvataggio.";
                return Page();
            }

            var destinazione = new DestinazioneSalvata
            {
                IdUtente = idUtente,
                AeroportoPartenza = aeroportoPartenza,
                AreaGeografica = areaSelezionata,
                CittaDestinazione = cittaDiTest,
                AeroportoDestinazione = aeroportoArrivoTest,
                DataPartenza = dataPartenza.Value,
                DataRitorno = dataRitorno,
                Prezzo = prezzoDaSalvare,
                Temperatura = temperaturaDaSalvare,
                DescrizioneMeteo = descrizioneMeteoDaSalvare
            };

            _context.DestinazioneSalvata.Add(destinazione);
            await _context.SaveChangesAsync();

            TempData["MessaggioSuccesso"] = "Destinazione salvata correttamente.";

            return RedirectToPage("/Preferiti");
        }

        private DestinazioneTest? OttieniDestinazioneDiTestDaArea(string area)
        {
            return area switch
            {
                "Europa" => new DestinazioneTest { Citta = "Rome", Aeroporto = "FCO" },
                "Asia" => new DestinazioneTest { Citta = "Tokyo", Aeroporto = "HND" },
                "America" => new DestinazioneTest { Citta = "New York", Aeroporto = "JFK" },
                "Africa" => new DestinazioneTest { Citta = "Il Cairo", Aeroporto = "CAI" },
                _ => null
            };
        }

        private class DestinazioneTest
        {
            public string Citta { get; set; } = "";
            public string Aeroporto { get; set; } = "";
        }
    }
}
