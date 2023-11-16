using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nsted.Data;
using Nsted.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Nsted.Controllers
{
    public class AnsattController : Controller
    {
        private readonly NstedDbContext nstedDbContext;

        public AnsattController(NstedDbContext nstedDbContext)
        {
            this.nstedDbContext = nstedDbContext;
        }

        public IActionResult Registrering()
        {
            return View();
        }

      


        [HttpPost]
        public IActionResult HandleFormSubmission(Kunde kunde, Registrering registrering)
        {

            // Legg til kunden i Kunder-tabellen
            nstedDbContext.Kunder.Add(kunde);
            nstedDbContext.SaveChanges(); // Lagre kunden først for å få KundeId

            // Sett KundeId i Registrering-objektet
            registrering.KundeId = kunde.KundeId;

            // Legg til registreringen i Registreringer-tabellen
            nstedDbContext.Registreringer.Add(registrering);
            nstedDbContext.SaveChanges();

            return RedirectToAction("ListRegistrering"); // Omdiriger til en suksessside

        }

        public IActionResult Delete(int id)
        {
            var registrering = nstedDbContext.Registreringer.FirstOrDefault(r => r.RegistreringId == id);

            if (registrering != null)
            {
                nstedDbContext.Registreringer.Remove(registrering);
                nstedDbContext.SaveChanges();
            }

            return RedirectToAction("ListRegistrering");
        }

        public IActionResult ListRegistrering()
        {
            List<Registrering> registreringer = nstedDbContext.Registreringer.Include(r => r.Kunde).ToList();
            return View(registreringer);
        }
    }
}


