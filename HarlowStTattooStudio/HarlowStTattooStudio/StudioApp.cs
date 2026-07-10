using HarlowStTattooStudio.Services;
using HarlowStTattooStudio.DomainModel;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace HarlowStTattooStudio
{
    public class StudioApp(
        AppointmentService appointmentService,
        ClientService clientService,
        ArtistService artistService,
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
                Console.Clear();

                Console.WriteLine("--------------------------------");
                Console.WriteLine("Welcome! Please make a selection:");
                Console.WriteLine("--------------------------------");
                Console.WriteLine("[1] Manage Clients");
                Console.WriteLine("[2] Manage Appointments");
                Console.WriteLine("[3] Manage Artist Leave");
                Console.WriteLine("[4] Manage Payments");
                Console.WriteLine("[5] View Reports");
                Console.WriteLine("[6] Exit");

                Console.Write("Enter choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowClientsMenu();
                        break;

                    case "2":
                        ShowAppointmentsMenu();
                        break;

                    case "3":
                        ShowLeaveMenu();
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

                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }


        private void ShowClientsMenu()
        {
            bool back = false;

            while (!back)
            {
                Console.Clear();

                Console.WriteLine("--- CLIENTS MENU ---");
                Console.WriteLine("[1] Add Client");
                Console.WriteLine("[2] View Clients");
                Console.WriteLine("[3] Back to Main Menu");

                Console.Write("Enter choice: ");

                string choice = Console.ReadLine();


                switch (choice)
                {
                    case "1":
                        Console.Write("Enter Client First Name: ");
                        string firstName = Console.ReadLine();

                        Console.Write("Enter Client Last Name: ");
                        string lastName = Console.ReadLine();

                        Console.Write("Enter Client Phone Number: ");
                        string phone = Console.ReadLine();


                        Client newClient = new Client
                        {
                            ClientFirstName = firstName,
                            ClientLastName = lastName,
                            ClientPhoneNumber = phone
                        };


                        bool added = clientService.AddClient(newClient);


                        if (added)
                        {
                            Console.WriteLine("Client successfully added!");
                        }
                        else
                        {
                            Console.WriteLine("Client already exists.");
                        }


                        Console.WriteLine("Press any key to return...");
                        Console.ReadKey();

                        break;


                    case "2":
                        Console.Clear();
                        Console.WriteLine("--- CLIENT LIST ---");

                        IEnumerable<Client> clients = clientService.GetAllClients();

                        foreach (Client client in clients)
                        {
                            Console.WriteLine($"Client ID: {client.ClientId}, " + $"Name: {client.ClientFirstName} {client.ClientLastName}, " + $"Phone: {client.ClientPhoneNumber}");
                        }

                        Console.WriteLine("Press any key to return...");
                        Console.ReadKey();

                        break;

                    case "3":
                        back = true;
                        break;

                    default:
                        Console.WriteLine("Invalid selection.");
                        Console.ReadKey();

                        break;
                }
            }
        }
        private void ShowAppointmentsMenu()
        {
            bool back = false;

            while (!back)
            {
                Console.Clear();
                Console.WriteLine("--- APPOINTMENTS MENU ---");
                Console.WriteLine("[1] View Appointments");
                Console.WriteLine("[2] Add Appointment");
                Console.WriteLine("[3] Cancel Appointment");
                Console.WriteLine("[4] Back to Main Menu");

                Console.Write("Enter choice: ");

                string choice = Console.ReadLine();


                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("--- APPOINTMENT LIST ---");
                        IEnumerable<Appointment> appointments = appointmentService.GetAll();

                        foreach (Appointment appointment in appointments)
                        {
                            Console.WriteLine(
                                $"Appointment ID: {appointment.AppointmentId}, " +
                                $"Client ID: {appointment.ClientId}, " +
                                $"Artist ID: {appointment.ArtistId}, " +
                                $"Start: {appointment.AppointmentStart:MM-dd-yyyy HH:mm}, " +
                                $"End: {appointment.AppointmentEnd:MM-dd-yyyy HH:mm}, " +
                                $"Status: {appointment.Status}");
                        }

                        Console.WriteLine("Press any key to return...");
                        Console.ReadKey();
                        break;


                    case "2":
                        int clientId;

                        while (true)
                        {
                            Console.Write("Enter Client ID: ");
                            if (int.TryParse(Console.ReadLine(), out clientId))
                            {
                                break;
                            }
                            Console.WriteLine("Invalid Client ID. Please try again.");
                        }

                        int artistId;

                        while (true)
                        {
                            Console.Write("Enter Artist ID: ");
                            if (int.TryParse(Console.ReadLine(), out artistId))
                            {
                                break;
                            }
                            Console.WriteLine("Invalid Artist ID. Please try again.");
                        }


                        DateTime appointmentStart;
                        while (true)
                        {
                            Console.Write("Enter Appointment Start (MM-dd-yyyy HH:mm): ");

                            if (DateTime.TryParseExact(Console.ReadLine(), "MM-dd-yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out appointmentStart))
                            {
                                break;
                            }

                            Console.WriteLine("Invalid date format. Please try again.");
                        }


                        DateTime appointmentEnd;
                        while (true)
                        {
                            Console.Write("Enter Appointment End (MM-dd-yyyy HH:mm): ");

                            if (DateTime.TryParseExact(Console.ReadLine(), "MM-dd-yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out appointmentEnd))
                            {
                                if (appointmentEnd > appointmentStart)
                                {
                                    break;
                                }
                                Console.WriteLine("End time must be after start time.");
                            }
                            else
                            {
                                Console.WriteLine("Invalid date format. Please try again.");
                            }
                        }


                        Appointment newAppointment = new Appointment
                        {
                            ClientId = clientId,
                            ArtistId = artistId,
                            AppointmentStart = appointmentStart,
                            AppointmentEnd = appointmentEnd,
                            Status = AppointmentStatus.Pending
                        };


                        bool scheduled = appointmentService.ScheduleAppointment(newAppointment);

                        if (scheduled)
                        {
                            Console.WriteLine("Appointment scheduled successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Unable to schedule appointment.");
                            Console.WriteLine("Check artist availability or appointment details.");
                        }


                        Console.WriteLine("Press any key to return...");
                        Console.ReadKey();
                        break;


                    case "3":
                        int appointmentId;

                        while (true)
                        {
                            Console.Write("Enter Appointment ID: ");

                            if (int.TryParse(Console.ReadLine(), out appointmentId))
                            {
                                break;
                            }

                            Console.WriteLine("Invalid Appointment ID.");
                        }

                        bool cancelled = appointmentService.CancelAppointment(appointmentId);

                        if (cancelled)
                        {
                            Console.WriteLine("Appointment cancelled successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Unable to cancel appointment.");
                        }

                        Console.WriteLine("Press any key to return...");
                        Console.ReadKey();
                        break;


                    case "4":
                        back = true;
                        break;


                    default:

                        Console.WriteLine("Invalid selection.");
                        Console.ReadKey();
                        break;
                }
            }
        }
        private void ShowLeaveMenu()
        {
            bool back = false;

            while (!back)
            {
                Console.Clear();
                Console.WriteLine("--- LEAVE MENU ---");
                Console.WriteLine("[1] Schedule Leave");
                Console.WriteLine("[2] View Artist Leave");
                Console.WriteLine("[3] Back to Main Menu");

                Console.Write("Enter choice: ");

                string choice = Console.ReadLine();


                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("--- AVAILABLE ARTISTS ---");

                        var artists = artistService.GetAllArtists();

                        foreach (Artist artist in artists)
                        {
                            Console.WriteLine($"Artist ID: {artist.ArtistId}, " + $"Name: {artist.ArtistFirstName} {artist.ArtistLastName}");
                        }

                        int artistId;
                        while (true)
                        {
                            Console.Write("Enter Artist ID: ");

                            if (int.TryParse(Console.ReadLine(), out artistId))
                            {
                                break;
                            }
                            Console.WriteLine("Invalid Artist ID. Please try again.");
                        }

                        DateTime leaveStart;
                        while (true)
                        {
                            Console.Write("Enter Leave Start (MM-dd-yyyy HH:mm): ");

                            if (DateTime.TryParseExact(Console.ReadLine(), "MM-dd-yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out leaveStart))
                            {
                                break;
                            }
                            Console.WriteLine("Invalid date format. Please try again.");
                        }

                        DateTime leaveEnd;
                        while (true)
                        {
                            Console.Write("Enter Leave End (MM-dd-yyyy HH:mm): ");

                            if (DateTime.TryParseExact(Console.ReadLine(), "MM-dd-yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out leaveEnd))
                            {
                                if (leaveEnd > leaveStart)
                                {
                                    break;
                                }
                                Console.WriteLine("Leave end must be after leave start.");
                            }
                            else
                            {
                                Console.WriteLine("Invalid date format. Please try again.");
                            }
                        }

                        Leave newLeave = new Leave
                        {
                            ArtistId = artistId,
                            LeaveStart = leaveStart,
                            LeaveEnd = leaveEnd
                        };

                        bool scheduled = leaveService.ScheduleLeave(newLeave);

                        if (scheduled)
                        {
                            Console.WriteLine("Leave scheduled successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Unable to schedule leave.");
                            Console.WriteLine("The artist may already have leave during this period.");
                        }

                        Console.WriteLine("Press any key to return...");
                        Console.ReadKey();
                        break;


                    case "2":
                        Console.Clear();
                        Console.WriteLine("--- VIEW ARTIST LEAVE ---");

                        int viewArtistId;

                        while (true)
                        {
                            Console.Write("Enter Artist ID: ");

                            if (int.TryParse(Console.ReadLine(), out viewArtistId))
                            {
                                break;
                            }
                            Console.WriteLine("Invalid Artist ID. Please try again.");
                        }

                        List<Leave> leaves = leaveService.GetArtistLeave(viewArtistId);

                        if (leaves.Count == 0)
                        {
                            Console.WriteLine("No leave found for this artist.");
                        }
                        else
                        {
                            foreach (Leave leave in leaves)
                            {
                                Console.WriteLine(
                                    $"Leave ID: {leave.LeaveId}, " + $"Start: {leave.LeaveStart:MM-dd-yyyy HH:mm}, " + $"End: {leave.LeaveEnd:MM-dd-yyyy HH:mm}");
                            }
                        }

                        Console.WriteLine("Press any key to return...");
                        Console.ReadKey();
                        break;


                    case "3":
                        back = true;
                        break;


                    default:
                        Console.WriteLine("Invalid selection.");
                        Console.ReadKey();
                        break;
                }
            }
        }
        private void ShowPaymentsMenu()
        {
            bool back = false;

            while (!back)
            {
                Console.Clear();

                Console.WriteLine("--- PAYMENTS MENU ---");
                Console.WriteLine("[1] Record Deposit");
                Console.WriteLine("[2] Record Full Payment");
                Console.WriteLine("[3] Back to Main Menu");

                Console.Write("Enter choice: ");

                string choice = Console.ReadLine();


                switch (choice)
                {
                    case "1":
                        int depositAppointmentId;
                        while (true)
                        {
                            Console.Write("Enter Appointment ID: ");

                            if (int.TryParse(Console.ReadLine(), out depositAppointmentId))
                            {
                                break;
                            }
                            Console.WriteLine("Invalid Appointment ID. Please try again.");
                        }

                        decimal depositAmount;
                        while (true)
                        {
                            Console.Write("Enter Deposit Amount: ");

                            if (decimal.TryParse(Console.ReadLine(), out depositAmount) && depositAmount > 0)
                            {
                                break;
                            }
                            Console.WriteLine("Invalid amount. Please try again.");
                        }

                        bool depositRecorded = paymentService.RecordDeposit(depositAppointmentId, depositAmount);

                        if (depositRecorded)
                        {
                            Console.WriteLine("Deposit recorded successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Unable to record deposit. Check appointment ID.");
                        }

                        bool ConfirmAppointment = appointmentService.ConfirmAppointment(depositAppointmentId);

                        Console.WriteLine("Press any key to return...");
                        Console.ReadKey();
                        break;


                    case "2":
                        int fullPaymentAppointmentId;
                        while (true)
                        {
                            Console.Write("Enter Appointment ID: ");

                            if (int.TryParse(Console.ReadLine(), out fullPaymentAppointmentId))
                            {
                                break;
                            }
                            Console.WriteLine("Invalid Appointment ID. Please try again.");
                        }

                        decimal fullPaymentAmount;
                        while (true)
                        {
                            Console.Write("Enter Full Payment Amount: ");

                            if (decimal.TryParse(Console.ReadLine(), out fullPaymentAmount) && fullPaymentAmount > 0)
                            {
                                break;
                            }
                            Console.WriteLine("Invalid amount. Please try again.");
                        }

                        bool fullPaymentRecorded = paymentService.RecordFullPayment(fullPaymentAppointmentId, fullPaymentAmount);
                        if (fullPaymentRecorded)
                        {
                            Console.WriteLine("Full payment recorded successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Unable to record full payment. Check appointment ID.");
                        }

                        Console.WriteLine("Press any key to return...");
                        Console.ReadKey();
                        break;


                    case "3":
                        back = true;
                        break;


                    default:
                        Console.WriteLine("Invalid selection.");
                        Console.ReadKey();
                        break;
                }
            }
        }
        private void ShowReportsMenu()
        {
            bool back = false;

            while (!back)
            {
                Console.Clear();

                Console.WriteLine("--- REPORTS MENU ---");
                Console.WriteLine("[1] View Revenue for All Confirmed Appointments");
                Console.WriteLine("[2] View Total Deposits Collected");
                Console.WriteLine("[3] View Revenue by Date Range");
                Console.WriteLine("[4] Back to Main Menu");

                Console.Write("Enter choice: ");

                string choice = Console.ReadLine();


                switch (choice)
                {
                    case "1":
                        decimal totalRevenue = reportService.GetConfirmedAppointmentsRevenue();

                        Console.WriteLine(
                            $"Total Revenue for Confirmed Appointments: {totalRevenue:C}");


                        Console.WriteLine("Press any key to return...");
                        Console.ReadKey();
                        break;



                    case "2":
                        decimal totalDeposits = reportService.GetTotalDeposits();

                        Console.WriteLine(
                            $"Total Deposits Collected: {totalDeposits:C}");

                        Console.WriteLine("Press any key to return...");
                        Console.ReadKey();
                        break;



                    case "3":
                        DateTime startDate;
                        while (true)
                        {
                            Console.Write("Enter Start Date (MM-dd-yyyy HH:mm): ");

                            if (DateTime.TryParseExact(
                                Console.ReadLine(),
                                "MM-dd-yyyy HH:mm",
                                CultureInfo.InvariantCulture,
                                DateTimeStyles.None,
                                out startDate))
                            {
                                break;
                            }
                            Console.WriteLine("Invalid date format. Please try again.");
                        }

                        DateTime endDate;
                        while (true)
                        {
                            Console.Write("Enter End Date (MM-dd-yyyy HH:mm): ");


                            if (DateTime.TryParseExact(Console.ReadLine(),"MM-dd-yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
                            {
                                if (endDate >= startDate)
                                {
                                    break;
                                }
                                Console.WriteLine("End date must be after start date.");
                            }
                            else
                            {
                                Console.WriteLine("Invalid date format. Please try again.");
                            }
                        }

                        decimal revenueByDate =
                            reportService.GetRevenueByDateRange(startDate, endDate);


                        Console.WriteLine($"Revenue from {startDate:MM-dd-yyyy HH:mm} " + $"to {endDate:MM-dd-yyyy HH:mm}: {revenueByDate:C}");
                        Console.WriteLine("Press any key to return...");
                        Console.ReadKey();
                        break;


                    case "4":
                        back = true;
                        break;


                    default:
                        Console.WriteLine("Invalid selection.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}