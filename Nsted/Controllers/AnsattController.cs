using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nsted.Data;
using Nsted.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Nsted.Controllers
{
    [Authorize]
    public class AnsattController : Controller
    {
        private readonly NstedDbContext nstedDbContext;
        private readonly ILogger<AnsattController> _logger;

        public AnsattController(NstedDbContext nstedDbContext, ILogger<AnsattController> logger)
        {
            this.nstedDbContext = nstedDbContext;
            this._logger = logger; // Assign logger
        }

        public IActionResult Registrering()
        {
            var customers = nstedDbContext.Kunder.ToList(); // Get the list of customers
            ViewBag.CustomerList = customers; // Pass the list to the view
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

        public IActionResult NyKundeOgService(Kunde kunde, ServiceSkjema serviceSkjema)
        {
            nstedDbContext.Kunder.Add(kunde);
            nstedDbContext.SaveChanges();

            // Ensure that the KundeId is set for the serviceSkjema
            serviceSkjema.KundeId = kunde.KundeId;

            nstedDbContext.ServiceSkjemas.Add(serviceSkjema);
            nstedDbContext.SaveChanges();

            return RedirectToAction("ListServiceSkjema");
        }



        [HttpPost]
        public IActionResult HandleFormSubmission(Kunde kunde, Registrering registrering)
        {

            nstedDbContext.Kunder.Add(kunde);
            nstedDbContext.SaveChanges();

            registrering.KundeId = kunde.KundeId;
            nstedDbContext.Registreringer.Add(registrering);
            nstedDbContext.SaveChanges();

            return RedirectToAction("ListRegistrering");
        }

        [HttpPost]
        public IActionResult CreateServiceSkjema(ServiceSkjema serviceSkjema, int kundeId) // Changed parameter to directly use kundeId
        {
            try
            {
                if (kundeId <= 0)
                {
                    throw new InvalidOperationException("Invalid KundeId");
                }

                nstedDbContext.ServiceSkjemas.Add(serviceSkjema); // Add new ServiceSkjema
                nstedDbContext.SaveChanges(); // Save changes

                return RedirectToAction("ListServiceSkjema"); // Redirect to list view
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating ServiceSkjema");
                // Handle error appropriately, maybe redirect to an error view
                return View("Error");
            }
        }


        public IActionResult Delete(int id)
        {
            var registrering = nstedDbContext.Registreringer.FirstOrDefault(r => r.RegistreringId == id);

            if (registrering != null)
            {
                var kundeId = registrering.KundeId;
                var kunde = nstedDbContext.Kunder.FirstOrDefault(k => k.KundeId == kundeId);

                nstedDbContext.Registreringer.Remove(registrering);

                if (kunde != null)
                {
                    nstedDbContext.Kunder.Remove(kunde);
                }

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

        public IActionResult OversiktServiceSkjema(int id)
        {
            var serviceSkjema = nstedDbContext.ServiceSkjemas
                                .Include(s => s.Kunde)
                                .FirstOrDefault(s => s.ServiceSkjemaId == id);

            if (serviceSkjema == null)
            {
                return NotFound();
            }

            return View(serviceSkjema);
        }
        [HttpPost]
        public IActionResult UpdateServiceSkjema(ServiceSkjema updatedServiceSkjema)
        {
            ModelState.Remove("Kunde");
            if (!ModelState.IsValid)
            {
                return View("OversiktServiceSkjema", updatedServiceSkjema);
            }

            var serviceSkjema = nstedDbContext.ServiceSkjemas
                                      .FirstOrDefault(k => k.ServiceSkjemaId == updatedServiceSkjema.ServiceSkjemaId);

            if (serviceSkjema == null)
            {
                return NotFound($"ServiceSkjema with ID {updatedServiceSkjema.ServiceSkjemaId} not found.");
            }

            // Check and update each property
            if (updatedServiceSkjema.MottattDato != serviceSkjema.MottattDato)
                serviceSkjema.MottattDato = updatedServiceSkjema.MottattDato;

            if (updatedServiceSkjema.OrdreNr != serviceSkjema.OrdreNr)
                serviceSkjema.OrdreNr = updatedServiceSkjema.OrdreNr;

            if (updatedServiceSkjema.ProduktType != serviceSkjema.ProduktType)
                serviceSkjema.ProduktType = updatedServiceSkjema.ProduktType;

            if (updatedServiceSkjema.ÅrsModell != serviceSkjema.ÅrsModell)
                serviceSkjema.ÅrsModell = updatedServiceSkjema.ÅrsModell;

            if (updatedServiceSkjema.Servicetype != serviceSkjema.Servicetype)
                serviceSkjema.Servicetype = updatedServiceSkjema.Servicetype;

            if (updatedServiceSkjema.SerieNummer != serviceSkjema.SerieNummer)
                serviceSkjema.SerieNummer = updatedServiceSkjema.SerieNummer;

            if (updatedServiceSkjema.AvtaltMedKunden != serviceSkjema.AvtaltMedKunden)
                serviceSkjema.AvtaltMedKunden = updatedServiceSkjema.AvtaltMedKunden;

            if (updatedServiceSkjema.Reparasjonsbeskrivelse != serviceSkjema.Reparasjonsbeskrivelse)
                serviceSkjema.Reparasjonsbeskrivelse = updatedServiceSkjema.Reparasjonsbeskrivelse;

            if (updatedServiceSkjema.BrukteDeler != serviceSkjema.BrukteDeler)
                serviceSkjema.BrukteDeler = updatedServiceSkjema.BrukteDeler;

            if (updatedServiceSkjema.Arbeidstimer != serviceSkjema.Arbeidstimer)
                serviceSkjema.Arbeidstimer = updatedServiceSkjema.Arbeidstimer;

            if (updatedServiceSkjema.FerdigstiltDato != serviceSkjema.FerdigstiltDato)
                serviceSkjema.FerdigstiltDato = updatedServiceSkjema.FerdigstiltDato;

            if (updatedServiceSkjema.ReturDeler != serviceSkjema.ReturDeler)
                serviceSkjema.ReturDeler = updatedServiceSkjema.ReturDeler;

            if (updatedServiceSkjema.ForsendelsesMåte != serviceSkjema.ForsendelsesMåte)
                serviceSkjema.ForsendelsesMåte = updatedServiceSkjema.ForsendelsesMåte;

            if (updatedServiceSkjema.KundeSignatur != serviceSkjema.KundeSignatur)
                serviceSkjema.KundeSignatur = updatedServiceSkjema.KundeSignatur;

            if (updatedServiceSkjema.MekanikerSignatur != serviceSkjema.MekanikerSignatur)
                serviceSkjema.MekanikerSignatur = updatedServiceSkjema.MekanikerSignatur;


            nstedDbContext.SaveChanges();

            return RedirectToAction("ListServiceSkjema");
        }

        public IActionResult FullførService(int id)
        {
            var serviceSkjema = nstedDbContext.ServiceSkjemas.Find(id);
            if (serviceSkjema == null)
            {
                // Håndter feil hvis serviceskjema ikke finnes
                return NotFound();
            }
            var fullførtOrdre = new FullførtOrdre
            {
                ServiceSkjemaId = serviceSkjema.ServiceSkjemaId,
                KundeId = serviceSkjema.KundeId,
                MottattDato = serviceSkjema.MottattDato,
                OrdreNr = serviceSkjema.OrdreNr,
                ProduktType = serviceSkjema.ProduktType,
                ÅrsModell = serviceSkjema.ÅrsModell,
                Servicetype = serviceSkjema.Servicetype,
                SerieNummer = serviceSkjema.SerieNummer,
                AvtaltMedKunden = serviceSkjema.AvtaltMedKunden,
                Reparasjonsbeskrivelse = serviceSkjema.Reparasjonsbeskrivelse,
                BrukteDeler = serviceSkjema.BrukteDeler,
                Arbeidstimer = serviceSkjema.Arbeidstimer,
                FerdigstiltDato = serviceSkjema.FerdigstiltDato,
                ReturDeler = serviceSkjema.ReturDeler,
                ForsendelsesMåte = serviceSkjema.ForsendelsesMåte,
                KundeSignatur = serviceSkjema.KundeSignatur,
                MekanikerSignatur = serviceSkjema.MekanikerSignatur
            };

            // Lagre fullført ordre i databasen
            nstedDbContext.FullførteOrdrer.Add(fullførtOrdre);
            nstedDbContext.SaveChanges();

            // Slett servicen fra serviceskjema-databasen
            nstedDbContext.ServiceSkjemas.Remove(serviceSkjema);
            nstedDbContext.SaveChanges();

            // Du kan returnere en passende respons, for eksempel en bekreftelsesmelding eller redirect til en annen visning
            return RedirectToAction("ListFullførteOrdrer"); // Redirect tilbake til oversikten over serviceskjema
        }


        public IActionResult ListFullførteOrdrer()
        {
            List<FullførtOrdre> fullførteOrdrer = nstedDbContext.FullførteOrdrer.ToList();
            return View(fullførteOrdrer);
        }

    }
}

