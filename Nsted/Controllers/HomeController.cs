using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nsted.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Nsted.Controllers
{
    // Autorisasjon kreves for å få tilgang til metoder i denne kontrolleren.
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Viser hjemmesiden.
        public IActionResult Homepage()
        {
            return View();
        }

        // Viser siden for lagerbeholdning.
        public IActionResult Lagerbeholdning()
        {
            return View();
        }

        // Viser siden for arbeidsdokument.
        public IActionResult Arbeidsdokument()
        {
            return View();
        }

        // Behandler feil som oppstår på nettstedet.
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Returnerer feilsiden med informasjon om feilen.
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
