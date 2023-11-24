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


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins("http://localhost:44480") // The URL of your Angular app
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials();
        });
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
    DBInit.Seed(app);
}
app.UseStaticFiles();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios...
    app.UseHsts();
}


app.UseCors("AllowSpecificOrigin"); // Enable CORS using the policy you defined

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.MapControllerRoute(
//  name: "default",
//   pattern: "{Controllers}/{action=Index}/{id?}");


app.MapFallbackToFile("index.html"); ;

app.Run();
