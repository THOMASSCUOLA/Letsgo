using Letsgo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Letsgo.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<DestinazioneSalvata> DestinazioniSalvate { get; set; }
        // 1. Questa è la tua nuova tabella nel database
        public DbSet<DestinazioneArea> DestinazioniAree { get; set; }

        // 2. Questo inserisce i dati iniziali quando crei il database
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // Fondamentale per Identity!

            builder.Entity<DestinazioneArea>().HasData(
                // --- EUROPA (10) ---
                new DestinazioneArea { Id = 1, AreaGeografica = "Europa", Citta = "Roma", Aeroporto = "FCO" },
                new DestinazioneArea { Id = 2, AreaGeografica = "Europa", Citta = "Milano", Aeroporto = "MXP" },
                new DestinazioneArea { Id = 3, AreaGeografica = "Europa", Citta = "Parigi", Aeroporto = "CDG" },
                new DestinazioneArea { Id = 4, AreaGeografica = "Europa", Citta = "Londra", Aeroporto = "LHR" },
                new DestinazioneArea { Id = 5, AreaGeografica = "Europa", Citta = "Madrid", Aeroporto = "MAD" },
                new DestinazioneArea { Id = 6, AreaGeografica = "Europa", Citta = "Berlino", Aeroporto = "BER" },
                new DestinazioneArea { Id = 7, AreaGeografica = "Europa", Citta = "Amsterdam", Aeroporto = "AMS" },
                new DestinazioneArea { Id = 8, AreaGeografica = "Europa", Citta = "Vienna", Aeroporto = "VIE" },
                new DestinazioneArea { Id = 9, AreaGeografica = "Europa", Citta = "Lisbona", Aeroporto = "LIS" },
                new DestinazioneArea { Id = 10, AreaGeografica = "Europa", Citta = "Atene", Aeroporto = "ATH" },

                // --- ASIA (10) ---
                new DestinazioneArea { Id = 11, AreaGeografica = "Asia", Citta = "Tokyo", Aeroporto = "HND" },
                new DestinazioneArea { Id = 12, AreaGeografica = "Asia", Citta = "Pechino", Aeroporto = "PEK" },
                new DestinazioneArea { Id = 13, AreaGeografica = "Asia", Citta = "Bangkok", Aeroporto = "BKK" },
                new DestinazioneArea { Id = 14, AreaGeografica = "Asia", Citta = "Singapore", Aeroporto = "SIN" },
                new DestinazioneArea { Id = 15, AreaGeografica = "Asia", Citta = "Dubai", Aeroporto = "DXB" },
                new DestinazioneArea { Id = 16, AreaGeografica = "Asia", Citta = "Seoul", Aeroporto = "ICN" },
                new DestinazioneArea { Id = 17, AreaGeografica = "Asia", Citta = "Mumbai", Aeroporto = "BOM" },
                new DestinazioneArea { Id = 18, AreaGeografica = "Asia", Citta = "Hong Kong", Aeroporto = "HKG" },
                new DestinazioneArea { Id = 19, AreaGeografica = "Asia", Citta = "Kuala Lumpur", Aeroporto = "KUL" },
                new DestinazioneArea { Id = 20, AreaGeografica = "Asia", Citta = "Taipei", Aeroporto = "TPE" },

                // --- AMERICA (10) ---
                new DestinazioneArea { Id = 21, AreaGeografica = "America", Citta = "New York", Aeroporto = "JFK" },
                new DestinazioneArea { Id = 22, AreaGeografica = "America", Citta = "Los Angeles", Aeroporto = "LAX" },
                new DestinazioneArea { Id = 23, AreaGeografica = "America", Citta = "Miami", Aeroporto = "MIA" },
                new DestinazioneArea { Id = 24, AreaGeografica = "America", Citta = "Toronto", Aeroporto = "YYZ" },
                new DestinazioneArea { Id = 25, AreaGeografica = "America", Citta = "San Paolo", Aeroporto = "GRU" },
                new DestinazioneArea { Id = 26, AreaGeografica = "America", Citta = "Buenos Aires", Aeroporto = "EZE" },
                new DestinazioneArea { Id = 27, AreaGeografica = "America", Citta = "Città del Messico", Aeroporto = "MEX" },
                new DestinazioneArea { Id = 28, AreaGeografica = "America", Citta = "Chicago", Aeroporto = "ORD" },
                new DestinazioneArea { Id = 29, AreaGeografica = "America", Citta = "San Francisco", Aeroporto = "SFO" },
                new DestinazioneArea { Id = 30, AreaGeografica = "America", Citta = "Lima", Aeroporto = "LIM" },

                // --- AFRICA (10) ---
                new DestinazioneArea { Id = 31, AreaGeografica = "Africa", Citta = "Il Cairo", Aeroporto = "CAI" },
                new DestinazioneArea { Id = 32, AreaGeografica = "Africa", Citta = "Marrakech", Aeroporto = "RAK" },
                new DestinazioneArea { Id = 33, AreaGeografica = "Africa", Citta = "Città del Capo", Aeroporto = "CPT" },
                new DestinazioneArea { Id = 34, AreaGeografica = "Africa", Citta = "Johannesburg", Aeroporto = "JNB" },
                new DestinazioneArea { Id = 35, AreaGeografica = "Africa", Citta = "Nairobi", Aeroporto = "NBO" },
                new DestinazioneArea { Id = 36, AreaGeografica = "Africa", Citta = "Tunisi", Aeroporto = "TUN" },
                new DestinazioneArea { Id = 37, AreaGeografica = "Africa", Citta = "Casablanca", Aeroporto = "CMN" },
                new DestinazioneArea { Id = 38, AreaGeografica = "Africa", Citta = "Addis Abeba", Aeroporto = "ADD" },
                new DestinazioneArea { Id = 39, AreaGeografica = "Africa", Citta = "Dakar", Aeroporto = "DSS" },
                new DestinazioneArea { Id = 40, AreaGeografica = "Africa", Citta = "Accra", Aeroporto = "ACC" },
                // --- OCEANIA (10) ---
                new DestinazioneArea { Id = 41, AreaGeografica = "Oceania", Citta = "Sydney", Aeroporto = "SYD" },
                new DestinazioneArea { Id = 42, AreaGeografica = "Oceania", Citta = "Melbourne", Aeroporto = "MEL" },
                new DestinazioneArea { Id = 43, AreaGeografica = "Oceania", Citta = "Auckland", Aeroporto = "AKL" },
                new DestinazioneArea { Id = 44, AreaGeografica = "Oceania", Citta = "Brisbane", Aeroporto = "BNE" },
                new DestinazioneArea { Id = 45, AreaGeografica = "Oceania", Citta = "Perth", Aeroporto = "PER" },
                new DestinazioneArea { Id = 46, AreaGeografica = "Oceania", Citta = "Wellington", Aeroporto = "WLG" },
                new DestinazioneArea { Id = 47, AreaGeografica = "Oceania", Citta = "Christchurch", Aeroporto = "CHC" },
                new DestinazioneArea { Id = 48, AreaGeografica = "Oceania", Citta = "Adelaide", Aeroporto = "ADL" },
                new DestinazioneArea { Id = 49, AreaGeografica = "Oceania", Citta = "Nadi", Aeroporto = "NAN" },    // Isole Figi
                new DestinazioneArea { Id = 50, AreaGeografica = "Oceania", Citta = "Papeete", Aeroporto = "PPT" }  // Polinesia Francese
           ); 
            
        }
    }
}
