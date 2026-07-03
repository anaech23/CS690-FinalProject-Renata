using HarlowStTattooStudio.DomainModel;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Linq;
using System;

namespace HarlowStTattooStudio
{
    internal class Program
    {
        // Initialize a static variable to keep track of the next ClientId, AppointmentId, and ArtistId to ensure unique identifiers for each entity
        static int nextClientId = 1;
        static int nextAppointmentId = 1;
        static int nextArtistId = 1;
        static int nextPaymentId = 1;
        static int nextLeaveId = 1;


        // In-memory lists to store data for Clients, Appointments, Artists, Payments, and Leaves
        static List<Client> clients = new List<Client>();

        static List<Appointment> appointments = new List<Appointment>();

        static List<Artist> artists = new List<Artist>()
        {
            new Artist { ArtistId = 1, ArtistFirstName = "Memo", ArtistLastName = "Ochoa" },
            new Artist { ArtistId = 2, ArtistFirstName = "Alex", ArtistLastName = "Morgan" },
            new Artist { ArtistId = 3, ArtistFirstName = "Harry", ArtistLastName = "Kane" },
            new Artist { ArtistId = 4, ArtistFirstName = "Scarlett", ArtistLastName = "Camberos" },
        };


        static List<Payment> payments = new List<Payment>();

        static List<Leave> leaves = new List<Leave>();


        static void Main(string[] args)
        {
            Console.Title = "HARLOW STREET TATTOO MANAGEMENT SYSTEM";

            ShowMainMenu();
        }

        static void ShowMainMenu()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
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
                        Console.WriteLine("Exiting the application...");
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void ShowClientsMenu()
        {
            bool inMenu = true;

            while (inMenu)
            {
                Console.Clear();
                Console.WriteLine("Clients Menu");
                Console.WriteLine("------------");
                Console.WriteLine("[1] View Clients");
                Console.WriteLine("[2] Add Client");
                Console.WriteLine("[3] Return to Main Menu");

                string clientChoice = Console.ReadLine();

                switch (clientChoice)
                {
                    case "1":
                        foreach (var Client in clients)
                        {
                            Console.WriteLine($"ID: {Client.ClientId}, Name: {Client.ClientFirstName} {Client.ClientLastName}, Phone: {Client.ClientPhoneNumber}");
                        }
                        Console.WriteLine("Press any key to return to the Clients Menu...");
                        Console.ReadKey();
                        break;

                    case "2":
                        var client = new Client();

                        Console.WriteLine("Enter Client First Name: ");
                        client.ClientFirstName = Console.ReadLine();

                        Console.WriteLine("Enter Client Last Name: ");
                        client.ClientLastName = Console.ReadLine();

                        Console.WriteLine("Enter Client Phone Number: ");
                        client.ClientPhoneNumber = Console.ReadLine();

                        //Assign a unique ClientId based on the current count of clients
                        client.ClientId = nextClientId++;

                        // Add the new client to the in-memory list
                        clients.Add(client);

                        // Display confirmation message
                        Console.WriteLine($"Client {client.ClientFirstName} {client.ClientLastName} added successfully with ID: {client.ClientId}");
                        Console.WriteLine("Press any key to return to the Clients Menu...");
                        Console.ReadKey();
                        break;

                    case "3":
                        inMenu = false; // Exit the clients menu
                        break;

                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void ShowAppointmentsMenu()
        {
            bool inMenu = true;

            while (inMenu)
            {
                Console.Clear();
                Console.WriteLine("Appointments Menu");
                Console.WriteLine("-----------------");
                Console.WriteLine("[1] View Appointments");
                Console.WriteLine("[2] Add Appointment");
                Console.WriteLine("[3] Cancel Appointment");
                Console.WriteLine("[4] Return to Main Menu");

                string appointmentChoice = Console.ReadLine();

                switch (appointmentChoice)
                {
                    case "1":
                        if (appointments.Count == 0)
                        {
                            Console.WriteLine("No appointments found.");

                        }
                        else
                        {
                            foreach (var Appointment in appointments)
                            {
                                var client = clients.FirstOrDefault(c => c.ClientId == Appointment.ClientId);
                                var artist = artists.FirstOrDefault(a => a.ArtistId == Appointment.ArtistId);

                                Console.WriteLine(
                                    $"Appointment ID: {Appointment.AppointmentId} |" +
                                    $"Client: {client?.ClientFirstName} {client?.ClientLastName} |" +
                                    $"Artist: {artist?.ArtistFirstName} {artist?.ArtistLastName} |" +
                                    $"Start: {Appointment.AppointmentStart} |" +
                                    $"End: {Appointment.AppointmentEnd} |" +
                                    $"Status: {Appointment.Status}");
                            }
                        }

                        Console.WriteLine("Press any key to return to the Appointments Menu...");
                        Console.ReadKey();
                        break;

                    case "2":
                        var appointment = new Appointment();

                        Console.WriteLine("Available Clients:");
                        foreach (var c in clients)
                        {
                            Console.WriteLine($"{c.ClientId} - {c.ClientFirstName} {c.ClientLastName}");
                        }

                        Console.WriteLine("Enter Client ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int clientId))
                        {
                            Console.WriteLine("Invalid Client ID.");
                            Console.ReadKey();
                            break;
                        }

                        // ClientId validation: Check if the entered ClientId exists in the clients list
                        bool clientExists = clients.Any(c => c.ClientId == clientId);

                        if (!clientExists)
                        {
                            Console.WriteLine("Client not found. Please create the client first.");
                            Console.ReadKey();
                            break;
                        }

                        appointment.ClientId = clientId;

                        Console.WriteLine("Available Artists:");
                        foreach (var a in artists)
                        {
                            Console.WriteLine($"{a.ArtistId} - {a.ArtistFirstName} {a.ArtistLastName}");
                        }


                        Console.WriteLine("Enter Artist ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int artistId))
                        {
                            Console.WriteLine("Invalid Artist ID.");
                            Console.ReadKey();
                            break;
                        }

                        var existingArtist = artists.FirstOrDefault(a => a.ArtistId == artistId);

                        if (existingArtist == null)
                        {
                            Console.WriteLine("Artist not found.");
                            Console.ReadKey();
                            break;
                        }

                        appointment.ArtistId = artistId;

                        DateTime startDateTime;

                        while (true)
                        {
                            Console.WriteLine("Enter Appointment Start Date and Time (MM-dd-yyyy HH:mm): ");
                            if (DateTime.TryParseExact(
                                Console.ReadLine(),
                                "MM-dd-yyyy HH:mm",
                                null,
                                System.Globalization.DateTimeStyles.None,
                                out startDateTime))
                            {
                                break; // Valid date and time format, exit the loop
                            }
                            else
                            {
                                Console.WriteLine("Invalid date and time format. Please use MM-dd-yyyy HH:mm (e.g., 10-25-2026 15:30).");
                            }
                        }

                        DateTime endDateTime;

                        while (true)
                        {
                           Console.Write("Enter Appointment End Date and Time (MM-dd-yyyy HH:mm): ");

                            if (DateTime.TryParseExact(
                                Console.ReadLine(),
                                "MM-dd-yyyy HH:mm",
                                null,
                                System.Globalization.DateTimeStyles.None,
                                out endDateTime))
                            {
                            if (endDateTime > startDateTime)
                                {
                                  break;
                                }

                                Console.WriteLine("End date and time must be after the start date and time.");
                            }
                            else
                            {
                                Console.WriteLine("Invalid format. Please enter both the date and time (example: 10-25-2026 15:30).");
                            }
                        }

                        appointment.AppointmentStart = startDateTime;
                        appointment.AppointmentEnd = endDateTime;

                        // Check if the artist is on leave during the requested appointment time
                        bool artistOnLeave = leaves.Any(l =>
                            l.ArtistId == artistId &&
                            startDateTime < l.LeaveEnd &&
                            endDateTime > l.LeaveStart);

                        if (artistOnLeave)
                        {
                            Console.WriteLine("Cannot book appointment: Artist is on leave during this time.");
                            Console.ReadKey();
                            break;
                        }

                        bool overlappingAppointment = appointments.Any(a =>
                            a.ArtistId == artistId &&
                            a.Status != AppointmentStatus.Cancelled &&
                            startDateTime < a.AppointmentEnd &&
                            endDateTime > a.AppointmentStart);

                        if (overlappingAppointment)
                        {
                            Console.WriteLine("Cannot book appointment: The artist already has an appointment during this time.");
                            Console.ReadKey();
                            break;
                        }

                        appointment.Status = AppointmentStatus.Scheduled;

                        // Assign a unique AppointmentId based on the current count of appointments
                        appointment.AppointmentId = nextAppointmentId++;

                        // Add the new appointment to the in-memory list
                        appointments.Add(appointment);

                        // Display confirmation message
                        Console.WriteLine($"Appointment added successfully with ID: {appointment.AppointmentId}");
                        Console.WriteLine("Press any key to return to the Appointments Menu...");
                        Console.ReadKey();

                        break;

                    case "3":
                        Console.WriteLine("Enter Appointment ID to cancel: ");

                        if (appointments.Count > 0)
                        {
                            if (!int.TryParse(Console.ReadLine(), out int appointmentIdToCancel))
                            {
                                Console.WriteLine("Invalid Appointment ID.");
                                Console.WriteLine("Press any key to return to the Appointments Menu...");
                                Console.ReadKey();
                                break;
                            }

                            var appointmentToCancel = appointments.FirstOrDefault(a => a.AppointmentId == appointmentIdToCancel);

                            if (appointmentToCancel != null)
                            {
                                if (appointmentToCancel.Status == AppointmentStatus.Cancelled)
                                {
                                    Console.WriteLine($"Appointment ID {appointmentIdToCancel} is already canceled.");
                                    Console.WriteLine("Press any key to return to the Appointments Menu...");
                                    Console.ReadKey();
                                    break;
                                }

                                appointmentToCancel.Status = AppointmentStatus.Cancelled;
                                Console.WriteLine($"Appointment ID {appointmentIdToCancel} has been canceled.");
                            }
                            else
                            {
                                Console.WriteLine($"Appointment ID {appointmentIdToCancel} not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No appointments available to cancel.");
                        }

                        Console.WriteLine("Press any key to return to the Appointments Menu...");
                        Console.ReadKey();
                        break;

                    case "4":
                        inMenu = false; // Exit the appointments menu
                        break;

                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void ShowArtistsMenu()
        {
            bool inMenu = true;

            while (inMenu)
            {
                Console.Clear();
                Console.WriteLine("Artists Menu");
                Console.WriteLine("------------");
                Console.WriteLine("[1] View Artists");
                Console.WriteLine("[2] Add Artist Leave");
                Console.WriteLine("[3] Return to Main Menu");

                string artistChoice = Console.ReadLine();

                switch (artistChoice)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Current Artists:");
                        Console.WriteLine("----------------");
                        foreach (var artist in artists)
                        {
                            Console.WriteLine($"ID: {artist.ArtistId} | Name: {artist.ArtistFirstName} {artist.ArtistLastName}");
                        }
                        Console.WriteLine("Press any key to return to the Artists Menu...");
                        Console.ReadKey();
                        break;

                    case "2":
                        Console.Clear();
                        Console.WriteLine("Add Artist Leave");
                        Console.WriteLine("----------------");

                        var newLeave = new Leave();

                        int artistId;

                        while (true)
                        {
                            Console.Write("Enter Artist ID: ");
                            string input = Console.ReadLine();

                            if (int.TryParse(input, out artistId) && artists.Any(a => a.ArtistId == artistId))
                                break;

                            Console.WriteLine("Invalid Artist ID. Please try again.");
                        }

                        newLeave.ArtistId = artistId;

                        DateTime startDate;
                        while (true)
                        {
                            Console.Write("Enter Leave Start Date (MM-dd-yyyy): ");
                            if (DateTime.TryParseExact(
                                Console.ReadLine(),
                                "MM-dd-yyyy",
                                null,
                                System.Globalization.DateTimeStyles.None,
                                out startDate))
                            {
                                break; // Valid date format, exit the loop
                            }
                            else
                            {
                                Console.WriteLine("Invalid format. Please use MM-dd-yyyy (e.g., 10-25-2026).");
                            }
                        }

                        newLeave.LeaveStart = startDate;

                        Console.Write("Enter Leave End Date (MM-dd-yyyy): ");
                        DateTime endDate;
                        while (true)
                        {
                            if (DateTime.TryParseExact(
                                Console.ReadLine(),
                                "MM-dd-yyyy",
                                null,
                                System.Globalization.DateTimeStyles.None,
                                out endDate))
                            {
                                if (endDate >= startDate)
                                    break; // Valid date format and end date is after start date, exit the loop
                                else
                                    Console.WriteLine("End date must be on or after the start date. Please try again.");
                            }
                            else
                            {
                                Console.WriteLine("Invalid format. Please use MM-dd-yyyy (e.g., 10-25-2026).");
                            }
                        }
                        newLeave.LeaveEnd = endDate;

                        // Assign a unique LeaveId based on the current count of leaves
                        newLeave.LeaveId = nextLeaveId++;
                        leaves.Add(newLeave);

                        Console.WriteLine("Leave scheduled successfully!");
                        Console.WriteLine("Press any key to return to the Artists Menu...");
                        Console.ReadKey();
                        break;

                    case "3":
                        inMenu = false; // Exit the artists menu
                        break;

                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void ShowPaymentsMenu()
        {
            bool inMenu = true;

            while (inMenu)
            {
                Console.Clear();
                Console.WriteLine("Payments Menu");
                Console.WriteLine("-------------");
                Console.WriteLine("[1] View Payments");
                Console.WriteLine("[2] Add Payment");
                Console.WriteLine("[3] Return to Main Menu");

                string paymentChoice = Console.ReadLine();

                switch (paymentChoice)
                {
                    case "1":
                        break;

                    case "2":
                        var Payment = new Payment();
                        break;

                    case "3":
                        inMenu = false; // Exit the payments menu
                        break;

                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void ShowReportsMenu()
        {
            bool inMenu = true;

            while (inMenu)
            {
                Console.Clear();
                Console.WriteLine("Reports Menu");
                Console.WriteLine("------------");
                Console.WriteLine("[1] Generate Report");
                Console.WriteLine("[2] Return to Main Menu");

                string reportChoice = Console.ReadLine();

                switch (reportChoice)
                {
                    case "1":
                        break;

                    case "2":
                        inMenu = false; // Exit the reports menu
                        break;

                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}