namespace Nsted.Models
{
    public class Kunde
    {
        public int Id { get; set; }

        public required string Fornavn { get; set; }

        public required string Etternavn { get; set; }
    }
}
