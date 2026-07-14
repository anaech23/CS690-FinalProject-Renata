using HarlowStTattooStudio;
using HarlowStTattooStudio.Data;
using HarlowStTattooStudio.Menus;
using HarlowStTattooStudio.Services;
using Microsoft.Extensions.DependencyInjection;

// Setup DI Container
var services = new ServiceCollection();

// Register StudioData as a singleton using Load()
services.AddSingleton<StudioData>(provider => StudioData.Load());

// Register services
services.AddSingleton<ClientService>();
services.AddSingleton<ArtistService>();
services.AddSingleton<AppointmentService>();
services.AddSingleton<LeaveService>();
services.AddSingleton<PaymentService>();
services.AddSingleton<ReportService>();

// Register Menus
services.AddSingleton<ClientMenu>();
services.AddSingleton<AppointmentMenu>();
services.AddSingleton<LeaveMenu>();
services.AddSingleton<PaymentMenu>();
services.AddSingleton<ReportMenu>();

// Register App class
services.AddSingleton<StudioApp>();

// Build service provider
var provider = services.BuildServiceProvider();

// Run the application
var app = provider.GetRequiredService<StudioApp>();
app.Run();