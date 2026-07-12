using HarlowStTattooStudio.DomainModel;
using HarlowStTattooStudio.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace HarlowStTattooStudio.Menus
{
    public class AppointmentMenu(AppointmentService appointmentService)
    {
        public void Show()
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
                            string display = $"Appointment ID: {appointment.AppointmentId}, " +
                                             $"Client ID: {appointment.ClientId}, " +
                                             $"Artist ID: {appointment.ArtistId}, " +
                                             $"Start: {appointment.AppointmentStart:MM-dd-yyyy HH:mm}, " +
                                             $"End: {appointment.AppointmentEnd:MM-dd-yyyy HH:mm}, " +
                                             $"Status: {appointment.Status}";

                            if (appointment.FollowupToAppointmentId.HasValue)
                            {
                                display += $" [Follow-up to ID {appointment.FollowupToAppointmentId.Value}]";
                            }

                            Console.WriteLine(display);
                        }

                        Console.WriteLine("Press any key to return...");
                        Console.ReadKey();
                        break;


                    case "2":
                        Console.Write("Enter Client ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int clientId))
                        {
                            Console.WriteLine("Invalid ID.");
                            Console.ReadKey();
                            break;
                        }

                        // Get the basics first (Artist, Start, End)
                        Appointment newAppt = GetAppointmentDetailsFromUser(clientId);

                        // Ask if this is a follow-up
                        Console.Write("Is this a follow-up appointment? [Y/N]: ");
                        string isFollowUp = Console.ReadLine()?.ToLower();

                        if (isFollowUp == "y")
                        {
                            var past = appointmentService.GetPastAppointments(clientId).ToList();

                            if (!past.Any())
                            {
                                Console.WriteLine("No past appointments found. Cannot link as follow-up.");
                            }
                            else
                            {
                                Console.WriteLine("Select previous appointment to link:");
                                for (int i = 0; i < past.Count; i++)
                                    Console.WriteLine($"[{i + 1}] Date: {past[i].AppointmentStart:MM-dd-yyyy} | ID: {past[i].AppointmentId}");

                                Console.Write("Enter selection: ");
                                if (int.TryParse(Console.ReadLine(), out int sel) && sel > 0 && sel <= past.Count)
                                {
                                    newAppt.FollowupToAppointmentId = past[sel - 1].AppointmentId;
                                }
                            }
                        }

                        // Send to the service
                        if (appointmentService.ScheduleAppointment(newAppt))
                        {
                            Console.WriteLine("Appointment scheduled successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Unable to schedule. Check artist availability.");
                        }

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

        private Appointment GetAppointmentDetailsFromUser(int clientId)
        {
            Console.WriteLine("Enter Artist ID: ");
            int artistId = int.Parse(Console.ReadLine());

            DateTime start, end;
            while (true)
            {
                Console.WriteLine("Start (MM-dd-yyyy HH:mm): ");
                if (DateTime.TryParseExact(Console.ReadLine(), "MM-dd-yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out start))
                    break;
            }
            while (true)
            {
                Console.Write("End (MM-dd-yyyy HH:mm): ");
                if (DateTime.TryParseExact(Console.ReadLine(), "MM-dd-yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out end) && end > start)
                    break;
                Console.WriteLine("End must be after start.");
            }

            return new Appointment
            {
                ClientId = clientId,
                ArtistId = artistId,
                AppointmentStart = start,
                AppointmentEnd = end,
                Status = AppointmentStatus.Pending
            };
        }
    }
}
