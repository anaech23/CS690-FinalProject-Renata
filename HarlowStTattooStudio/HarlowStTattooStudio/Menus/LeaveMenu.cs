using HarlowStTattooStudio.DomainModel;
using HarlowStTattooStudio.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace HarlowStTattooStudio.Menus
{
    public class LeaveMenu(LeaveService leaveService, ArtistService artistService)
    {
        public void Show()
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
    }
}