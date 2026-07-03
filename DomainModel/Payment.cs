using System;
using System.Collections.Generic;
using System.Text;

namespace HarlowStTattooStudio.DomainModel
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public int AppointmentID { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
