using System;
using System.Collections.Generic;
using System.Text;

namespace HarlowStTattooStudio.DomainModel
{
    public class Client
    {
        public int ClientId { get; set; }
        public string ClientFirstName { get; set; }
        public string ClientLastName { get; set; }
        public string ClientPhoneNumber { get; set; }
    }
}
