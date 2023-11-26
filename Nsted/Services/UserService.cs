using BCrypt.Net;
namespace Nsted.Services
{

    /// Tjeneste som krypterer passordet etter man har registrert en ny bruker.
    public class UserService
    {
        /// Hasher et passord ved hjelp av BCrypt algoritmen.
        /// <param name="password">Passordet som skal hashes.</param>
        /// <returns>Hashet versjon av passordet.</returns>
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        /// Verifiserer et inngitt passord mot et lagret hash.
        /// <param name="enteredPassword">Inngitt passord for verifisering.</param>
        /// <param name="storedHash">Lagret hash for sammenligning.</param>
        /// <returns>True hvis passordet matcher hashen, ellers false.</returns>
        public bool VerifyPassword(string enteredPassword, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, storedHash);
        }
    }
}
