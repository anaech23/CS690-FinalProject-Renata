using HarlowStTattooStudio.Services;
using HarlowStTattooStudio.DomainModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using HarlowStTattooStudio.Menus;

namespace HarlowStTattooStudio
{
    public class StudioApp(
        ClientMenu clientMenu,
        AppointmentMenu appointmentMenu,
        LeaveMenu leaveMenu,
        PaymentMenu paymentMenu,
        ReportMenu reportMenu)
    {
        public void Run()
        {
            Console.Title = "HARLOW STREET TATTOO MANAGEMENT SYSTEM";

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

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": clientMenu.Show(); break;
                    case "2": appointmentMenu.Show(); break;
                    case "3": leaveMenu.Show(); break;
                    case "4": paymentMenu.Show(); break;
                    case "5": reportMenu.Show(); break;
                    case "6": running = false; break;
                }
            }
        }
    }
}