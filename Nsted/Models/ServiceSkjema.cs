using System;
using System.ComponentModel.DataAnnotations; // Required for data annotations

namespace Nsted.Models
{
    public class ServiceSkjema
    {
        public int ServiceSkjemaId { get; set; }
        public int KundeId { get; set; } // Foreign Key referencing Kunde

        // Navigation property to Kunde
        public Kunde Kunde { get; set; }

        public DateTime MottattDato { get; set; }
        public int OrdreNr { get; set; }

        [Required]
        public string ProduktType { get; set; }

        public int ÅrsModell { get; set; }

        // If Servicetype is a string, change to string
        public string Servicetype { get; set; }

        public int SerieNummer { get; set; }

        [Required]
        public string AvtaltMedKunden { get; set; }

        [Required]
        public string Reparasjonsbeskrivelse { get; set; }

        [Required]
        public string BrukteDeler { get; set; }

        public int Arbeidstimer { get; set; }
        public DateTime FerdigstiltDato { get; set; }

        [Required]
        public string ReturDeler { get; set; }

        [Required]
        public string ForsendelsesMåte { get; set; }

        [Required]
        public string KundeSignatur { get; set; }

        [Required]
        public string MekanikerSignatur { get; set; }
    }
}
