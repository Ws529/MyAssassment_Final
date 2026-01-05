using MyAssessment.Services;
using System.Net;
using System.Net.NetworkInformation;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Prefer explicit URLs and Kestrel ListenAnyIP to accept requests from any active network interface
builder.WebHost.UseUrls("http://0.0.0.0:8000");
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(8000); // HTTP on port 8000
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

// Print helpful access information showing local IPv4 addresses
IEnumerable<string> GetLocalIPv4Addresses()
{
    return NetworkInterface.GetAllNetworkInterfaces()
        .Where(ni => ni.OperationalStatus == OperationalStatus.Up && ni.NetworkInterfaceType != NetworkInterfaceType.Loopback)
        .SelectMany(ni => ni.GetIPProperties().UnicastAddresses)
        .Select(u => u.Address)
        .Where(a => a != null && a.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork && !IPAddress.IsLoopback(a))
        .Select(a => a.ToString())
        .Distinct();
}

var ips = GetLocalIPv4Addresses().ToList();
if (ips.Any())
{
    Console.WriteLine("Aplikasi siap! Akses aplikasi lewat salah satu alamat berikut:");
    foreach (var ip in ips)
    {
        Console.WriteLine($" - http://{ip}:8000");
    }
    Console.WriteLine($"Jika menggunakan HP, coba buka http://{ips.First()}:8000");
}
else
{
    Console.WriteLine("Aplikasi siap! Jika menggunakan HP, buka: http://[IP-LAPTOP-KAMU]:8000");
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
