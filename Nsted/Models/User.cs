namespace Nsted.Models
{
    /// <summary>
    /// Representerer en bruker i systemet. Inneholder brukerens grunnleggende informasjon.
    /// </summary>
    public class User
    {
        /// Unik identifikator for brukeren.
        public int Id { get; set; }

        /// Brukerens e-postadresse. Brukes som en unik identifikator for brukeren.
        public string Email { get; set; }

        /// Hashet versjon av brukerens passord. Dette sikrer at passordet lagres på en sikker måte i databasen.
        public string PasswordHash { get; set; }
    }
}
