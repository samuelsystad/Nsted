namespace Nsted.Models
{
    public class Kunde
    {
        public int KundeId { get; set; }

        public required string Fornavn { get; set; }

        public required string Etternavn { get; set; }

        public required int Telefon { get; set; }

        public required string Email { get; set; }

        public required string Adresse {  get; set; }
        
        public required DateTime Registrert {  get; set; }
    }
}

