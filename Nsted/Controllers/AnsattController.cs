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
            this._logger = logger; // Tildel loggeren
        }

        // Vis registreringsskjemaet for ansatte
        public IActionResult Registrering()
        {
            var customers = nstedDbContext.Kunder.ToList(); // Hent listen over kunder
            ViewBag.CustomerList = customers; // Send listen til visningen
            return View();
        }

        // Vis eksisterende kundeinformasjon
        public IActionResult EksisterendeKunde()
        {
            var customers = nstedDbContext.Kunder.ToList(); // Hent listen over kunder
            ViewBag.CustomerList = customers; // Send listen til visningen
            return View();
        }

        // Vis arbeidsdokumentsiden
        public IActionResult Arbeidsdokument()
        {
            return View();
        }

        // Vis skjema for ny service
        public IActionResult NyService()
        {
            var customers = nstedDbContext.Kunder.ToList(); // Hent listen over kunder
            ViewBag.CustomerList = customers; // Send listen til visningen
            return View();
        }

        // Vis skjema for ny kundeservice
        public IActionResult NyKundeService()
        {
            return View();
        }

        // Håndter innsending av skjema for ny kunde og registrering
        [HttpPost]
        public IActionResult HandleFormSubmission(Kunde kunde, Registrering registrering)
        {
            nstedDbContext.Kunder.Add(kunde);
            nstedDbContext.SaveChanges(); // Lagre kunden for å få KundeId

            registrering.KundeId = kunde.KundeId;
            nstedDbContext.Registreringer.Add(registrering);
            nstedDbContext.SaveChanges();

            return RedirectToAction("ListRegistrering"); // Omdiriger til suksesssiden
        }

        // Håndter innsending av skjema for eksisterende kunde og registrering
        [HttpPost]
        public IActionResult HandleFormSubmission2(Registrering registrering, int kundeId)
        {
            if (kundeId <= 0)
            {
                throw new InvalidOperationException("Ugyldig KundeId");
            }
            registrering.KundeId = kundeId;
            nstedDbContext.Registreringer.Add(registrering); // Legg til nytt ServiceSkjema
            nstedDbContext.SaveChanges(); // Lagre endringer

            return RedirectToAction("ListRegistrering"); // Omdiriger til visning av listen
        }

        // Opprett et nytt servicekjema
        public IActionResult CreateServiceSkjema(ServiceSkjema serviceSkjema, int kundeId)
        {
            if (kundeId <= 0)
            {
                throw new InvalidOperationException("Ugyldig KundeId");
            }
            serviceSkjema.KundeId = kundeId;
            nstedDbContext.ServiceSkjemas.Add(serviceSkjema);
            nstedDbContext.SaveChanges();
            return RedirectToAction("ListServiceSkjema"); // Omdiriger etter vellykket lagring
        }

        // Generer en unik ordrenummer
        public IActionResult GenerateUniqueOrderNumber()
        {
            int uniqueOrderNumber;
            do
            {
                uniqueOrderNumber = new Random().Next(1000000); // Generer et tilfeldig nummer
            }
            while (nstedDbContext.Registreringer.Any(s => s.OrdreNr == uniqueOrderNumber)); // Sjekker om det allerede finnes i databasen

            return Json(new { uniqueOrderNumber = uniqueOrderNumber });
        }

        // Behandle innsending av skjema for ny kunde og service
        public IActionResult NyKundeOgService(Kunde kunde, ServiceSkjema serviceSkjema)
        {
            nstedDbContext.Kunder.Add(kunde);
            nstedDbContext.SaveChanges();

            // Forsikre deg om at KundeId er satt for serviceSkjema
            serviceSkjema.KundeId = kunde.KundeId;

            nstedDbContext.ServiceSkjemas.Add(serviceSkjema);
            nstedDbContext.SaveChanges();

            return RedirectToAction("ListServiceSkjema");
        }

        // Slett registrering og tilhørende kundeinformasjon
        public IActionResult Delete(int id)
        {
            var registrering = nstedDbContext.Registreringer.FirstOrDefault(r => r.RegistreringId == id);

            if (registrering != null)
            {
                var kundeId = registrering.KundeId;
                var kunde = nstedDbContext.Kunder.FirstOrDefault(k => k.KundeId == kundeId);

                nstedDbContext.Registreringer.Remove(registrering);

                nstedDbContext.SaveChanges();
            }

            return RedirectToAction("ListRegistrering");
        }

        // Sletter Registreringsskjema
        public IActionResult DeleteRegistrering(int id)
        {
            var registrering = nstedDbContext.Registreringer.FirstOrDefault(r => r.RegistreringId == id);

            if (registrering != null)
            {
                nstedDbContext.Registreringer.Remove(registrering);
                nstedDbContext.SaveChanges();
            }

            return RedirectToAction("ListRegistrering");
        }


        // Sletter serviceskjema
        public IActionResult DeleteServiceskjema(int id)
        {
            var serviceSkjema = nstedDbContext.ServiceSkjemas.FirstOrDefault(s => s.ServiceSkjemaId == id);

            if (serviceSkjema != null)
            {
                nstedDbContext.ServiceSkjemas.Remove(serviceSkjema);

                nstedDbContext.SaveChanges();
            }

            return RedirectToAction("ListServiceSkjema");
        }


        // Vis liste over registreringer
        public IActionResult ListRegistrering()
        {
            var registreringer = nstedDbContext.Registreringer.Include(r => r.Kunde).ToList();
            return View(registreringer);
        }

        // Vis liste over servicekjemaer
        public IActionResult ListServiceSkjema()
        {
            List<ServiceSkjema> serviceskjemas = nstedDbContext.ServiceSkjemas.Include(s => s.Kunde).ToList();
            var customers = nstedDbContext.Kunder.ToList();
            ViewBag.CustomerList = customers;

            return View(serviceskjemas);
        }

        // Vis oversikt over et servicekjema
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

        // Vis oversikt over en registrering
        public IActionResult OversiktRegistreringSkjema(int id)
        {
            var registreringSkjema = nstedDbContext.Registreringer
                                    .Include(r => r.Kunde)
                                    .FirstOrDefault(r => r.RegistreringId == id);

            if (registreringSkjema == null)
            {
                return NotFound();
            }

            return View(registreringSkjema);
        }

        // Vis oversikt over en fullført ordre
        public IActionResult OversiktFullførteOrdreSide(int id)
        {
            var fullførtOrdre = nstedDbContext.FullførteOrdrer
                .FirstOrDefault(s => s.ServiceSkjemaId == id);

            if (fullførtOrdre == null)
            {
                return NotFound();
            }

            return View(fullførtOrdre);
        }

        // Håndter oppdatering av registrering
        [HttpPost]
        public IActionResult UpdateRegistrering(Registrering updatedRegistrering)
        {
            try
            {
                ModelState.Remove("Kunde");
                if (!ModelState.IsValid)
                {
                    // Returner til redigeringssiden hvis modelltilstanden er ugyldig
                    return View("OversiktRegistreringSkjema", updatedRegistrering);
                }

                var registrering = nstedDbContext.Registreringer
                                                  .FirstOrDefault(r => r.RegistreringId == updatedRegistrering.RegistreringId);

                if (registrering == null)
                {
                    return NotFound($"Registrering with ID {updatedRegistrering.RegistreringId} not found.");
                }

                // Sjekk og oppdater hver egenskap
                if (updatedRegistrering.BookingTilUke != registrering.BookingTilUke)
                    registrering.BookingTilUke = updatedRegistrering.BookingTilUke;

                if (updatedRegistrering.HenvendelseMottatt != registrering.HenvendelseMottatt)
                    registrering.HenvendelseMottatt = updatedRegistrering.HenvendelseMottatt;

                if (updatedRegistrering.CaseFerdig != registrering.CaseFerdig)
                    registrering.CaseFerdig = updatedRegistrering.CaseFerdig;

                if (updatedRegistrering.ProduktType != registrering.ProduktType)
                    registrering.ProduktType = updatedRegistrering.ProduktType;

                if (updatedRegistrering.Feilbeskrivelse != registrering.Feilbeskrivelse)
                    registrering.Feilbeskrivelse = updatedRegistrering.Feilbeskrivelse;

                if (updatedRegistrering.AvtaltLevering != registrering.AvtaltLevering)
                    registrering.AvtaltLevering = updatedRegistrering.AvtaltLevering;

                if (updatedRegistrering.ProduktMottatt != registrering.ProduktMottatt)
                    registrering.ProduktMottatt = updatedRegistrering.ProduktMottatt;

                if (updatedRegistrering.AvtaltFerdigstillelseInnen != registrering.AvtaltFerdigstillelseInnen)
                    registrering.AvtaltFerdigstillelseInnen = updatedRegistrering.AvtaltFerdigstillelseInnen;

                if (updatedRegistrering.ServiceFerdig != registrering.ServiceFerdig)
                    registrering.ServiceFerdig = updatedRegistrering.ServiceFerdig;

                if (updatedRegistrering.AntallTimerUtført != registrering.AntallTimerUtført)
                    registrering.AntallTimerUtført = updatedRegistrering.AntallTimerUtført;

                if (updatedRegistrering.OrdreNr != registrering.OrdreNr)
                    registrering.OrdreNr = updatedRegistrering.OrdreNr;

                if (updatedRegistrering.ServiceSkjema != registrering.ServiceSkjema)
                    registrering.ServiceSkjema = updatedRegistrering.ServiceSkjema;


                nstedDbContext.SaveChanges();

                return View("OversiktRegistreringSkjema", updatedRegistrering);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Feil i UpdateRegistrering");
                // Håndter feilen på riktig måte, for eksempel returner en feilmelding eller modell til visningen
                return View("Error", new ErrorViewModel { ErrorMessage = "En feil oppstod under oppdatering av registreringen." });
            }
        }

        [HttpPost]
        public IActionResult UpdateServiceSkjema(ServiceSkjema updatedServiceSkjema)
        {
            ModelState.Remove("Kunde");
            if (!ModelState.IsValid)
            {
                // Håndter ugyldige data og vis skjemaet på nytt
                return View("OversiktServiceSkjema", updatedServiceSkjema);
            }

            // Finn det eksisterende service-skjemaet basert på ID
            var serviceSkjema = nstedDbContext.ServiceSkjemas
                                      .FirstOrDefault(k => k.ServiceSkjemaId == updatedServiceSkjema.ServiceSkjemaId);

            if (serviceSkjema == null)
            {
                // Hvis skjemaet ikke finnes, returner en feilmelding
                return NotFound($"ServiceSkjema med ID {updatedServiceSkjema.ServiceSkjemaId} ble ikke funnet.");
            }

            // Oppdater feltene i service-skjemaet med de nye verdiene fra updatedServiceSkjema
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


            // Lagre endringene i databasen
            nstedDbContext.SaveChanges();

            // Vis en oversikt over service-skjemaet etter oppdateringen
            return View("OversiktServiceSkjema", updatedServiceSkjema);
        }

        public IActionResult FullførService(int id)
        {
            // Finn service-skjemaet basert på ID
            var serviceSkjema = nstedDbContext.ServiceSkjemas.Find(id);
            if (serviceSkjema == null)
            {
                // Håndter feil hvis service-skjemaet ikke finnes
                return NotFound();
            }
            // Opprett en fullført ordre basert på service-skjemaet
            var fullførtOrdre = new FullførtOrdre
            {
                // Kopier data fra service-skjemaet til fullført ordre
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

            // Legg til fullført ordre i databasen
            nstedDbContext.FullførteOrdrer.Add(fullførtOrdre);
            nstedDbContext.SaveChanges();

            // Slett service-skjemaet fra databasen
            nstedDbContext.ServiceSkjemas.Remove(serviceSkjema);
            nstedDbContext.SaveChanges();

            // Redirect til oversikten over fullførte ordrer
            return RedirectToAction("ListFullførteOrdrer");
        }

        public IActionResult ListFullførteOrdrer()
        {
            // Hent en liste over fullførte ordrer fra databasen
            List<FullførtOrdre> fullførteOrdrer = nstedDbContext.FullførteOrdrer.ToList();
            return View(fullførteOrdrer);
        }
    }
}
