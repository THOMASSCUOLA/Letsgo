using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Letsgo.Data;
using Letsgo.Models;
using System.Security.Claims;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Letsgo.Pages
{
    [Authorize]
    public class PreferitiModel : PageModel
    {
        private readonly ApplicationDbContext _context;
       
        public PreferitiModel(ApplicationDbContext contesto, UserManager<IdentityUser> userManager)
        {
            _context = contesto;
            
        }
        public List<DestinazioneSalvata> Destinazioni { get; set; } = new();

        public async Task OnGetAsync()
        {
         

            
            var idUtente = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!string.IsNullOrWhiteSpace(idUtente))
            {
                Destinazioni = await _context.DestinazioniSalvate
                    .Where(d => d.IdUtente == idUtente)
                    .OrderByDescending(d => d.DataSalvataggio)
                    .ToListAsync();
            }
        }
        public async Task<IActionResult> OnPostEliminaAsync(int id)
        {
            // Prendo l'ID dell'utente loggato
            var idUtente = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(idUtente))
                return RedirectToPage("/Account/Login", new { area = "Identity" });

            // Cerco la destinazione nel database (assicurandomi che sia dell'utente corretto)
            var destinazione = await _context.DestinazioniSalvate
                .FirstOrDefaultAsync(d => d.Id == id && d.IdUtente == idUtente);

            // Se esiste, la cancello
            if (destinazione != null)
            {
                _context.DestinazioniSalvate.Remove(destinazione);
                await _context.SaveChangesAsync();

                TempData["MessaggioSuccesso"] = "Destinazione eliminata con successo.";
            }

            // Ricarico la pagina per mostrare la lista aggiornata
            return RedirectToPage();
        }
    }
}
