using HarlowStTattooStudio.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace HarlowStTattooStudio.Menus
{
    public class ReportMenu(ReportService reportService)
    {
        public void Show()
        {
            // Password Check
            Console.WriteLine("Please enter administrator password to view this menu:");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int password) && password == 9876)
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
                            Console.WriteLine($"Total Revenue for Confirmed Appointments: {totalRevenue:C}");
                            Console.WriteLine("Press any key to return...");
                            Console.ReadKey();
                            break;

                        case "2":
                            decimal totalDeposits = reportService.GetTotalDeposits();
                            Console.WriteLine($"Total Deposits Collected: {totalDeposits:C}");
                            Console.WriteLine("Press any key to return...");
                            Console.ReadKey();
                            break;

                        case "3":
                            DateTime startDate;
                            while (true)

                            {
                                Console.Write("Enter Start Date (MM-dd-yyyy): ");

                                if (DateTime.TryParseExact(Console.ReadLine(),"MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate))
                                {
                                    break;
                                }
                                Console.WriteLine("Invalid date format. Please try again.");
                            }


                            DateTime endDate;
                            while (true)

                            {
                                Console.Write("Enter End Date (MM-dd-yyyy): ");
                                
                                if (DateTime.TryParseExact(Console.ReadLine(), "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
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

                            decimal revenueByDate = reportService.GetRevenueByDateRange(startDate, endDate);


                            Console.WriteLine($"Revenue from {startDate:MM-dd-yyyy} " + $"to {endDate:MM-dd-yyyy}: {revenueByDate:C}");
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
            else
            {
                Console.WriteLine("Access Denied. Incorrect password.");
                Console.ReadKey();
            }
        }
    }
}
