using HarlowStTattooStudio.Services;

namespace HarlowStTattooStudio
{
    public class StudioApp(
        AppointmentService appointmentService,
        ArtistService artistService,
        ClientService clientService,
        LeaveService leaveService,
        PaymentService paymentService,
        ReportService reportService)
    {
        public void Run()
        {
            Console.Title = "HARLOW STREET TATTOO MANAGEMENT SYSTEM";

            ShowMainMenu();
        }

        private void ShowMainMenu()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("HARLOW STREET TATTOO MANAGEMENT SYSTEM");
                Console.WriteLine("--------------------------------");
                Console.WriteLine("Welcome! Please make a selection: ");
                Console.WriteLine("--------------------------------");
                Console.WriteLine("[1] Clients");
                Console.WriteLine("[2] Appointments");
                Console.WriteLine("[3] Artists");
                Console.WriteLine("[4] Payments");
                Console.WriteLine("[5] Reports");
                Console.WriteLine("[6] Exit");

                string menuChoice = Console.ReadLine();

                switch (menuChoice)
                {
                    case "1":
                        ShowClientsMenu();
                        break;
                    case "2":
                        ShowAppointmentsMenu();
                        break;
                    case "3":
                        ShowArtistsMenu();
                        break;
                    case "4":
                        ShowPaymentsMenu();
                        break;
                    case "5":
                        ShowReportsMenu();
                        break;
                    case "6":
                        running = false;
                        break;
                    default: Console.WriteLine("Invalid selection. Please try again.");
                        Console.ReadKey();
                        break;

                }
            }
        }

        private void ShowClientsMenu()
        {
            clientService.ShowClientsMenu();
        }

        private void ShowAppointmentsMenu()
        {
            bool backToMainMenu = false;
            while (!backToMainMenu)
            {
                Console.Clear();
                Console.WriteLine("--- APPOINTMENTS MENU ---");
                Console.WriteLine("[1] View Appointments");
                Console.WriteLine("[2] Add Appointment");
                Console.WriteLine("[3] Cancel Appointment");
                Console.WriteLine("[4] Back to Main Menu");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        var appointments = appointmentService.GetAll();
                        foreach (var a in appointments) { Console.WriteLine(a); }
                        {
                            Console.WriteLine($"ID: {a.AppointmentId}, Client ID: {a.ClientId}, Artist ID: {a.ArtistId}, Start: {a.AppointmentStart}, End: {a.AppointmentEnd}, Status: {a.Status}");
                        }
                        Console.WriteLine("Press any key to return to the Appointments Menu...");
                        Console.ReadKey();
                        break;

                    case "2":
                        // Gather information for a new appointment
                        Console.Write("Enter Client ID: ");
                        int clientId = int.Parse(Console.ReadLine());

                        Console.WriteLine("Enter Artist ID: ");
                        int artistId = int.Parse(Console.ReadLine());

                        Console.WriteLine("Enter Appointment Start (yyyy-MM-dd HH:mm): ");
                        DateTime appointmentStart = DateTime.Parse(Console.ReadLine());

                        Console.WriteLine("Enter Appointment End (yyyy-MM-dd HH:mm): ");
                        DateTime appointmentEnd = DateTime.Parse(Console.ReadLine());

                        // Create a new appointment
                        var newAppointment = new Appointment
                        {
                            ClientId = clientId,
                            ArtistId = artistId,
                            AppointmentStart = appointmentStart,
                            AppointmentEnd = appointmentEnd,
                            Status = AppointmentStatus.Scheduled
                        };
                        break;
                    case "3":
                        appointmentService.CancelAppointment();
                        break;
                    case "4":
                        backToMainMenu = true;
                        break;

                }
            }
        }
    }
}