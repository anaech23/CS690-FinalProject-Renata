using HarlowStTattooStudio.Data;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using HarlowStTattooStudio.DomainModel;
using System.Linq;

namespace HarlowStTattooStudio.Services
{
    public class AppointmentService
    {
        private readonly StudioData _studioData;

        public AppointmentService(StudioData studioData)
        {
            _studioData = studioData;
        }

        public bool ScheduleAppointment(Appointment newAppointment)
        {
            // Check for deposit payment
            bool hasPaidDeposit = _studioData.Payments.Any(p =>
                p.ClientId == newAppointment.ClientId && p.PaymentType >= PaymentType.Deposit);
            if (!hasPaidDeposit)
            {
                return false; // Cannot schedule appointment without deposit payment
            }

            // Check if artist is on leave
            bool isArtistOnLeave = _studioData.Leaves.Any(l =>
                l.ArtistId == newAppointment.ArtistId &&
                newAppointment.AppointmentDate >= l.LeaveStart &&
                newAppointment.AppointmentDate <= l.LeaveEnd);
            if (isArtistOnLeave)
            {
                return false; // Cannot schedule appointment if artist is on leave
            }

            bool hasOverlap = _studioData.Appointments.Any(a =>
                a.ArtistId == newAppointment.ArtistId &&
                a.Status != DomainModel.AppointmentStatus.Cancelled &&
                newAppointment.AppointmentStart < a.AppointmentEnd &&
                newAppointment.AppointmentEnd > a.AppointmentStart);
            if (hasOverlap)
            {
                return false; // Cannot schedule appointment due to overlap
            }

            newAppointment.AppointmentId = _studioData.NextAppointmentId++;
            newAppointment.Status = DomainModel.AppointmentStatus.Scheduled;
            _studioData.Appointments.Add(newAppointment);

            return true; // Appointment scheduled successfully
        }

        public IEnumerable<Appointment> GetAll()
        {
            return _studioData.Appointments;
        }
        
        public bool CancelAppointment(int appointmentId)
        {
            var app = _studioData.Appointments.FirstOrDefault(a => a.AppointmentId == appointmentId);

            if (app == null || app.Status == AppointmentStatus.Cancelled)
                return false;

            app.Status = AppointmentStatus.Cancelled;
            return true;
        }
    }
}
