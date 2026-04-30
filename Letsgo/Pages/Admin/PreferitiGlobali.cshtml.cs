using Letsgo.Data;
using Letsgo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Letsgo.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class PreferitiGlobaliModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public PreferitiGlobaliModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<DestinazioneSalvata> Destinazioni { get; set; } = new();
        public async Task OnGetAsync()
        {
            Destinazioni = await _context.DestinazioniSalvate.OrderByDescending(a => a.DataSalvataggio).ToListAsync();
        }
    }
}
