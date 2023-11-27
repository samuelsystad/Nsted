using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nsted.Services;
using Nsted.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using Microsoft.Extensions.Primitives;

namespace Nsted
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Konfigurerer tjenester som brukes i applikasjonen.
        public void ConfigureServices(IServiceCollection services)
        {
            // Legger til MVC-rammeverket for kontroller og visninger.
            services.AddControllersWithViews();

            // Legger til UserService som en Scoped-tjeneste.
            services.AddScoped<UserService>();

            // Konfigurerer og legger til Entity Framework DbContext med MySQL-database.
            services.AddDbContext<NstedDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
                    ServerVersion.AutoDetect(Configuration.GetConnectionString("DefaultConnection"))));

            // Legger til autentisering med informasjonskapsler (cookies).
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                });

            // Legger til autorisasjonstjenester.
            services.AddAuthorization();
        }

        // Konfigurerer HTTP-request-pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

           
                app.Use(async (context, next) =>
                {
                    // Legger til sikkerhetsheadere for beskyttelse mot webangrep.
                    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                    context.Response.Headers.Add("X-Frame-Options", "DENY");
                    context.Response.Headers.Add("Referrer-Policy", "no-referrer");
                    context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
                    context.Response.Headers.Add(
                        "Content-Security-Policy",
                        "default-src 'self'; " +
                        "img-src 'self'; " +
                        "font-src 'self'; " +
                        "style-src 'self'; " +
                        "script-src 'self'; " +
                        "frame-src 'self'; " +
                        "connect-src 'self';");
                    await next();
                });

                // Håndterer feil og unntak, og omdiriger til feilsiden.
                app.UseExceptionHandler("/Home/Error");

                // Aktiverer HSTS (HTTP Strict Transport Security) for sikrere kommunikasjon over HTTPS.
                app.UseHsts();
           
            
                // Viser detaljerte feilmeldinger under utviklingsmiljøet.
                app.UseDeveloperExceptionPage();
            

            // Utfører HTTPS-omdirigering.
            app.UseHttpsRedirection();

            // Aktiverer støtte for statiske filer som CSS, JavaScript, bilder, etc.
            app.UseStaticFiles();

            // Setter opp ruting for applikasjonen.
            app.UseRouting();

            // Aktiverer autentisering og autorisasjon.
            app.UseAuthentication();
            app.UseAuthorization();

            // Konfigurerer endepunkter for MVC-kontroller og aksjoner.
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
