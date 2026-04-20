using Letsgo.Models;
using Letsgo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Letsgo.Pages
{
    public class SearchModel : PageModel

    {
        private readonly ServizioMeteo _servizioMeteo;

        public SearchModel(ServizioMeteo servizioMeteo)
        {
            _servizioMeteo = servizioMeteo;
        }
        [BindProperty]
        public string? AereoportoPartenza { get; set; }

        [BindProperty]
        public string? Areaselezionata { get; set; }

        [BindProperty]
        public DateTime? DataPartenza { get; set; }

        [BindProperty]
        public DateTime? DataRitorno { get; set; }

        public string? Messaggio { get; set; }

        public string? MessaggioErrore { get; set; }

        public string? CittaDiTest { get; set; }

        public RisultatoMeteo? MeteoCorrente { get; set; }
        public void OnGet()
        {
        }
        public async Task OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Areaselezionata))
            {
                MessaggioErrore = "Seleziona un'area geografica.";
                return;
            }

            CittaDiTest = OttieniCittaDiTestDaArea(Areaselezionata);

            if (string.IsNullOrWhiteSpace(CittaDiTest))
            {
                MessaggioErrore = "Area geografica non valida.";
                return;
            }

            MeteoCorrente = await _servizioMeteo.OttieniMeteoAsync(CittaDiTest);

            if (MeteoCorrente == null)
            {
                MessaggioErrore = "Non sono riuscito a recuperare il meteo.";
                return;
            }

            Messaggio = $"Per l'area {Areaselezionata} sto usando {CittaDiTest} come città di test.";
        }

        private string? OttieniCittaDiTestDaArea(string area)
        {
            return area switch
            {
                "Europa" => "Rome",
                "Asia" => "Tokyo",
                "America" => "New York",
                "Africa" => "Il Cairo",
                _ => null
            };
        }
    }
}
