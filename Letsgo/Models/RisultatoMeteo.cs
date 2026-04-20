namespace Letsgo.Models
{
    public class RisultatoMeteo
    {

        public InfoPrincipale? Main { get; set; }
        public List<InfoMeteo>? Weather { get; set; }
        public string? Name { get; set; }
    }

    public class InfoPrincipale
    {
        public double Temp { get; set; }
    }

    public class InfoMeteo
    {
        public string? Description { get; set; }
    }
}



