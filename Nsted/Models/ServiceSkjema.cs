using System;
using System.ComponentModel.DataAnnotations;

namespace Nsted.Models
{
    /// <summary>
    /// Modell for et serviceskjema. Representerer detaljer om en serviceordre.
    /// </summary>
    public class ServiceSkjema
    {
        public int ServiceSkjemaId { get; set; }
        public int KundeId { get; set; } // Fremmednøkkel som refererer til Kunder tabellen

        public Kunde Kunde { get; set; } // Navigation property to Kunde

        [Required]
        public DateTime MottattDato { get; set; }

        [Required]
        public int OrdreNr { get; set; }

        [Required]
        public string ProduktType { get; set; }

        [Required]
        public int ÅrsModell { get; set; }

        [Required]
        public string Servicetype { get; set; }

        [Required]
        public int SerieNummer { get; set; }

        [Required]
        public string AvtaltMedKunden { get; set; }

        [Required]
        public string Reparasjonsbeskrivelse { get; set; }

        [Required]
        public string BrukteDeler { get; set; }

        [Required]
        public int Arbeidstimer { get; set; }

        [Required]
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
