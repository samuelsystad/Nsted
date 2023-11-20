using Nsted.Data;
using Nsted.Models;
using System.ComponentModel.DataAnnotations;

public class FullførtOrdre
{
    [Key]
    public int ServiceSkjemaId { get; set; }

    public int KundeId { get; set; }
    public DateTime MottattDato { get; set; }
    public int OrdreNr { get; set; }
    public string ProduktType { get; set; }
    public int ÅrsModell { get; set; }
    public string Servicetype { get; set; }
    public int SerieNummer { get; set; }
    public string AvtaltMedKunden { get; set; }
    public string Reparasjonsbeskrivelse { get; set; }
    public string BrukteDeler { get; set; }
    public int Arbeidstimer { get; set; }
    public DateTime FerdigstiltDato { get; set; }
    public string ReturDeler { get; set; }
    public string ForsendelsesMåte { get; set; }
    public string KundeSignatur { get; set; }
    public string MekanikerSignatur { get; set; }

    // Metode for å fullføre en tjeneste og lagre den som en FullførtOrdre
}
