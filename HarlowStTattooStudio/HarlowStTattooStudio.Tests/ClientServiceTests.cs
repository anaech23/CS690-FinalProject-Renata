using HarlowStTattooStudio.Data;
using HarlowStTattooStudio.DomainModel;
using HarlowStTattooStudio.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace HarlowStTattooStudio.Tests
{
    public class ClientServiceTests
    {
        private readonly StudioData _data;
        private readonly ClientService _service;

        public ClientServiceTests()
        {
            _data = new StudioData();
            _service = new ClientService(_data);
        }

        [Fact]
        public void AddClient_NewPhoneNumber_ReturnsTrue()
        {
            // Add client
            var client = new Client { ClientFirstName = "John", ClientPhoneNumber = "555-1234" };
            
            // Act
            var result = _service.AddClient(client);

            //Assert
            Assert.True(result);
            Assert.Single(_data.Clients);
        }

        [Fact]
        public void AddClient_DuplicatePhoneNumber_ReturnsFalse()
        {
            // Add first client
            _service.AddClient(new Client { ClientFirstName = "John", ClientPhoneNumber = "555-1234" });

            // Try to add same number
            var duplicate = new Client { ClientFirstName = "Jane", ClientPhoneNumber = "555-1234" };
            var result = _service.AddClient(duplicate);

            // Assert
            Assert.False(result);
            Assert.Single(_data.Clients); // List count should still be 1
        }
    }
}
