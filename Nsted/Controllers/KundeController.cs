using Microsoft.AspNetCore.Mvc;
using Nsted.Data;
using Nsted.Models;

namespace Nsted.Controllers
{
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

            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult List()
        {
            return View();
        }
    }
}
