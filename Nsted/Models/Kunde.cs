using System;
using System.ComponentModel.DataAnnotations;

namespace Nsted.Models
{
    public class Kunde
    {
        public int KundeId { get; set; }
        public string Fornavn { get; set; }
        public string Etternavn { get; set; }
        public int Telefon { get; set; }
        public string Email { get; set; }
        public string Adresse { get; set; }
        public DateTime Registrert { get; set; }
    }

}
