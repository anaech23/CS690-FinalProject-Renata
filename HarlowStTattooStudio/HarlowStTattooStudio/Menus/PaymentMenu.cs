using HarlowStTattooStudio.DomainModel;
using HarlowStTattooStudio.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace HarlowStTattooStudio.Menus
{
    public class PaymentMenu(PaymentService paymentService, AppointmentService appointmentService)

    {
        public void Show()
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
    }
}
