using MyAssessment.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddRazorPages();
builder.Services.AddSingleton<MongoDbService>();
builder.Services.AddSingleton<DatabaseSeeder>();
builder.Services.AddControllers();

var app = builder.Build();

// Run database seeder on startup
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
    await seeder.CheckAndSeedAsync();
}

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
