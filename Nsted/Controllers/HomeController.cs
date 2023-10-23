using Microsoft.AspNetCore.Mvc;
using Nsted.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Nsted.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Homepage()
        {
            return View();
        }

        public IActionResult Service()
        {
            return View();
        }

        public IActionResult Lagerbeholdning()
        {
            return View();
        }

        public IActionResult Arbeidsdokument()
        {
            return View();
        }

        public IActionResult Registrering()
        {
            return View();
        }

        public IActionResult NyService()
        {
            return View();
        }

        public IActionResult UnderBehandling()
        {
            return View();
        }

        public IActionResult FerdigeOrdrer()
        {
            return View();
        }

<<<<<<< HEAD
        public IActionResult Login()
        {
            return View();
        }   

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
=======


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
>>>>>>> 48671de3a933751918323943604fd53103473eba
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}