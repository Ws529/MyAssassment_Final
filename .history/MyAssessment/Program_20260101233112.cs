using MyAssessment.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel to listen on all IPs
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(7000); // HTTP on port 7000
});

// Add services
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<MongoDbService>();
builder.Services.AddSingleton<DatabaseSeeder>();

var app = builder.Build();

// Run database seeder on startup
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
    await seeder.CheckAndSeedAsync();
}

app.UseStaticFiles();
app.UseRouting();

// MVC Routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Dashboard}/{id?}");

// API Controllers
app.MapControllers();

app.Run();
