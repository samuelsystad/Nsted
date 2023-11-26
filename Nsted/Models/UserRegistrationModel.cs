using System.ComponentModel.DataAnnotations;

namespace Nsted.Models
{
    /// Modell for å registrere en ny bruker.
    public class UserRegistrationModel
    {
        /// Brukerens e-postadresse. Brukes som en unik identifikator for brukeren.
        public string Email { get; set; }

        /// Brukerens passord. Dette passordet skal hashes før det lagres i databasen.
        public string Password { get; set; }

        /// Et felt for å bekrefte passordet. Dette må samsvare med 'Password' feltet.
        /// Anvender 'Compare' attributtet for å validere at dette feltet er likt med 'Password' feltet.
        [Compare("Password", ErrorMessage = "Passordene er ikke like.")]
        public string ConfirmPassword { get; set; }
    }
}
