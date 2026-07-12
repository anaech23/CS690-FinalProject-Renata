using HarlowStTattooStudio.Services;
using HarlowStTattooStudio.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace HarlowStTattooStudio.Menus
{
    public class ClientMenu(ClientService clientService)
    {
        public void Show()
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
    }
}

