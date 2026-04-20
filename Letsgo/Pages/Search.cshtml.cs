using Letsgo.Models;
using Letsgo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Letsgo.Pages
{
    public class SearchModel : PageModel

    {
        private readonly ServizioMeteo _servizioMeteo;
        private readonly ServizioVoli _servizioVoli;

        public SearchModel(ServizioMeteo servizioMeteo, ServizioVoli servizioVoli)
        {
            _servizioMeteo = servizioMeteo;
            _servizioVoli = servizioVoli;
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

        public RisultatoMeteo? MeteoCorrente { get; set; }
        public RisultatoVoli? RisultatoVoli { get; set; }
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

        private DestinazioneTest? OttieniDestinazioneDiTestDaArea(string area)
        {
            return area switch
            {
                "Europa" => new DestinazioneTest { Citta = "Roma", Aeroporto = "FCO" },
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
