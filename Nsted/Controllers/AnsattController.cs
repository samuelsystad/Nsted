using Microsoft.AspNetCore.Mvc;
using Nsted.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Nsted.Controllers
{
    public class AnsattController : Controller

    {
     
        public IActionResult Registrering()
        {
            return View();
        }
    }
}
