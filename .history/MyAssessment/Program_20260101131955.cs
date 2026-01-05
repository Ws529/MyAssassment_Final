using MyAssessment.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddRazorPages();
builder.Services.AddSingleton<MongoDbService>();
builder.Services.AddControllers();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.MapControllers();

app.Run();