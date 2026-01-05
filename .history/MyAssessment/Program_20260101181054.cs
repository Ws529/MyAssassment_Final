using MyAssessment.Services;

var builder = WebApplication.CreateBuilder(args);

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
