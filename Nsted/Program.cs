using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Nsted.Data;
using Nsted.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System;

var builder = WebApplication.CreateBuilder(args);

// Henter konfigurasjon fra appsettings.json eller andre konfigurasjonskilder.
var configuration = builder.Configuration;

// Henter tilkoblingsstrengen til databasen fra konfigurasjonen.
var connectionString = configuration.GetConnectionString("DefaultConnection");

// Legger til Entity Framework DbContext med MySQL-database.
builder.Services.AddDbContext<NstedDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(10, 5, 9))));

// Legger til UserService som en Scoped-tjeneste.
builder.Services.AddScoped<UserService>();

// Konfigurerer autentisering med informasjonskapsler (cookies).
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    });

// Legger til autorisasjonstjenester.
builder.Services.AddAuthorization();

// Legger til st�tte for Razor Pages.
builder.Services.AddRazorPages();

var app = builder.Build();


// Legger til sikkerhetsheadere for beskyttelse mot webangrep.
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
    
    await next();
});
// H�ndterer feil og unntak, og omdiriger til feilsiden.
app.UseExceptionHandler("/Home/Error");

// Aktiverer HSTS (HTTP Strict Transport Security) for sikrere kommunikasjon over HTTPS.
app.UseHsts();

// Utf�rer HTTPS-omdirigering.
app.UseHttpsRedirection();

// Aktiverer st�tte for statiske filer som CSS, JavaScript, bilder, etc.
app.UseStaticFiles();

// Setter opp ruting for applikasjonen.
app.UseRouting();

// Aktiverer autentisering og autorisasjon.
app.UseAuthentication();
app.UseAuthorization();

// Konfigurerer Razor Pages-ruter.
app.MapRazorPages();

// Konfigurerer standardkontrolleren og kontrollerens handlinger.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

// Starter applikasjonen.
app.Run();

