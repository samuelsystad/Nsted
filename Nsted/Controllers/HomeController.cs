using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nsted.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Nsted.Controllers
{
    [Authorize]
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

        
        public IActionResult NyService()
        {
            return View();
        }

        public IActionResult FerdigeOrdrer()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
