using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Microsoft.AspNetCore.Identity;
using LosCasaAngular.DAL;


var builder = WebApplication.CreateBuilder(args);


// Database
builder.Services.AddDbContext<ListingDbContext>(options => {
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:ListingDbContextConnection"]);
});

var loggerConfiguration = new LoggerConfiguration()
    .MinimumLevel.Information() // Levels : Trace < Information < Warning < Error < Fatal
    .WriteTo.File($"Logs/app_{DateTime.Now:yyyyMMdd_HHmmss}.log");

loggerConfiguration.Filter.ByExcluding(e => e.Properties.TryGetValue("SourceContext", out var value) && e.Level == LogEventLevel.Information &&
    e.MessageTemplate.Text.Contains("Executed DbCommand"));

var logger = loggerConfiguration.CreateLogger();
builder.Logging.AddSerilog(logger);

builder.Services.AddScoped<InterListingRepository, ListingRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseStaticFiles();

//app.UseSession();
//app.UseAuthentication();
//app.UseAuthorization();

//app.MapRazorPages();

//app.UseAuthentication();


//app.MapControllerRoute(
//    name: "default",
//    pattern: "{Controllers}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
