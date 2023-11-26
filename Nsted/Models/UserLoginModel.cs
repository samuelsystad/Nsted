namespace Nsted.Models
{
    /// Modell for å logge inn en bruker.
    public class UserLoginModel
    {
        /// Brukerens e-postadresse. Brukes som identifikator for innlogging.
        public string Email { get; set; }

        /// Brukerens passord. Dette passordet vil bli sammenlignet med det hashede passordet lagret i databasen.
        public string Password { get; set; }
    }
}
