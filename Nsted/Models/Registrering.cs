namespace Nsted.Models
{
    /// <summary>
    /// Modell for registrering av henvendelser.
    /// </summary>
    public class Registrering
        {
            public int RegistreringId { get; set; }
            public int KundeId { get; set; } // Fremmednøkkel som peker til Kunder-tabellen
            public required Kunde Kunde { get; set; } // Navigasjonsegenskap til Kunde
            public required int BookingTilUke { get; set; }
            public required DateTime HenvendelseMottatt { get; set; }
            public required bool CaseFerdig { get; set; }
            public required string ProduktType { get; set; }
            public required string Feilbeskrivelse { get; set; }
            public required DateTime AvtaltLevering { get; set; }   

            public required DateTime ProduktMottatt { get; set; }
            public required DateTime AvtaltFerdigstillelseInnen { get; set; }
        public required DateTime ServiceFerdig { get; set; }

        public required int AntallTimerUtført { get; set; } 

        public required int OrdreNr { get; set; }

        public required bool ServiceSkjema { get; set; }

     }
 }
