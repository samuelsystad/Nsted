using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nsted.Models;
using Nsted.Data;
using Nsted.Services;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

public class AccountController : Controller
{

    private readonly NstedDbContext _context;
    private readonly UserService _userService;

    public AccountController(NstedDbContext context, UserService userService)
    {
        _context = context;
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login()
    {
        // Vis innloggingsvisningen
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserLoginModel model)
    {
        if (ModelState.IsValid)
        {
            // Hent brukeren med e-posten fra databasen
            var user = _context.Users.FirstOrDefault(u => u.Email == model.Email);
            if (user != null && _userService.VerifyPassword(model.Password, user.PasswordHash))
            {
                // Opprett en Claims-principal for innlogging
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                // Logg inn brukeren
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                // Redirect til hjemmesiden etter vellykket innlogging
                return RedirectToAction("Homepage", "Home");
            }
            else
            {
                // Håndter feil innloggingsinformasjon
                ModelState.AddModelError(string.Empty, "Brukernavnet eller passordet er feil, vennligst prøv igjen.");
            }
        }

        return View(model);
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Register()
    {
        // Vis registreringsvisningen med et tomt registreringsskjema
        return View(new UserRegistrationModel());
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register(UserRegistrationModel model)
    {
        if (ModelState.IsValid)
        {
            // Sjekk om e-posten allerede eksisterer i databasen
            if (!_context.Users.Any(u => u.Email == model.Email))
            {
                // Hash passordet før lagring
                var hashedPassword = _userService.HashPassword(model.Password);
                var newUser = new User { Email = model.Email, PasswordHash = hashedPassword };

                // Legg til den nye brukeren i databasen
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                // Opprett en Claims-principal for innlogging
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, newUser.Email),
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                // Logg inn den nye brukeren
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                // Redirect til hjemmesiden etter vellykket registrering
                return RedirectToAction("Homepage", "Home");
            }
            else
            {
                // Håndter feil hvis e-posten allerede er i bruk
                ModelState.AddModelError("", "E-posten er allerede i bruk.");
            }
        }

        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        // Logg ut brukeren og redirect til innloggingssiden
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
}
