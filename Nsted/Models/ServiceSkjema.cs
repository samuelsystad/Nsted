namespace Nsted.Models
{
    public class ServiceSkjema
    {
        public int ServiceSkjemaId { get; set; }
        public int KundeId { get; set; } // Foreign Key referencing Kunde

        // Navigation property to Kunde
        public required Kunde Kunde { get; set; }

        public DateTime MottattDato { get; set; }
        public int OrdreNr { get; set; }
        public required string ProduktType { get; set; }
        public int ÅrsModell { get; set; }
        public int Servicetype { get; set; }
        public int SerieNummer { get; set; }
        public required string AvtaltMedKunden { get; set; }
        public required string Reparasjonsbeskrivelse { get; set; }
        public required string BrukteDeler { get; set; }
        public int Arbeidstimer { get; set; }
        public DateTime FerdigstiltDato { get; set; }
        public required string ReturDeler { get; set; }
        public required string ForsendelsesMåte { get; set; }
        public required string KundeSignatur { get; set; }
        public required string MekanikerSignatur { get; set; }
    }
}
