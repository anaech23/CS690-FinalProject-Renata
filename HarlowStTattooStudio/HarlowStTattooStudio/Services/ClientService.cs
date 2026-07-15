using System;
using System.Collections.Generic;
using System.Text;
using HarlowStTattooStudio.Data;
using HarlowStTattooStudio.DomainModel;
using System.Linq;

namespace HarlowStTattooStudio.Services
{
    public class ClientService
    {
        private readonly StudioData _studioData;

        public ClientService(StudioData studioData)
        {
            _studioData = studioData;
        }

        public bool AddClient(Client newClient)
        {
             // Check if the client already exists based on phone number
             bool clientExists = _studioData.Clients.Any(c => c.ClientPhoneNumber == newClient.ClientPhoneNumber);
             if (clientExists)
             {
                return false; // Client already exists
             }
             newClient.ClientId = _studioData.NextClientId++; // Assign a new ClientId
             _studioData.Clients.Add(newClient);
            _studioData.Save();
             return true; // Client added successfully   
        }

        // Method to return all clients
        public IEnumerable<Client> GetAllClients()
        {
            return _studioData.Clients;
        }
    }
}
