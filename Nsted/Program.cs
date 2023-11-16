using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Nsted.Data;
using Nsted.Services;

var builder = WebApplication.CreateBuilder(args);

// Build configuration
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultConnection");

// Register the DbContext as a service
builder.Services.AddDbContext<NstedDbContext>(options =>
options.UseMySql(connectionString, new MySqlServerVersion(new Version(10, 5, 9))));

// Register UserService
builder.Services.AddScoped<UserService>(); // Add this line

builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
