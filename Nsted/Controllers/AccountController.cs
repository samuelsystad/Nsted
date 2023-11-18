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
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(UserLoginModel model)
    {
        if (ModelState.IsValid)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == model.Email);
            if (user != null && _userService.VerifyPassword(model.Password, user.PasswordHash))
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                // Add other claims as needed
            };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                // Redirect to the desired page (Homepage in this case)
                return RedirectToAction("Homepage", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }

        return View(model);
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register(UserLoginModel model)
    {
        if (ModelState.IsValid)
        {
            if (!_context.Users.Any(u => u.Email == model.Email))
            {
                var hashedPassword = _userService.HashPassword(model.Password);

                var newUser = new User { Email = model.Email, PasswordHash = hashedPassword };
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                // Optionally log the user in immediately after registration
                // Similar logic as in the Login method

                return RedirectToAction("Homepage", "Home");

            }
            else
            {
                ModelState.AddModelError("", "Email already in use.");
            }
        }

        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
}

