using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nsted.Data;
using Nsted.Models;
using System.Linq;

namespace Nsted.Controllers
{
    // Autorisasjon kreves for å få tilgang til metoder i denne kontrolleren.
    [Authorize]
    public class KundeController : Controller
    {
        private readonly NstedDbContext nstedDbContext;

        // Konstruktør som initialiserer databasen kontekst.
        public KundeController(NstedDbContext nstedDbContext)
        {
            this.nstedDbContext = nstedDbContext;
        }

        // Viser siden for å legge til en ny kunde.
        public IActionResult Add()
        {
            return View();
        }

        // Behandler forespørselen for å legge til en ny kunde.
        [HttpPost]
        public IActionResult Add(Kunde kunde)
        {
            nstedDbContext.Add(kunde);
            nstedDbContext.SaveChanges();

            return RedirectToAction("List");
        }

        // Viser siden for å redigere en eksisterende kunde.
        public IActionResult Edit(int id)
        {
            var kunde = nstedDbContext.Kunder.FirstOrDefault(k => k.KundeId == id);

            if (kunde == null)
            {
                return NotFound();
            }

            return View(kunde);
        }

        // Behandler forespørselen for å oppdatere en eksisterende kunde.
        [HttpPost]
        public IActionResult Edit(int id, Kunde kunde)
        {
            if (id != kunde.KundeId)
            {
                return BadRequest();
            }

            var existingKunde = nstedDbContext.Kunder.FirstOrDefault(k => k.KundeId == id);
            if (existingKunde == null)
            {
                return NotFound();
            }

            // Oppdaterer eksisterende kundeinformasjon.
            existingKunde.Fornavn = kunde.Fornavn;
            existingKunde.Etternavn = kunde.Etternavn;
            existingKunde.Telefon = kunde.Telefon;
            existingKunde.Email = kunde.Email;
            existingKunde.Adresse = kunde.Adresse;
            existingKunde.Registrert = kunde.Registrert;

            nstedDbContext.SaveChanges();

            return RedirectToAction("List");
        }

        // Behandler forespørselen for å slette en kunde.
        public IActionResult Delete(int id)
        {
            var kunde = nstedDbContext.Kunder.FirstOrDefault(k => k.KundeId == id);

            if (kunde != null)
            {
                nstedDbContext.Kunder.Remove(kunde);
                nstedDbContext.SaveChanges();
            }

            return RedirectToAction("List");
        }

        // Viser en liste over alle kunder.
        public IActionResult List()
        {
            var kunder = nstedDbContext.Kunder.ToList();
            return View(kunder);
        }
    }
}
