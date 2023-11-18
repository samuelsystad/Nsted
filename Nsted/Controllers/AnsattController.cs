using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nsted.Data;
using Nsted.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Nsted.Controllers
{ 
    [Authorize]
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

        public IActionResult Arbeidsdokument()
        {
            return View();
        }

        public IActionResult NyService()
        {
            var customers = nstedDbContext.Kunder.ToList(); // Get the list of customers
            ViewBag.CustomerList = customers; // Pass the list to the view
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

        [HttpPost]
        public IActionResult CreateServiceSkjema(ServiceSkjema serviceSkjema)
        {
            
            
                nstedDbContext.ServiceSkjemas.Add(serviceSkjema);
                nstedDbContext.SaveChanges();
                return RedirectToAction("ListServiceSkjema"); // Redirect after successful save
            }

      


        public IActionResult Delete(int id)
        {
            // Find the registrering by ID
            var registrering = nstedDbContext.Registreringer.FirstOrDefault(r => r.RegistreringId == id);

            if (registrering != null)
            {
                // Assuming each registrering is associated with a kunde, 
                // and you have a foreign key or navigation property setup
                var kundeId = registrering.KundeId; // Replace with the correct property if different
                var kunde = nstedDbContext.Kunder.FirstOrDefault(k => k.KundeId == kundeId);

                // Remove registrering
                nstedDbContext.Registreringer.Remove(registrering);

                // Check if the associated kunde exists and remove it
                if (kunde != null)
                {
                    nstedDbContext.Kunder.Remove(kunde);
                }

                // Save changes to the database
                nstedDbContext.SaveChanges();
            }

            return RedirectToAction("ListRegistrering"); // Redirect to the list view
        }


        public IActionResult ListRegistrering()
        {
            List<Registrering> registreringer = nstedDbContext.Registreringer.Include(r => r.Kunde).ToList();
            return View(registreringer);
        }

        public IActionResult ListServiceSkjema()
        {
            List<ServiceSkjema> serviceskjemas = nstedDbContext.ServiceSkjemas.Include(s => s.Kunde).ToList();
            var customers = nstedDbContext.Kunder.ToList();
            ViewBag.CustomerList = customers;

            return View(serviceskjemas);
        }

    }
}


