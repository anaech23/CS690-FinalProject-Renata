using System;
using System.Collections.Generic;
using System.Text;

namespace HarlowStTattooStudio.DomainModel
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int ClientId { get; set; }
        public int ArtistId { get; set; }
        public DateTime AppointmentStart { get; set; }
        public DateTime AppointmentEnd { get; set; }
        public int? FollowupAppointmentId { get; set; }
        public decimal DepositAmount { get; set; }
        public bool DepositPaid { get; set; }
        public AppointmentStatus Status { get; set; }
    }
}
