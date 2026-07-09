using Microsoft.Extensions.DependencyInjection;
using HarlowStTattooStudio;
using HarlowStTattooStudio.Services;

// Setup DI Container
var services = new ServiceCollection();

// Register services
services.AddSingleton<ClientService>();
services.AddSingleton<ArtistService>();
services.AddSingleton<AppointmentService>();
services.AddSingleton<LeaveService>();
services.AddSingleton<PaymentService>();
services.AddSingleton<ReportService>();

// Register App class
services.AddSingleton<StudioApp>();

// Build service provider
var provider = services.BuildServiceProvider();

// Run the application
var app = provider.GetRequiredService<StudioApp>();
app.Run();