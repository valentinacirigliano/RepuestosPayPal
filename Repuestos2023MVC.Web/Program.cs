using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repuestos2023.DataLayer.Data;
using Repuestos2023.DataLayer.Repository.Interfaces;
using Repuestos2023.DataLayer.Repository;
using Repuestos2023.Utilities;
using Microsoft.Extensions.Options;
using PayPalCheckoutSdk.Core;
using Repuestos2023MVC.Web.Areas.PayPal;
using Repuestos2023.DataLayer.Servicios.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") 
    ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

// Add services to the container.
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30); // Ajusta el tiempo de expiración según tus necesidades.
});

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options
    .UseSqlServer(builder.Configuration
    .GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddAntiforgery(options =>
{
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<IEmailSender, EmailSender>();
builder.Services.AddScoped<IPayPalService, PayPalService>();

builder.Services.AddRazorPages();
// Configuración de PayPal

var paypalSettings = new PayPalSettings
{
	ClientId = "AfC1YUOTZzy-umuWUMzfroDDGRWohqt_p2IUxnjcgkRUNymCxfprY3Xck4U0TUvgBBKVivYW4p1eHbsW",
	ClientSecret = "EKxsZB1feFXSONi9Xnoy4X0w4Hwxs_QCysrfx2HWYtw86YPz0Ce__nGadjr7bDTFukeNeqiXOfBUoLUR"
};
builder.Services.AddSingleton(paypalSettings);
builder.Services.Configure<PayPalSettings>(builder.Configuration.GetSection("PayPalSettings"));
builder.Services.AddSingleton(serviceProvider =>
{
    var options = serviceProvider.GetRequiredService<IOptions<PayPalSettings>>().Value;
    var environment = new SandboxEnvironment(options.ClientId, options.ClientSecret);
    return new PayPalHttpClient(environment);
});
builder.Services.AddHttpClient<PayPalHttpClient>(client =>
{
    var options = builder.Configuration.GetSection("PayPalSettings").Get<PayPalSettings>();
    var environment = new SandboxEnvironment(options.ClientId, options.ClientSecret);
    client.BaseAddress = new Uri("https://api.sandbox.paypal.com"); // Para entorno de sandbox
})
.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.Use(async (context, next) =>
{
	context.Response.Headers.Add("Content-Security-Policy", "script-src 'self' https://*.paypal.com 'unsafe-inline' 'unsafe-eval'");
	await next();
});
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();
app.UseSession();


app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "/{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();