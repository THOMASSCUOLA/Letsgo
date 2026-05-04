using Letsgo.Data;
using Letsgo.Models;
using Microsoft.EntityFrameworkCore;

namespace Letsgo.Services
{
    public class ServizioDestinazioni
    {
        private readonly ApplicationDbContext _context;

        // Chiediamo il Database al costruttore
        public ServizioDestinazioni(ApplicationDbContext context)
        {
            _context = context;
        }

        // Metodo asincrono che interroga il database!
        public async Task<List<DestinazioneArea>> OttieniDestinazioniDaAreaAsync(string area)
        {
            return await _context.DestinazioniAree
                                 .Where(d => d.AreaGeografica == area)
                                 .ToListAsync();
        }
    }
    
}
