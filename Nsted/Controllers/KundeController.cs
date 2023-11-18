using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nsted.Data;
using Nsted.Models;
using System.Linq;

namespace Nsted.Controllers
{
    [Authorize]
    public class KundeController : Controller
    {
        private readonly NstedDbContext nstedDbContext;

        public KundeController(NstedDbContext nstedDbContext)
        {
            this.nstedDbContext = nstedDbContext;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Kunde kunde)
        {
            nstedDbContext.Add(kunde);
            nstedDbContext.SaveChanges();

            return RedirectToAction("List");
        }

        public IActionResult Edit(int id)
        {
            var kunde = nstedDbContext.Kunder.FirstOrDefault(k => k.KundeId == id);

            if (kunde == null)
            {
                return NotFound();
            }

            return View(kunde);
        }

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

            existingKunde.Fornavn = kunde.Fornavn;
            existingKunde.Etternavn = kunde.Etternavn;
            existingKunde.Telefon = kunde.Telefon;
            existingKunde.Email = kunde.Email;
            existingKunde.Adresse = kunde.Adresse;
            existingKunde.Registrert = kunde.Registrert;
            // Update other fields as necessary

            nstedDbContext.SaveChanges();

            return RedirectToAction("List");
        }

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

        public IActionResult List()
        {
            List<Kunde> kunder = nstedDbContext.Kunder.ToList();
            return View(kunder);
        }
    }
}
